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
	private PackedScene _trapScene1; // Trampa instanciable
	private PackedScene _trapScene2; // Trampa instanciable
	private PackedScene _trapScene3; // Trampa instanciable

	public override void _Ready()
	{
		 laberinto.InicializarTablero();
		 laberinto.GenerarLaberintoConPrim();
		 laberinto.ColocarTrampas(3,3);
		 laberinto.ColocarTrampas(3,4);
		 laberinto.ColocarTrampas(3,5);
		 _trapScene1 = GD.Load<PackedScene>("res://Traps/Trap1.tscn");
		 _trapScene2 = GD.Load<PackedScene>("res://Traps/Trap2.tscn");
		 _trapScene3 = GD.Load<PackedScene>("res://Traps/Trap3.tscn");
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
                    var trapInstance = (Trap1)_trapScene1.Instantiate();
                    trapInstance.Position = MapToLocal(new Vector2I(i, j));
                    AddChild(trapInstance);
				}
				if(laberinto.tablero[i,j] == 4)
				{
				   tile.X=1;
				   position.X = i;
				   position.Y = j;
				   SetCell(position,tile_id,tile);
				   // Instanciar y colocar una trampa
                   var trapInstance = (Trap2)_trapScene2.Instantiate();
                   trapInstance.Position = MapToLocal(new Vector2I(i, j));
                   AddChild(trapInstance);
				}
				if(laberinto.tablero[i,j] == 5)
				{
				   tile.X=1;
				   position.X = i;
				   position.Y = j;
				   SetCell(position,tile_id,tile);
				   // Instanciar y colocar una trampa
                   var trapInstance = (Trap3)_trapScene3.Instantiate();
                   trapInstance.Position = MapToLocal(new Vector2I(i, j));
                   AddChild(trapInstance);
				}
            }

        }
    }

   
   }
