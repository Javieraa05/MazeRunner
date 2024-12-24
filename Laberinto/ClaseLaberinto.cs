
    using System;
    using System.Collections.Generic;

    public enum TipoTrampa
    {
        Ninguna,
        Puas,
        Fuego,
        Osos
    }

    public class Tile
    {
        public bool EsCamino { get; set; } = false;      // Indica si es camino o muro
        public TipoTrampa Trampa { get; set; } = TipoTrampa.Ninguna;  // Tipo de trampa
        public bool TieneLlave { get; set; } = false;    // Indica si tiene llave

        public Tile(bool esCamino = false)
        {
            EsCamino = esCamino;
        }
    }

    public class Laberinto
    {
        public int sizeX = 25;
        public int sizeY = 25;
        public Tile[,] tablero;
        private Random rand = new Random();

        public void InicializarTablero()
        {
            tablero = new Tile[sizeX, sizeY];
            for (int i = 0; i < sizeX; i++)
            {
                for (int j = 0; j < sizeY; j++)
                {
                    tablero[i, j] = CrearTilePorDefecto();
                }
            }
        }

        private Tile CrearTilePorDefecto()
        {
            return new Tile();
        }

        public void GenerarLaberintoConPrim()
        {
            var celdasAbiertas = new List<(int, int)>();
            int[] dx = { -1, 1, 0, 0 }; // arriba, abajo, izquierda, derecha
            int[] dy = { 0, 0, -1, 1 };

            int inicioX = 1, inicioY = 1;
            tablero[inicioX, inicioY].EsCamino = true;
            celdasAbiertas.Add((inicioX, inicioY));

            while (celdasAbiertas.Count > 0)
            {
                var (x, y) = celdasAbiertas[rand.Next(celdasAbiertas.Count)];

                var celdasAdyacentes = new List<(int, int)>();
                for (int i = 0; i < 4; i++)
                {
                    int nuevoX = x + dx[i] * 2;
                    int nuevoY = y + dy[i] * 2;

                    if (EsPosicionValida(nuevoX, nuevoY) && !tablero[nuevoX, nuevoY].EsCamino)
                    {
                        celdasAdyacentes.Add((nuevoX, nuevoY));
                    }
                }

                if (celdasAdyacentes.Count > 0)
                {
                    var (nx, ny) = celdasAdyacentes[rand.Next(celdasAdyacentes.Count)];
                    tablero[nx, ny].EsCamino = true;
                    tablero[(x + nx) / 2, (y + ny) / 2].EsCamino = true; // Conectar celdas
                    celdasAbiertas.Add((nx, ny));
                }
                else
                {
                    celdasAbiertas.Remove((x, y));
                }
            }
        }

        public void ColocarTrampas(int cantidad, TipoTrampa tipoTrampa)
        {
            var posicionesCaminos = new List<(int, int)>();

            for (int i = 0; i < sizeX; i++)
            {
                for (int j = 0; j < sizeY; j++)
                {
                    if (tablero[i, j].EsCamino)
                    {
                        posicionesCaminos.Add((i, j));
                    }
                }
            }

            int cantidadAColocar = Math.Min(cantidad, posicionesCaminos.Count);
            for (int i = 0; i < cantidadAColocar; i++)
            {
                var (x, y) = posicionesCaminos[rand.Next(posicionesCaminos.Count)];
                posicionesCaminos.Remove((x, y));

                tablero[x, y].Trampa = tipoTrampa;
            }
        }

        private bool EsPosicionValida(int x, int y)
        {
            return x >= 0 && x < sizeX && y >= 0 && y < sizeY;
        }
    }

