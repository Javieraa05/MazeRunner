using Godot;
using System;

public partial class SeleccionPersonajes : Control
{
    private int _personajeActual = 0; // Índice del personaje actual
    private string[] _personajes = {
        "Personaje1", "Personaje2", "Personaje3", "Personaje4", 
        "Personaje5"
    };
    private bool _esTurnoPlayer1 = true; // Controla el turno de selección
    private TextureRect _imagenPersonaje;
    private Label _etiquetaJugador;
    private int _seleccionPlayer1;
    private int _seleccionPlayer2;

    public override void _Ready()
    {
        _imagenPersonaje = GetNode<TextureRect>("TextureRect");
        _etiquetaJugador = GetNode<Label>("Label");

        ActualizarVista();
        
        GetNode<Button>("BotonAnterior").Pressed += OnBotonAnteriorPressed;
        GetNode<Button>("BotonSiguiente").Pressed += OnBotonSiguientePressed;
        GetNode<Button>("BotonSeleccionar").Pressed += OnBotonSeleccionarPressed;
    }

    private void OnBotonAnteriorPressed()
    {
        _personajeActual = (_personajeActual - 1 + _personajes.Length) % _personajes.Length;
        ActualizarVista();
    }

    private void OnBotonSiguientePressed()
    {
        _personajeActual = (_personajeActual + 1) % _personajes.Length;
        ActualizarVista();
    }

    private void OnBotonSeleccionarPressed()
    {
      if (_esTurnoPlayer1)
    {
        // Guarda la selección del Player 1
        _seleccionPlayer1 = _personajeActual + 1;
        GD.Print($"Player 1 seleccionó: {_seleccionPlayer1}");
        _esTurnoPlayer1 = false;
        _etiquetaJugador.Text = "Player 2";
    }
    else
    {
        // Guarda la selección del Player 2
        _seleccionPlayer2 = _personajeActual + 1;
        GD.Print($"Player 2 seleccionó: {_seleccionPlayer2}");

        // Carga la escena Main
        var mainScene = GD.Load<PackedScene>("res://Main/main.tscn");
        if (mainScene != null)
        {
            var mainInstance = mainScene.Instantiate();

            // Asigna los personajes seleccionados a los jugadores
            if (mainInstance is Node mainNode)
            {
                var player1Node = mainNode.GetNodeOrNull<Player_1>("Viewports/ViewportContainer1/Viewport1/World/Player_1");
                if (player1Node != null)
                {
                    player1Node.SelectedCharacter1 = _seleccionPlayer1;
                }

                var player2Node = mainNode.GetNodeOrNull<Player_2>("Viewports/ViewportContainer1/Viewport1/World/Player_2");
                if (player2Node != null)
                {
                    player2Node.SelectedCharacter2 = _seleccionPlayer2;
                }
            }

            // Cambia la escena activa a Main
            GetTree().Root.AddChild(mainInstance);
            GetTree().Root.RemoveChild(this);
            QueueFree(); // Libera la escena de selección de personajes
        }
    }
    }

    private void ActualizarVista()
    {
        _imagenPersonaje.Texture = (Texture2D)GD.Load<Texture>($"res://Imagenes/Personajes/{_personajes[_personajeActual]}.png");


    	_etiquetaJugador.Text = _esTurnoPlayer1 ? "Player 1" : "Player 2";
    }
}

