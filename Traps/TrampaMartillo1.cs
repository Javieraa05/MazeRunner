using Godot;
using System;

public partial class TrampaMartillo1 : RigidBody2D
{
	AnimatedSprite2D animatedSprite2D;
	AnimationPlayer animationPlayer;

	public override void _Ready()
	{
		animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		
		animatedSprite2D.Play("default");
		animationPlayer.Play("Collision");
		
		
	}


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
