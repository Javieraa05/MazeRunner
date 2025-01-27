using Godot;
using System;
using System.Collections.Generic;

public abstract partial class PlayerBase : CharacterBody2D
{
    public int Velocidad { get; set; } = 100;
    public int VidaMaxima { get; set; } = 6;
    public int Vida { get; set; } = 6;
    public HabilidadBase habilidadActual;
    public int SeleccionPersonaje1 = 1;
    public int SeleccionPersonaje2 = 2;
    public int CantidadLlaves=0;
    protected PackedScene characterScene;
    protected AnimatedSprite2D animatedSprite;
    public bool Escudo = false;
    public Vector2 Zoom = new Vector2(5,5);
    public List<LLaves> llaves = new List<LLaves>();
    public Area2D areaAtaque;
    public bool botonAtaque=false;
    public int Experiencia=0;
    public float TiempoEnfriamientoAtaque = 2;
    public bool PuedeAtacar = true;
    
    [Signal] public delegate void HealthChangedEventHandler(int health);
    [Signal] public delegate void KeysChangedEventHandler(LLaves llave);
    [Signal] public delegate void ActivarHabilidadEventHandler(int cuentaRegresiva);
    [Signal] public delegate void ExperienciaCambioEventHandler(int experiencia);
    [Signal] public delegate void NoticiaEventHandler(string noticia);

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
        EmitSignal(nameof(KeysChanged), CantidadLlaves);
        EmitSignal(nameof(ExperienciaCambio), Experiencia);


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
        Velocity = input * Velocidad;
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
        GD.Print($"Salud restante: {Vida}");
        EmitSignal(nameof(HealthChanged), Vida); // Emitir señal para actualizar el HUD

        if (Vida <= 0)
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
        Experiencia = 0;
        EmitSignal(nameof(KeysChanged), CantidadLlaves);
        EmitSignal(nameof(ExperienciaCambio), Experiencia);
        foreach(LLaves llave in llaves)
        {
            llave.Aparecer();
        }
        llaves.Clear();
        AjustarSalud(7);
    }
    public void EmitirNoticia(string noticia)
    {
        EmitSignal(nameof(Noticia), noticia);
    }
    public void AsignarHabilidad(HabilidadBase habilidad)
    {   
        habilidadActual = habilidad;
    }

    public abstract void UsarHabilidad();
    

    public void AjustarSalud(int cantidad)
    {
        Vida = Mathf.Clamp(Vida + cantidad, 0, VidaMaxima); // Asegura que la salud esté dentro de los límites
        EmitSignal(nameof(HealthChanged), Vida);    // Emite la señal para actualizar el HUD
        GD.Print($"La salud del jugador ahora es: {Vida}");
    }
   
   public void ReducirVelocidad()
   {
    Velocidad /= 4;
    GetTree().CreateTimer(2).Timeout += () => Velocidad*=4;
   }

   public void RecogerExperiencia()
   {
    Experiencia++;
    EmitSignal(nameof(ExperienciaCambio), Experiencia);
    GD.Print("Experiencia: " + Experiencia);
   }


}
