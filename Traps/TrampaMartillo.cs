using Godot;
using System;

public partial class TrampaMartillo : RigidBody2D
{
	AnimatedSprite2D animatedSprite2D;
	AnimationPlayer animationPlayer;
	Area2D area2D;
	public override void _Ready()
	{
		animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		area2D = GetNode<Area2D>("Area2D");
		animatedSprite2D.Play("default");
		animationPlayer.Play("Collision");
		
		area2D.BodyEntered += OnBodyEntered;
	}


	public void OnBodyEntered(Node body)
	{
		if(body is PlayerBase player)
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
