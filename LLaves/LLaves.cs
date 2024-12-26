using Godot;
using System;

public partial class LLaves : Area2D
{
	AnimatedSprite2D animatedSprite2D;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		animatedSprite2D.Play("default");	
		BodyEntered += OnBodyEntered;
	}

	protected virtual void OnBodyEntered(Node body)
	{
		if ((body is Player) && Player.CantidadLlaves<=2)
		{
			Player.CantidadLlaves++;
			QueueFree();
		}
		if (body is Player2 && Player2.CantidadLlaves<=2)
		{
			Player2.CantidadLlaves++;
			QueueFree();
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
