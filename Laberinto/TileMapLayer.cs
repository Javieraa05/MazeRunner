using Godot;
using System;
using System.Collections.Generic;


public partial class TileMapLayer : Godot.TileMapLayer
{
    public int cantidadTrampas = 1;
    Vector2I coordenada_tile = new Vector2I(0,0);
    Vector2I position_map = new Vector2I(0,0);
    int tile_set_id = 4;
    Laberinto laberinto = new Laberinto(); 
    private PackedScene _trapScene1; // Trampa instanciable
    private PackedScene _trapScene2; // Trampa instanciable
    private PackedScene _trapScene3; // Trampa instanciable
    public PackedScene _keyScene;

    public override void _Ready()
    {
        laberinto.InicializarTablero();
        laberinto.GenerarLaberintoConPrim();
        laberinto.ColocarTrampas(cantidadTrampas, TipoTrampa.Puas);
        laberinto.ColocarTrampas(cantidadTrampas, TipoTrampa.Fuego);
        laberinto.ColocarTrampas(cantidadTrampas, TipoTrampa.Osos);
        laberinto.ColocarLlave();
        _trapScene1 = GD.Load<PackedScene>("res://Traps/TrampaFuego.tscn");
        _trapScene2 = GD.Load<PackedScene>("res://Traps/TrampaPuas.tscn");
        _trapScene3 = GD.Load<PackedScene>("res://Traps/TrampaOso.tscn");
        
        Mostrar();
    }

    public override void _Process(double delta)
    {
    }

    public virtual void Mostrar()
    {
        for (int i = 0; i < laberinto.sizeX; i++)
        {
            for (int j = 0; j < laberinto.sizeY; j++)
            {
                Tile tileData = laberinto.tablero[i, j];
                if (!tileData.EsCamino) 
                {
                    coordenada_tile.X = 2;
                    coordenada_tile.Y = 1;

                    position_map.X = i;
                    position_map.Y = j;

                    tile_set_id = 2;
                    
                    SetCell(position_map, tile_set_id, coordenada_tile);
                    
                }
                else 
                {   

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
                    if(tileData.TieneLlave)
                    {
                        
                        var llaveInstance = (LLaves)_keyScene.Instantiate();
                        llaveInstance.Position = MapToLocal(new Vector2I(i, j));
                        AddChild(llaveInstance);
                    }
                }
            }
        }
    }
}
