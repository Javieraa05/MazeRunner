using Godot;
using System.Collections.Generic;

public partial class HUD_Player : CanvasLayer
{
    private PlayerBase jugador; // Referencia al jugador asociado
    private Label ContadorLlaves;
    private List<TextureRect> corazones; // Lista de corazones del HUD
    private TextureRect imagenHabilidad; 
    private Label textoHabilidad;
    private Label ExperienciaContador;
    private Label atacarLabel;
    private Label informacionLabel;
    private string teclaHabilidad;   
    public override void _Ready()
    {
        
        // Inicializa los corazones del HUD
        corazones = new List<TextureRect>();
        foreach (Node child in GetNode("PanelContainer/MarginContainer/VBoxContainer/HeartsContainer").GetChildren())
        {
            if (child is TextureRect textureRect)
            {
                corazones.Add(textureRect);
            }
        }

        ContadorLlaves = GetNodeOrNull<Label>("PanelContainer/MarginContainer/VBoxContainer/ExperienciaContainer/KeyCounter");
        ExperienciaContador = GetNode<Label>("PanelContainer/MarginContainer/VBoxContainer/ExperienciaContainer/ExperienciaLabel");
        imagenHabilidad = GetNode<TextureRect>("HabilidadContainer/IconoHabilidad");
        textoHabilidad = GetNode<Label>("HabilidadContainer/TeclaYCooldown");
        atacarLabel =  GetNode<Label>("AtacarLabel");
        informacionLabel = GetNode<Label>("InformacionLabel");
        
    }

    public void SeleccionarJugador(PlayerBase jugador)
    {
        this.jugador = jugador;
        if (jugador != null)
        {
            // Conecta las señales del jugador al HUD
            jugador.Connect("HealthChanged", new Callable(this, nameof(CorazonesCambian)));
            jugador.Connect("KeysChanged", new Callable(this, nameof(LlavesCambian)));
            jugador.Connect("ActivarHabilidad", new Callable(this, nameof(SeActivaHabilidad)));
            jugador.Connect("ExperienciaCambio", new Callable(this, nameof(ExperienciaCambia)));
            jugador.Connect("Noticia", new Callable(this, nameof(Informar)));


            // Inicializa el HUD con los valores actuales del jugador
            ActualizarCorazones(jugador.Health);
        }
    }
    private void SeActivaHabilidad(float cuentaRegresiva)
    {
            // Inicia la cuenta regresiva
        float tiempoRestante = cuentaRegresiva;

        // Usa un temporizador para actualizar el texto de la habilidad cada segundo
        Timer timer = new Timer();
        AddChild(timer);
        timer.WaitTime = 1.0f; // Cada segundo
        timer.OneShot = false; // Se repite hasta que llegue a 0
        timer.Start();

        textoHabilidad.Text = $"{tiempoRestante:F0}"; // Mostrar los segundos restantes

        timer.Timeout += () =>
        {
            tiempoRestante -= 1.0f;
            if (tiempoRestante > 0)
            {
                textoHabilidad.Text = $"{tiempoRestante:F0}"; // Actualizar la cuenta regresiva
            }
            else
            {
                textoHabilidad.Text = teclaHabilidad; // Mostrar la tecla de habilidad al terminar
                timer.QueueFree(); // Eliminar el temporizador
            }
        };
    }

    private void CorazonesCambian(int health)
    {
        ActualizarCorazones(health);
    }

    private void ActualizarCorazones(int health)
    {
        for (int i = 0; i < corazones.Count; i++)
        {
            corazones[i].Texture = i < health
                ? GD.Load<Texture2D>("res://Imagenes/Hearts/full.png")
                : GD.Load<Texture2D>("res://Imagenes/Hearts/empty.png");
        }
    }

    private void LlavesCambian(int CantidadLlaves)
    {
         // Actualiza el contador en el HUD
        ContadorLlaves.Text = $"{CantidadLlaves}/3";
    }
    public void ImagenHabilidad(Texture texture)
    {
        imagenHabilidad.Texture = (Texture2D)texture;
    }
     
     public void TeclaHabilidad(string tecla)
    {
        teclaHabilidad = tecla;
        textoHabilidad.Text = teclaHabilidad;
    }

    public void ExperienciaCambia(int experiencia)
    {
        ExperienciaContador.Text = experiencia.ToString();
    }
    
     public override void _Process(double delta)
     {
        if(jugador.botonAtaque)
        {
            string tecla; 
            if(teclaHabilidad == "E") tecla = "R";
            else tecla = "N";
            atacarLabel.Text = $"presiona -{tecla}- para atacar";
        }
        else atacarLabel.Text = "";
     }

    public void Informar(string noticia)
    {
        informacionLabel.Text = noticia;
        GetTree().CreateTimer(5).Timeout += () => informacionLabel.Text = "";
       
    }
    
    
}
