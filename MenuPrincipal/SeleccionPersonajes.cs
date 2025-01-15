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
            "Miguel", 
            "Curación", 
            "Miguel, conocido en su barrio como el “doctor de los remedios caseros”, se hizo famoso porque curó al vecino con una mezcla de miel, limón y “un toquecito” de ron. Cuando el ciclón dejó sin médicos al pueblo, él tomó la batuta, aplicando desde sueros improvisados con tubos de refresco hasta masajes de la vida que revivían hasta al más agotado. Ahora, recorre el laberinto con su botiquín que incluye una botella de Habana Club por si acaso... ya sabes, para “desinfectar heridas”."
        ));
        personajes.Add(2, new Personaje(
            "Yadira", 
            "Sprint", 
            "Yadira, campeona del Marabana tres años seguidos, siempre decía que su secreto era correr detrás de la guagua cuando no hay vuelto. Una vez, en pleno apagón, corrió tan rápido para alcanzar la última croqueta en la cafetería que hasta el vendedor quedó asustado. Ahora, en el laberinto, Yadira usa su velocidad para esquivar trampas y llegar primera a las cajas de comida… porque “el que no corre, vuela”."
        ));
        personajes.Add(3, new Personaje(
            "Maritza", 
            "Escudo", 
            "Maritza era herrera en un taller de mecánica en Centro Habana. Un día, tras un apagón que dejó la ciudad sin hornos, creó un escudo indestructible fundiendo pedazos de lavadoras soviéticas y tapas de olla de presión. En su barrio, siempre la llamaban para proteger las mesas de dominó cuando alguien se ponía “muy caliente”. Ahora, en el laberinto, su escudo es legendario y siempre dice: “¡Aquí no pasa ni el panadero con el carrito!"
        ));
        personajes.Add(4, new Personaje(
            "Ernesto", 
            "Revelación de Mapa", 
            "Ernesto trabajaba como guía turístico en Viñales, conocido por llevar a los extranjeros a “rutas alternativas” donde el GPS no funcionaba. Con un ojo para los atajos y una habilidad innata para leer mapas del tiempo de los españoles, siempre decía: “Si yo me pierdo, ¡es porque el camino cambió de lugar!”. Ahora, en el laberinto, su habilidad para descubrir pasillos secretos lo convirtió en el tipo que todos siguen, especialmente cuando hay comida de por medio."
        ));
        personajes.Add(5, new Personaje(
            "Manolo", 
            "Intercambio de Posición", 
            "Manolo era un maestro del truco y la improvisación, conocido en el malecón por su habilidad de cambiar su lugar en la cola del pollo con una charla rápida y un par de gestos mágicos. Una vez, logró colarse en una cola de dos cuadras con el famoso “oye, ¿tú eres el primo de fulano?”. Ahora, en el laberinto, usa su don para cambiar posiciones estratégicas, aunque siempre dice con una sonrisa: “El que no inventa, no avanza"
        ));
       
        _imagenPersonaje = GetNode<TextureRect>("TextureRect");
        _etiquetaJugador = GetNode<Label>("LabelPlayer");
        _etiquetaHistoria = GetNode<Label>("LabelHistoria");

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
