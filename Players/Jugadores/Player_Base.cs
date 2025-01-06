using Godot;
using System;
using System.Collections.Generic;

public abstract partial class PlayerBase : CharacterBody2D
{
    [Export] public int Speed { get; set; } = 120;
    [Export] public int MaxHealth { get; set; } = 6;

    [Export] public int Health { get; set; } = 6;
    private HabilidadBase habilidadActual;
    public int SelectedCharacter1 { get; set; }
    public int SelectedCharacter2 { get; set; }
    public static int CantidadLlaves;
    protected PackedScene characterScene;
    protected AnimatedSprite2D animatedSprite;
    
    [Signal] public delegate void HealthChangedEventHandler(int health);

    [Signal] public delegate void KeysChangedEventHandler(int keys);

    public static Dictionary<int, HabilidadBase> habilidadesPorPersonaje = new Dictionary<int, HabilidadBase>
    {
        { 1, new Curacion() },  // Personaje 1 tiene Curación
        { 2, new Sprint() },    // Personaje 2 tiene Sprint
        // Personaje 4 tiene Radar de Llaves
    };


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
        //GD.Print($"Input: {input}, Velocity: {Velocity}");
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

    public void RecogerLlave()
    {
        CantidadLlaves++;
        EmitSignal(nameof(KeysChanged), CantidadLlaves);
        GD.Print($"Llaves: {CantidadLlaves}");
    }

    protected abstract void SetInitialPosition();

    public void TomarDano(int cantidadDano)
    {
        Health -= cantidadDano;
        GD.Print($"Salud restante: {Health}");
        EmitSignal(nameof(HealthChanged), Health); // Emitir señal para actualizar el HUD

        if (Health <= 0)
        {
            GD.Print("¡Jugador eliminado! Restableciendo posición...");
            Health = 6; // Restablece la salud al valor inicial
            ResetPosition(); // Llama al método para restablecer la posición
        }
    }
    public void ResetPosition()
    {
        SetInitialPosition(); // Usa el método que ya define la posición inicial
        Velocity = Vector2.Zero; // Detiene el movimiento
        GD.Print("El jugador ha sido restablecido a la posición inicial.");
    }

    public void ModificarSalud(int cantidad)
    {
        Health = Mathf.Clamp(Health + cantidad, 0, MaxHealth);
        GD.Print($"Salud actual: {Health}");
    }
    public void AsignarHabilidad(HabilidadBase habilidad)
    {   
        habilidadActual = habilidad;
    }

    public void UsarHabilidad()
    {
        if (habilidadActual != null && habilidadActual.Disponible)
        {
            GD.Print("Se mando a activar la habilidad");
            habilidadActual.Activar(this);
        }
    }

}
