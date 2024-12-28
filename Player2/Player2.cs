using Godot;
using System;

public partial class Player2 : CharacterBody2D
{
	[Export]
    public int Speed { get; set; } = 120;
    public static int CantidadLlaves = 0;

	AnimatedSprite2D animatedSprite2D;

     public void SetInitialPosition(Vector2 newPosition)
    {
        Position = newPosition;
    }

    public void GetInput()
    {

        Vector2 inputDirection = Input.GetVector("izquierda1", "derecha1", "arriba1", "abajo1");
        Velocity = inputDirection * Speed;
		
    }

	public void Animated()
	{
		if(Input.IsActionPressed("izquierda1"))
        {
            animatedSprite2D.Play("run_lefth");
        }
        else if(Input.IsActionPressed("derecha1"))
        {
            animatedSprite2D.Play("run_rigth");
        }
		else if(Input.IsActionPressed("arriba1"))
        {
            animatedSprite2D.Play("run_top");
        }
        else if(Input.IsActionPressed("abajo1"))
        {
            animatedSprite2D.Play("run_down");
        }
        else
        {
            animatedSprite2D.Play("Idle");
        }
	}

    public override void _PhysicsProcess(double delta)
    {
        GetInput();
		Animated();
        MoveAndSlide();

    }

	

	public override void _Ready()
    {
		animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        SetInitialPosition(new Vector2(1575, 1582));
    }

	
}
