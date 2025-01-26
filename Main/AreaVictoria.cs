using Godot;
using System;

public partial class AreaVictoria : Area2D
{
    
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		BodyEntered += OnBodyEntered;
	}

	 private void OnBodyEntered(Node body)
    {
        if (body is Player_1 player1)
        {
            GD.Print("¡Jugador 1 gana!");
            
            GetTree().Paused = true;
            player1.EmitirNoticia("Haz Ganado la Partida");
            player1.player2.EmitirNoticia("El jugador 1 ha ganado la Partida");
            GetTree().CreateTimer(6).Timeout += () => GetTree().Quit();

        }
        else if (body is Player_2 player2)
        {
            GD.Print("¡Jugador 2 gana!");

            GetTree().Paused = true;
            player2.EmitirNoticia("Haz Ganado la Partida");
            player2.player1.EmitirNoticia("El jugador 2 ha ganado la Partida");
            GetTree().CreateTimer(6).Timeout += () => GetTree().Quit();

            
        }
    }

   
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
