using Godot;
using System.Collections.Generic;

public partial class HUD_Player : CanvasLayer
{
    private PlayerBase player; // Referencia al jugador asociado
    private Label KeyCounter;
    private List<TextureRect> hearts; // Lista de corazones del HUD
    private TextureRect imagenHabilidad; 
    private Label textoHabilidad;
    private Label ExperienciaContador;
    private Label atacarLabel;
    private string teclaHabilidad;   
    public override void _Ready()
    {
        
        // Inicializa los corazones del HUD
        hearts = new List<TextureRect>();
        foreach (Node child in GetNode("HeartsContainer").GetChildren())
        {
            if (child is TextureRect textureRect)
            {
                hearts.Add(textureRect);
            }
        }

        KeyCounter = GetNodeOrNull<Label>("KeyCounter");
        ExperienciaContador = GetNode<Label>("ExperienciaContainer/ExperienciaLabel");
        imagenHabilidad = GetNode<TextureRect>("HabilidadContainer/IconoHabilidad");
        textoHabilidad = GetNode<Label>("HabilidadContainer/TeclaYCooldown");
        atacarLabel =  GetNode<Label>("AtacarLabel");
        
    }

    public void SetPlayer(PlayerBase player)
    {
        this.player = player;
        if (player != null)
        {
            // Conecta las señales del jugador al HUD
            player.Connect("HealthChanged", new Callable(this, nameof(OnHealthChanged)));
            player.Connect("KeysChanged", new Callable(this, nameof(OnKeysChanged)));
            player.Connect("ActivarHabilidad", new Callable(this, nameof(SeActivaHabilidad)));
            player.Connect("ExperienciaCambio", new Callable(this, nameof(SeCambioExperiencia)));


            // Inicializa el HUD con los valores actuales del jugador
            UpdateHearts(player.Health);
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

    private void OnHealthChanged(int health)
    {
        UpdateHearts(health);
    }

    private void UpdateHearts(int health)
    {
        for (int i = 0; i < hearts.Count; i++)
        {
            hearts[i].Texture = i < health
                ? GD.Load<Texture2D>("res://Imagenes/Hearts/full.png")
                : GD.Load<Texture2D>("res://Imagenes/Hearts/empty.png");
        }
    }

    private void OnKeysChanged(int CantidadLlaves)
    {
         // Actualiza el contador en el HUD
        KeyCounter.Text = $"{CantidadLlaves}";
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

    public void SeCambioExperiencia(int experiencia)
    {
        ExperienciaContador.Text = experiencia.ToString();
    }
    
     public override void _Process(double delta)
     {
        if(player.botonAtaque)
        {
            string tecla; 
            if(teclaHabilidad == "E") tecla = "R";
            else tecla = "N";
            atacarLabel.Text = $"presiona -{tecla}- para atacar";
        }
        else atacarLabel.Text = "";
     }

    
    
}
