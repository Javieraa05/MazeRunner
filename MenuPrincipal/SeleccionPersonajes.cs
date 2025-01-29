using Godot;
using System;
using System.Collections.Generic;

public partial class SeleccionPersonajes : Control
{
    private int PersonajeActual = 0; // Índice del personaje actual
    private string[] Personajes = {
        "Personaje1", "Personaje2", "Personaje3", "Personaje4", 
        "Personaje5"
    };
    private bool EsTurnoJugador1 = true; // Controla el turno de selección
    private TextureRect ImagenPersonaje;
    private Label EtiquetaJugador;
    private int SeleccionPlayer1;
    private int SeleccionPlayer2;
    private Label EtiquetaNombre;
    private Label EtiquetaHabilidad;
    private Label EtiquetaHistoria;
    

    private Dictionary <int, Personaje> personajes = new Dictionary<int, Personaje>();


    public override void _Ready()
    {
       
         // Inicializar los personajes
        personajes.Add(1, new Personaje(
            "Miguel\n", 
            "Curación   Tiempo de Recarga: 25s", 
            "Conocido en su barrio como el “doctor de los remedios caseros”, \n" +
            "se hizo famoso porque curó al vecino con una mezcla de miel, limón y “un toquecito” de ron. \n" +
            "Cuando el ciclón dejó sin médicos al pueblo, él tomó la batuta, aplicando desde sueros improvisados \n" +
            "con tubos de refresco hasta 'masajes de la vida' que revivían hasta al más agotado. \n" +
            "Ahora, recorre el laberinto con su botiquín que incluye una botella de Habana Club por si acaso... \n" +
            "ya sabes, para “desinfectar heridas”."

        ));
        personajes.Add(2, new Personaje(
            "Yadira\n", 
            "Super Velocidad   Tiempo de Recarga: 35s", 
            "Campeona del Marabana tres años seguidos, siempre decía que su secreto era\n" +
            "'correr detrás de las guagua cuando no habian gacelas'.\n" +
            "Una vez, en pleno apagón, corrió tan rápido para alcanzar la última croqueta en la cafetería\n" +
            "que hasta el vendedor quedó asustado.\n" +
            "Ahora, en el laberinto, Yadira usa su velocidad para esquivar enemigos."


        ));
        personajes.Add(3, new Personaje(
            "Maritza\n", 
            "Escudo   Tiempo de Recarga: 40s", 
            "Era herrera en un taller de mecánica en Centro Habana.\n" +
            "Un día, tras un apagón que dejó la ciudad sin hornos, creó un escudo indestructible\n" +
            "fundiendo pedazos de lavadoras soviéticas y tapas de olla de presión.\n" +
            "En su barrio, siempre la llamaban para proteger las mesas de dominó cuando alguien se ponía “muy caliente”.\n" +
            "Ahora, en el laberinto, su escudo es legendario."

        ));
        personajes.Add(4, new Personaje(
            "Ernesto\n", 
            "Revelación de Mapa   Tiempo de Recarga: 25s", 
            "Ernesto trabajaba como guía turístico en Viñales, conocido por llevar a los extranjeros\n" +
            "a 'rutas alternativas' donde el GPS no funcionaba.\n" +
            "Con un ojo para los atajos y una habilidad innata para leer mapas del tiempo de los españoles,\n" +
            "siempre decía: 'Si yo me pierdo, ¡es porque el camino cambió de lugar!'.\n" +
            "Ahora, en el laberinto, su habilidad para descubrir pasillos secretos lo convirtió en el tipo que todos siguen."

        ));
        personajes.Add(5, new Personaje(
            "Manolo\n", 
            "Intercambio de Posición   Tiempo de Recarga: 45s", 
            "Manolo era un maestro del truco y la improvisación, conocido en el malecón\n" +
            "por su habilidad de cambiar su lugar en la cola del pollo con una charla rápida y un par de gestos mágicos.\n" +
            "Una vez, logró colarse en una cola de dos cuadras con el famoso\n" +
            "‘oye, ¿tú eres el primo de fulano?’. \n" +
            "Ahora, en el laberinto, usa su don para cambiar posiciones estratégicas,\n" +
            "aunque siempre dice con una sonrisa: 'El que no inventa, no avanza'."

        ));
       
        ImagenPersonaje = GetNode<TextureRect>("VContainer/HContainer/ContainerImagen/TextureRect");
        EtiquetaJugador = GetNode<Label>("VContainer/LabelPlayer");
        EtiquetaNombre = GetNode<Label>("VContainer/PanelContainer/MarginContainer/VContainer/LabelNombre");
        EtiquetaHabilidad = GetNode<Label>("VContainer/PanelContainer/MarginContainer/VContainer/LabelHabilidad");
        EtiquetaHistoria = GetNode<Label>("VContainer/PanelContainer/MarginContainer/VContainer/LabelHistoria");

        ActualizarVista();
        
        GetNode<Button>("VContainer/HContainer/ContainerBoton1/BotonAnterior").Pressed += OnBotonAnteriorPressed;
        GetNode<Button>("VContainer/HContainer/ContainerBoton2/BotonSiguiente").Pressed += OnBotonSiguientePressed;
        GetNode<Button>("BotonSeleccionar").Pressed += OnBotonSeleccionarPressed;
    }

    private void OnBotonAnteriorPressed()
    {
        PersonajeActual = (PersonajeActual - 1 + Personajes.Length) % Personajes.Length;
        ActualizarVista();
    }

    private void OnBotonSiguientePressed()
    {
        PersonajeActual = (PersonajeActual + 1) % Personajes.Length;
        ActualizarVista();
    }

    private void OnBotonSeleccionarPressed()
    {
      if (EsTurnoJugador1)
    {
        // Guarda la selección del Player 1
        SeleccionPlayer1 = PersonajeActual + 1;
        GD.Print($"Player 1 seleccionó: {SeleccionPlayer1}");
        EsTurnoJugador1 = false;
        EtiquetaJugador.Text = "Jugador 2";
    }
    else
    {
        // Guarda la selección del Player 2
        SeleccionPlayer2 = PersonajeActual + 1;
        GD.Print($"Player 2 seleccionó: {SeleccionPlayer2}");

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
                    player1Node.SeleccionPersonaje1 = SeleccionPlayer1;
                }

                var player2Node = mainNode.GetNodeOrNull<Player_2>("Viewports/ViewportContainer1/Viewport1/World/Player_2");
                if (player2Node != null)
                {
                    player2Node.SeleccionPersonaje2 = SeleccionPlayer2;
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
        ImagenPersonaje.Texture = (Texture2D)GD.Load<Texture>($"res://Imagenes/Personajes/{Personajes[PersonajeActual]}.png");


    	EtiquetaJugador.Text = EsTurnoJugador1 ? "Jugador 1" : "Jugador 2";

        EtiquetaNombre.Text = ObtenerNombrePersonaje(PersonajeActual+1);
        EtiquetaHabilidad.Text = ObtenerHabilidadPersonaje(PersonajeActual+1);
        EtiquetaHistoria.Text = ObtenerHistoriaPersonaje(PersonajeActual+1);
    }

    // Método para mostrar la historia de un personaje seleccionado
    public string ObtenerHistoriaPersonaje(int id)
    {
        if (personajes.ContainsKey(id))
        {
            return $"Historia: {personajes[id].Historia}";
        }
        return "Personaje no encontrado.";
    }
     public string ObtenerNombrePersonaje(int id)
    {
        if (personajes.ContainsKey(id))
        {
            return $"Nombre: {personajes[id].Nombre}";
        }
        return "Personaje no encontrado.";
    }
     public string ObtenerHabilidadPersonaje(int id)
    {
        if (personajes.ContainsKey(id))
        {
            return $"Habilidad: {personajes[id].Habilidad}";
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
