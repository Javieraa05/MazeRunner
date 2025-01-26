using Godot;
using System;

public partial class Boton : Button
{
	private AudioStreamPlayer Audio;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Audio = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
		this.Pressed += onIsPressed;
	}
	public void onIsPressed()
	{
		Audio.Play();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
