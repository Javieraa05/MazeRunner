using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export] public int Speed { get; set; } = 120;
    [Export] public int Health = 100;
    public float ActivationCooldown = 2.0f;
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

    public void TakeDamage(int amount)
    {
        Health -= amount;
        Speed /= 4; // Reduce speed when damaged
        
        // Inicia el cooldown
            var cooldownTimer = new Godot.Timer();
            cooldownTimer.WaitTime = ActivationCooldown;
            cooldownTimer.OneShot = true;
            cooldownTimer.Timeout += ResetSpeed; // Godot 4 utiliza eventos
            AddChild(cooldownTimer);
            cooldownTimer.Start();
    }
    public void ResetSpeed() 
    {
        Speed *= 4; // Restaura la velocidad
         if (Health <= 0)
        {
            QueueFree();
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
        SetInitialPosition(new Vector2(10, 1580));
    }

	
}
