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
        if (body is Player_1)
        {
            GD.Print("¡Jugador 1 gana!");
            MostrarPantallaVictoria("¡Jugador 1 ha ganado!");
        }
        else if (body is Player_2)
        {
            GD.Print("¡Jugador 2 gana!");
            MostrarPantallaVictoria("¡Jugador 2 ha ganado!");
        }
    }

    private void MostrarPantallaVictoria(string mensaje)
    {
        // Pausa el juego y muestra el mensaje de victoria
        GetTree().Paused = true;
        GD.Print(mensaje); // Esto puede reemplazarse con un popup o una escena de victoria
		GetTree().Quit(); // Cierra el juego
    }
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
