using Godot;
using System;

public partial class SeleccionPersonajes : Control
{
    private int _personajeActual = 0; // Índice del personaje actual
    private string[] _personajes = {
        "Personaje1", "Personaje2", "Personaje3", "Personaje4", 
        "Personaje5", "Personaje6", "Personaje7", "Personaje8", 
        "Personaje9", "Personaje10"
    };
    private bool _esTurnoPlayer1 = true; // Controla el turno de selección
    private TextureRect _imagenPersonaje;
    private Label _etiquetaJugador;

    public override void _Ready()
    {
        _imagenPersonaje = GetNode<TextureRect>("TextureRect");
        _etiquetaJugador = GetNode<Label>("Label");

        ActualizarVista();
        
        GetNode<Button>("BoxContainer/BotonAnterior").Pressed += OnBotonAnteriorPressed;
        GetNode<Button>("BoxContainer/BotonSiguiente").Pressed += OnBotonSiguientePressed;
        GetNode<Button>("BoxContainer/BotonSeleccionar").Pressed += OnBotonSeleccionarPressed;
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
           // GetTree().Root.GetNode<Player_1>("Main/Viewports/ViewportContainer1/Viewport1/World/Player_1").P = _personajeActual + 1;
            Player_1.P = _personajeActual + 1;
			_esTurnoPlayer1 = false;
            _etiquetaJugador.Text = "Turno de Player 2";
        }
        else
        {
          //  GetTree().Root.GetNode<Player_2>("Main/Viewports/ViewportContainer1/Viewport1/World//Player_2").P = _personajeActual + 1;
		  Player_2.P = _personajeActual + 1;
            GetTree().ChangeSceneToFile("res://Main/main.tscn"); // Cambia a la escena principal
        }
        GD.Print($"Personaje {_personajes[_personajeActual]} seleccionado por {(_esTurnoPlayer1 ? "Player 1" : "Player 2")}");
    }

    private void ActualizarVista()
    {
        _imagenPersonaje.Texture = (Texture2D)GD.Load<Texture>($"res://Imagenes/Personajes/{_personajes[_personajeActual]}.png");


    	_etiquetaJugador.Text = _esTurnoPlayer1 ? "Turno de Player 1" : "Turno de Player 2";
    }
}
