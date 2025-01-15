using Godot;
using System;
using System.Collections.Generic;

public abstract partial class PlayerBase : CharacterBody2D
{
    public int Speed { get; set; } = 100;
    public int MaxHealth { get; set; } = 6;

    public int Health { get; set; } = 6;
    public HabilidadBase habilidadActual;
    public int SelectedCharacter1 { get; set; }
    public int SelectedCharacter2 { get; set; }
    public int CantidadLlaves=0;
    protected PackedScene characterScene;
    protected AnimatedSprite2D animatedSprite;
    public bool Escudo = false;
    public Vector2 Zoom = new Vector2(5,5);
    public List<LLaves> llaves = new List<LLaves>();
    
    [Signal] public delegate void HealthChangedEventHandler(int health);

    [Signal] public delegate void KeysChangedEventHandler(LLaves llave);
    [Signal] public delegate void ActivarHabilidadEventHandler(int cuentaRegresiva);

    public static Dictionary<int, HabilidadBase> habilidadesPorPersonaje = new Dictionary<int, HabilidadBase>
    {
        { 1, new Curacion() },  
        { 2, new Sprint() },   
        { 3, new Escudo() },
        { 4, new RevelacionMapa() },
        { 5, new IntercambiaPosicion() },
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

    public void RecogerLlave(LLaves llave)
    {
        CantidadLlaves++;
        llave.Desaparecer();
        llaves.Add(llave);
        EmitSignal(nameof(KeysChanged), CantidadLlaves);
        GD.Print($"Llaves: {CantidadLlaves}");
    }
    public int GetCantidadLlaves()
    {
        return CantidadLlaves; // Método para obtener la cantidad actual de llaves
    }

    protected abstract void SetInitialPosition();

    public void TomarDano(int cantidadDano)
    {
        AjustarSalud(-cantidadDano);
        GD.Print($"Salud restante: {Health}");
        EmitSignal(nameof(HealthChanged), Health); // Emitir señal para actualizar el HUD

        if (Health <= 0)
        {
            GD.Print("¡Jugador eliminado! Restableciendo posición...");
            ResetPosition(); // Llama al método para restablecer la posición
        }
    }
    public void ResetPosition()
    {
        SetInitialPosition(); // Usa el método que ya define la posición inicial
        Velocity = Vector2.Zero; // Detiene el movimiento
        GD.Print("El jugador ha sido restablecido a la posición inicial.");
        CantidadLlaves = 0; 
        EmitSignal(nameof(KeysChanged), CantidadLlaves);
        foreach(LLaves llave in llaves)
        {
            llave.Aparecer();
        }
        llaves.Clear();
        AjustarSalud(7);
    }

    public void AsignarHabilidad(HabilidadBase habilidad)
    {   
        habilidadActual = habilidad;
    }

    public void UsarHabilidad()
    {
        if(habilidadActual == null) GD.Print("No hay habilidad asignada");
        if (habilidadActual != null && habilidadActual.Disponible)
        {
            GD.Print("Se mando a activar la habilidad");
            habilidadActual.Activar(this);
            EmitSignal(nameof(ActivarHabilidad), habilidadActual.Cooldown);
        }
        else
        {
            GD.Print("No se puede usar la habilidad");
        }
    }

    public void AjustarSalud(int cantidad)
    {
        Health = Mathf.Clamp(Health + cantidad, 0, 6); // Asegura que la salud esté dentro de los límites
        EmitSignal(nameof(HealthChanged), Health);    // Emite la señal para actualizar el HUD
        GD.Print($"La salud del jugador ahora es: {Health}");
    }
   
   public void ReducirVelocidad()
   {
    Speed /= 4;
    GetTree().CreateTimer(2).Timeout += () => Speed*=4;
   }


}
