using Godot;
using System;
using System.Diagnostics;

public partial class Player_1 : CharacterBody2D
{
	[Export] public  int Speed { get; set; } = 120;
    [Export] public int Health { get; set; } = 6;
    public static int P = 1; 
    
    public static int CantidadLlaves;
	PackedScene characterScene;
	AnimatedSprite2D animatedSprite;
	
	
	public override void _Ready()
	{
		characterScene = GD.Load<PackedScene>($"res://Players/Personaje{P}/personaje_{P}.tscn");
		if (characterScene == null)
        {
            GD.PrintErr("No se pudo cargar la escena.");
        }
        else
        {
            // Instancia la escena
            AnimatedSprite2D characterInstance = (AnimatedSprite2D)characterScene.Instantiate();
            // Agrega la instancia al nodo padre actual (este Node2D)
            AddChild(characterInstance);

            // Instancia del personaje
            animatedSprite = characterInstance;
        }
        PosicionInicial();
	}

    public override void _Process(double delta)
    {
        Vector2 input = Input.GetVector("izquierda2", "derecha2", "arriba2", "abajo2");
        Move(input);  
        Animate(input); 
        
    }

    public virtual void Move(Vector2 input)
    {
        Velocity = input * Speed;
        MoveAndSlide();
    }

    public virtual void Animate(Vector2 direction)
    {
        if (GetNode<AnimatedSprite2D>("AnimatedSprite2D") is AnimatedSprite2D sprite)
        {
            if (direction.Length() > 0)
            {
                if (direction.X > 0) sprite.Play("corre_derecha");
                else if (direction.X < 0) sprite.Play("corre_izquierda");
                else if (direction.Y > 0) sprite.Play("corre_abajo");
                else sprite.Play("corre_arriba");
            }
            else
            {
                sprite.Play("Idle");
            }
        }
    }
     
    public static void RecogerLlave()
    {
       CantidadLlaves++;
        GD.Print("Llaves: " + CantidadLlaves);  
    }
    private void PosicionInicial()
    {
        Position = new Vector2(205,1877);
    }
  
    
}
