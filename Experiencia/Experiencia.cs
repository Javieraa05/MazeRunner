using Godot;
using System;

public partial class Experiencia : Area2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		BodyEntered += OnAreaEntered;
	}

	public void OnAreaEntered(Node body)
	{
		if (body is PlayerBase player)
		{
			player.RecogerExperiencia();
			QueueFree();
		}
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
