using Godot;
using System;

public partial class Sierra : Area2D
{
	AnimatedSprite2D animatedSprite2D;
	AnimationPlayer animationPlayer;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		

		animatedSprite2D.Play("default");
		animationPlayer.Play("new_animation");


		BodyEntered += OnBodyEntered;
		
	}

	private void OnBodyEntered(Node body)
	{
		if(body is PlayerBase player)
		{
			player.ResetPosition();
			player.EmitirNoticia("Te ha matado una Sierra");
		}
		else
		{
			GD.Print("no es un jugador");
		}
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
