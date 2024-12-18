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
	private PackedScene _trapScene; // Trampa instanciable

	public override void _Ready()
	{
		 laberinto.InicializarTablero();
		 laberinto.GenerarLaberintoConPrim();
		 _trapScene = GD.Load<PackedScene>("res://Traps/Trap1.tscn");
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
                if(laberinto.tablero[i,j] == 1) 
				{
					tile.X=0;
					position.X = i;
			        position.Y = j;
					SetCell(position,tile_id,tile);
				}
				if(laberinto.tablero[i,j] == 0)
				{
				tile.X=1;
				position.X = i;
			    position.Y = j;
				SetCell(position,tile_id,tile);
				}
				if(laberinto.tablero[i,j] == 3)
				{
					tile.X=1;
					position.X = i;
				    position.Y = j;
					SetCell(position,tile_id,tile);
					// Instanciar y colocar una trampa
                    var trapInstance = (proximityTrap)_trapScene.Instantiate();
                    trapInstance.Position = MapToLocal(new Vector2I(i, j));
                    AddChild(trapInstance);
				}
            }

        }
    }

   
   }
