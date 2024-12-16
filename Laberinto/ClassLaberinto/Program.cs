namespace ClassLaberinto
{
using System;

class Laberinto
{
    public int sizeX = 25;  
    public int sizeY = 25;  // Tamaño de la matriz 
    // Tamaño de la matriz 
    public int[,] tablero;
	private Random rand = new Random();


     public void InicializarTablero()
    {   tablero = new int[sizeX,sizeY];
        for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeY; j++)
            {
                tablero[i, j] = 1;  // Asignamos muros
            }
        }
    }

    // Genera el laberinto utilizando el algoritmo de Prim
    public void GenerarLaberintoConPrim()
    {
        // Crear una lista de las celdas adyacentes a las celdas abiertas
        List<(int, int)> celdasAbiertas = new List<(int, int)>();
        int[] dx = { -1, 1, 0, 0 };  // Direcciones posibles: arriba, abajo, izquierda, derecha
        int[] dy = { 0, 0, -1, 1 };

        // Comenzamos desde la celda (1,1), que está garantizada para no estar en los bordes
        int inicioX = 1, inicioY = 1;
        tablero[inicioX, inicioY] = 0;  // Iniciamos la primera celda como camino
        celdasAbiertas.Add((inicioX, inicioY));

        while (celdasAbiertas.Count > 0)
        {
            // Elegimos una celda aleatoria de las celdas abiertas
            var (x, y) = celdasAbiertas[rand.Next(celdasAbiertas.Count)];

            // Intentamos crear un camino en una dirección aleatoria
            List<(int, int)> celdasAdyacentes = new List<(int, int)>();
            for (int i = 0; i < 4; i++)
            {
                int nuevoX = x + dx[i] * 2;
                int nuevoY = y + dy[i] * 2;

                // Aseguramos que la nueva celda esté dentro de los límites y que esté bloqueada
                if (EsValido(nuevoX, nuevoY) && tablero[nuevoX, nuevoY] == 1)
                {
                    celdasAdyacentes.Add((nuevoX, nuevoY));
                }
            }

            // Si encontramos una celda adyacente válida, la conectamos
            if (celdasAdyacentes.Count > 0)
            {
                var (nx, ny) = celdasAdyacentes[rand.Next(celdasAdyacentes.Count)];
                tablero[nx, ny] = 0;  // Hacemos un camino
                tablero[(x + nx) / 2, (y + ny) / 2] = 0;  // Hacemos un camino entre las celdas
                celdasAbiertas.Add((nx, ny));
            }

            // Si no podemos expandir más, eliminamos la celda de la lista de celdas abiertas
            else
            {
                celdasAbiertas.Remove((x, y));
            }

        }

        tablero[0,10]=0; //izquierda
        tablero[24,10]=0; //derecha 
        tablero[12,24]=0; //abajo
        tablero[12,0]=0; //ariba

        tablero[1,10]=0; //izquierda
        tablero[23,10]=0; //derecha 
        tablero[12,23]=0; //abajo
        tablero[12,1]=0; //arriba
        
    }

    // Verificar si una posición está dentro de los límites del tablero
    private bool EsValido(int x, int y)
    {
        return x >= 0 && x < sizeX && y >= 0 && y < sizeY;
    }


    
}
}