
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
        public bool TieneLlave { get; set; } = false;  
        public (int,int) PosicionLlave = (0,0);  

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

            //Abrir puertas 
            tablero[12,0].EsCamino = true;
            tablero[12,1].EsCamino = true;

            tablero[0,12].EsCamino = true;
            tablero[1,12].EsCamino = true;

            tablero[23,12].EsCamino = true;
            tablero[24,12].EsCamino = true;

            tablero[12,23].EsCamino = true;
            tablero[12,24].EsCamino = true;
        }

        public void ColocarTrampas(int cantidad, TipoTrampa tipoTrampa)
        {
            var posicionesCaminos = new List<(int, int)>();

            for (int i = 1; i < sizeX-1; i++)
            {
                for (int j = 1; j < sizeY-1; j++)
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
   
        public void ColocarLlave()
        {
            var posicionesCaminos = new List<(int, int)>();

            for (int i = 5; i < sizeX-5; i++)
            {
                for (int j = 5; j < sizeY-5; j++)
                {
                    if (tablero[i, j].EsCamino)
                    {
                        posicionesCaminos.Add((i, j));
                    }
                }
            }

            if (posicionesCaminos.Count > 0)
            {
                var (x, y) = posicionesCaminos[rand.Next(posicionesCaminos.Count)];
                tablero[x, y].TieneLlave = true;
                tablero[x,y].PosicionLlave = (x,y);
            }

    }
        private bool EsPosicionValida(int x, int y)
        {
            return x >= 0 && x < sizeX && y >= 0 && y < sizeY;
        }
    }

