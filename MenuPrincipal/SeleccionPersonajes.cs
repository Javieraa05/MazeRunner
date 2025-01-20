using Godot;
using System;
using System.Collections.Generic;

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
    private Label _etiquetaHistoria;
    private Dictionary <int, Personaje> personajes = new Dictionary<int, Personaje>();


    public override void _Ready()
    {
       
         // Inicializar los personajes
        personajes.Add(1, new Personaje(
            "Miguel\n", 
            "Curación\n", 
            "Conocido en su barrio como el “doctor de los remedios caseros”, \n" +
            "se hizo famoso porque curó al vecino con una mezcla de miel, limón y “un toquecito” de ron. \n" +
            "Cuando el ciclón dejó sin médicos al pueblo, él tomó la batuta, aplicando desde sueros improvisados \n" +
            "con tubos de refresco hasta 'masajes de la vida' que revivían hasta al más agotado. \n" +
            "Ahora, recorre el laberinto con su botiquín que incluye una botella de Habana Club por si acaso... \n" +
            "ya sabes, para “desinfectar heridas”."

        ));
        personajes.Add(2, new Personaje(
            "Yadira\n", 
            "Sprint\n", 
            "Campeona del Marabana tres años seguidos, siempre decía que su secreto era\n" +
            "'correr detrás de la guagua cuando no hay vuelto'.\n" +
            "Una vez, en pleno apagón, corrió tan rápido para alcanzar la última croqueta en la cafetería\n" +
            "que hasta el vendedor quedó asustado.\n" +
            "Ahora, en el laberinto, Yadira usa su velocidad para esquivar trampas y llegar primera a las cajas de comida…\n" +
            "porque “el que no corre, vuela”."


        ));
        personajes.Add(3, new Personaje(
            "Maritza\n", 
            "Escudo\n", 
            "Era herrera en un taller de mecánica en Centro Habana.\n" +
            "Un día, tras un apagón que dejó la ciudad sin hornos, creó un escudo indestructible\n" +
            "fundiendo pedazos de lavadoras soviéticas y tapas de olla de presión.\n" +
            "En su barrio, siempre la llamaban para proteger las mesas de dominó cuando alguien se ponía “muy caliente”.\n" +
            "Ahora, en el laberinto, su escudo es legendario y siempre dice:\n" +
            "“¡Aquí no pasa ni el panadero con el carrito!”."

        ));
        personajes.Add(4, new Personaje(
            "Ernesto\n", 
            "Revelación de Mapa\n", 
            "Ernesto trabajaba como guía turístico en Viñales, conocido por llevar a los extranjeros\n" +
            "a 'rutas alternativas' donde el GPS no funcionaba.\n" +
            "Con un ojo para los atajos y una habilidad innata para leer mapas del tiempo de los españoles,\n" +
            "siempre decía: 'Si yo me pierdo, ¡es porque el camino cambió de lugar!'.\n" +
            "Ahora, en el laberinto, su habilidad para descubrir pasillos secretos lo convirtió en el tipo que todos siguen,\n" +
            "especialmente cuando hay comida de por medio."

        ));
        personajes.Add(5, new Personaje(
            "Manolo\n", 
            "Intercambio de Posición\n", 
            "Manolo era un maestro del truco y la improvisación, conocido en el malecón\n" +
            "por su habilidad de cambiar su lugar en la cola del pollo con una charla rápida y un par de gestos mágicos.\n" +
            "Una vez, logró colarse en una cola de dos cuadras con el famoso\n" +
            "‘oye, ¿tú eres el primo de fulano?’. \n" +
            "Ahora, en el laberinto, usa su don para cambiar posiciones estratégicas,\n" +
            "aunque siempre dice con una sonrisa: 'El que no inventa, no avanza'."

        ));
       
        _imagenPersonaje = GetNode<TextureRect>("VContainer/HContainer/ContainerImagen/TextureRect");
        _etiquetaJugador = GetNode<Label>("VContainer/LabelPlayer");
        _etiquetaHistoria = GetNode<Label>("VContainer/PanelContainer/MarginContainer/LabelHistoria");

        ActualizarVista();
        
        GetNode<Button>("VContainer/HContainer/ContainerBoton1/BotonAnterior").Pressed += OnBotonAnteriorPressed;
        GetNode<Button>("VContainer/HContainer/ContainerBoton2/BotonSiguiente").Pressed += OnBotonSiguientePressed;
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

        _etiquetaHistoria.Text = GetHistoriaPersonaje(_personajeActual+1);
    }

    // Método para mostrar la historia de un personaje seleccionado
    public string GetHistoriaPersonaje(int id)
    {
        if (personajes.ContainsKey(id))
        {
            return $"Nombre: {personajes[id].Nombre}\n" +
                   $"Habilidad: {personajes[id].Habilidad}\n" +
                   $"Historia: {personajes[id].Historia}";
        }
        return "Personaje no encontrado.";
    }
}


  public class Personaje
    {
        public string Nombre { get; set; }
        public string Habilidad { get; set; }
        public string Historia { get; set; }

        public Personaje(string nombre, string habilidad, string historia)
        {
            Nombre = nombre;
            Habilidad = habilidad;
            Historia = historia;
        }
    }
