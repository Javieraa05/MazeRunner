using Godot;
using System;

public partial class AreaMuerte : Area2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		BodyEntered += OnBodyEntered;
	}

	public void OnBodyEntered(Node body)
	{
		if (body is PlayerBase player)
		{
			player.ResetPosition();
			player.EmitirNoticia("Te ha asesinado un Martillo");
		}
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
