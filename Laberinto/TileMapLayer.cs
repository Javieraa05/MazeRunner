using Godot;
using System;
using System.Collections.Generic;


public partial class TileMapLayer : Godot.TileMapLayer
{
    [Export] public int cantidadTrampas = 3;
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
        laberinto.ColocarTrampas(cantidadTrampas, TipoTrampa.Puas);
        laberinto.ColocarTrampas(cantidadTrampas, TipoTrampa.Fuego);
        laberinto.ColocarTrampas(cantidadTrampas, TipoTrampa.Osos);
        _trapScene1 = GD.Load<PackedScene>("res://Traps/TrampaFuego.tscn");
        _trapScene2 = GD.Load<PackedScene>("res://Traps/TrampaPuas.tscn");
        _trapScene3 = GD.Load<PackedScene>("res://Traps/TrampaOso.tscn");
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
                Tile tileData = laberinto.tablero[i, j];
                if (!tileData.EsCamino) 
                {
                    tile.X = 0;
                    position.X = i;
                    position.Y = j;
                    SetCell(position, tile_id, tile);
                }
                else 
                {
                    tile.X = 1;
                    position.X = i;
                    position.Y = j;
                    SetCell(position, tile_id, tile);

                    if (tileData.Trampa == TipoTrampa.Puas)
                    {
                        var trapInstance = (TrampaFuego)_trapScene1.Instantiate();
                        trapInstance.Position = MapToLocal(new Vector2I(i, j));
                        AddChild(trapInstance);
                    }
                    else if (tileData.Trampa == TipoTrampa.Fuego)
                    {
                        var trapInstance = (TrampaPuas)_trapScene2.Instantiate();
                        trapInstance.Position = MapToLocal(new Vector2I(i, j));
                        AddChild(trapInstance);
                    }
                    else if (tileData.Trampa == TipoTrampa.Osos)
                    {
                        var trapInstance = (TrampaOso)_trapScene3.Instantiate();
                        trapInstance.Position = MapToLocal(new Vector2I(i, j));
                        AddChild(trapInstance);
                    }
                }

               /* if (tileData.TieneLlave)
                {
                    // Aquí podrías instanciar una llave si fuera necesario.
                }*/
            }
        }
    }
}
