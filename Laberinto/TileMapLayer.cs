using Godot;
using System;
using System.Collections.Generic;
using ClassLaberinto;

public partial class TileMapLayer : Godot.TileMapLayer
{
	Vector2I tile = new Vector2I(0,0);
    Vector2I position = new Vector2I(0,0);
	int tile_id = 1;
    Laberinto laberinto = new Laberinto();

	public override void _Ready()
	{
		 laberinto.InicializarTablero();
		 laberinto.GenerarLaberintoConPrim();
		 Mostrar();
		
			
	}

	
	public override void _Process(double delta)
	{
	}

    public void Mostrar()
    {
        for (int i = 0; i < laberinto.sizeX; i++)
        {
            for (int j = 0; j < laberinto.sizeY; j++)
            {
                if(laberinto.tablero[i,j] == 1) tile.X=0;
				else tile.X=1;
				position.X = i;
			    position.Y = j;
				SetCell(position,tile_id,tile);
				
            }

        }
    }

   
   }
