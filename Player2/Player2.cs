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

        Vector2 inputDirection = Input.GetVector("izquierda2", "derecha2", "arriba2", "abajo2");
        Velocity = inputDirection * Speed;
		
    }

	public void Animated()
	{
		if(Input.IsActionPressed("izquierda2"))
        {
            animatedSprite2D.Play("run_lefth");
        }
        else if(Input.IsActionPressed("derecha2"))
        {
            animatedSprite2D.Play("run_rigth");
        }
		else if(Input.IsActionPressed("arriba2"))
        {
            animatedSprite2D.Play("run_top");
        }
        else if(Input.IsActionPressed("abajo2"))
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
