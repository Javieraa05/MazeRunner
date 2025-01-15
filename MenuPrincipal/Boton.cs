using Godot;
using System;

public partial class Boton : Button
{
	private AudioStreamPlayer audioPlayer;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		audioPlayer = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
		this.Pressed += IsPressed;
	}
	public void IsPressed()
	{
		audioPlayer.Play();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
