using Godot;
using System.Collections.Generic;

public partial class HUD_Player : CanvasLayer
{
    private PlayerBase player; // Referencia al jugador asociado
    private Label KeyCounter;
    private List<TextureRect> hearts; // Lista de corazones del HUD
    
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
    }

    public void SetPlayer(PlayerBase player)
    {
        this.player = player;
        if (player != null)
        {
            // Conecta las señales del jugador al HUD
            player.Connect("HealthChanged", new Callable(this, nameof(OnHealthChanged)));
            player.Connect("KeysChanged", new Callable(this, nameof(OnKeysChanged)));

            // Inicializa el HUD con los valores actuales del jugador
            UpdateHearts(player.Health);
        }
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

    private void OnKeysChanged(int keys)
    {
        KeyCounter.Text = $"Llaves: {keys}"; // Actualiza el contador en el HUD
    }

    
    
}
