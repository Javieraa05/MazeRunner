using Godot;
using System;

public partial class Experiencia : Area2D
{
	AudioStreamPlayer audioStreamPlayer;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		audioStreamPlayer = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
		BodyEntered += OnAreaEntered;
	}

	public void OnAreaEntered(Node body)
	{
		if (body is PlayerBase player)
		{	
			audioStreamPlayer.Play();
			player.RecogerExperiencia();
			GetTree().CreateTimer(0.11).Timeout += () => QueueFree();
			
		}
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
