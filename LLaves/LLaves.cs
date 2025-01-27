using Godot;
using System;

public partial class LLaves : Area2D
{
	AnimatedSprite2D animatedSprite2D;
	AudioStreamPlayer audioStreamPlayer;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		audioStreamPlayer = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
		animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		animatedSprite2D.Play("default");	
		BodyEntered += OnBodyEntered;
	}

	protected virtual void OnBodyEntered(Node body)
	{
		if ((body is PlayerBase player) && player.CantidadLlaves<=2 && Visible==true)
		{
			player.RecogerLlave(this);
			audioStreamPlayer.Play();
		}
	}
	public void Desaparecer()
	{
		Visible=false;
	}
	public void Aparecer()
	{
		Visible=true;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
