using Godot;
using System;

public abstract partial class PlayerBase : CharacterBody2D
{
    [Export] public int Speed { get; set; } = 120;
    [Export] public int Health { get; set; } = 6;
    public int SelectedCharacter1 { get; set; }
    public int SelectedCharacter2 { get; set; }
    public static int CantidadLlaves;
    protected PackedScene characterScene;
    protected AnimatedSprite2D animatedSprite;

    public override void _Ready()
    {
       
        
        if (characterScene == null)
        {
            GD.PrintErr("No se pudo cargar la escena.");
        }
        else
        {
            AnimatedSprite2D characterInstance = (AnimatedSprite2D)characterScene.Instantiate();
            AddChild(characterInstance);
            animatedSprite = characterInstance;
        }
        SetInitialPosition();
    }

    public override void _Process(double delta)
    {
        Vector2 input = GetInput();
        Move(input);
        Animate(input);
    }

    protected abstract Vector2 GetInput();

    protected virtual void Move(Vector2 input)
    {
        Velocity = input * Speed;
        MoveAndSlide();
    }

    protected virtual void Animate(Vector2 direction)
    {
        if (animatedSprite != null)
        {
            if (direction.Length() > 0)
            {
                if (direction.X > 0) animatedSprite.Play("corre_derecha");
                else if (direction.X < 0) animatedSprite.Play("corre_izquierda");
                else if (direction.Y > 0) animatedSprite.Play("corre_abajo");
                else animatedSprite.Play("corre_arriba");
            }
            else
            {
                animatedSprite.Play("Idle");
            }
        }
    }

    public static void RecogerLlave()
    {
        CantidadLlaves++;
        GD.Print($"Llaves: {CantidadLlaves}");
    }

    protected abstract void SetInitialPosition();
}
