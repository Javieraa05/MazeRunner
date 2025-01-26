using Godot;

public partial class Door : Node2D
{
    public float VelocidadApertura = 50f; // Velocidad de apertura
    public float DistanciaMaxima = 30f; // Distancia máxima de movimiento de cada bloque
    private StaticBody2D puertaIzquierda;
    private StaticBody2D puertaDerecha;
    private Area2D _Area2DAbrir;

    private bool EstaAbriendo = false;
    private bool EstaCerrando = false;
    private Vector2 PosInicialIzq;
    private Vector2 PosInicialDer;

    public override void _Ready()
    {
        // Obtener referencias a los nodos
        puertaIzquierda = GetNode<StaticBody2D>("PuertaIzquierda");
        puertaDerecha = GetNode<StaticBody2D>("PuertaDerecha");
        _Area2DAbrir = GetNode<Area2D>("Area2DAbrir");

        // Guardar posiciones iniciales
        PosInicialIzq = puertaIzquierda.Position;
        PosInicialDer = puertaDerecha.Position;

        // Conectar eventos
        _Area2DAbrir.BodyEntered += OnBodyEntered;
    }

    private void OnBodyEntered(Node body)
    {
        if (body is PlayerBase player && player.GetCantidadLlaves()>=3)
        {
            EstaAbriendo = true;
        }
        else if(body is PlayerBase players )
        {
            players.EmitirNoticia("No tienes suficientes llaves");
            GD.Print("No tienes suficientes llaves para abrir la puerta.");
        }
        
    }

    public override void _Process(double delta)
    {
        if (EstaAbriendo)
        {
            AbrirPuerta(delta);
        }
    }

    private void AbrirPuerta(double delta)
    {
        bool izquierdaMovida = false;
        bool derechaMovida = false;

        // Mover la puerta izquierda hacia la izquierda
        if (puertaIzquierda.Position.X > PosInicialIzq.X - DistanciaMaxima)
        {
            puertaIzquierda.Position += new Vector2(-VelocidadApertura * (float)delta, 0);
            izquierdaMovida = true;
        }

        // Mover la puerta derecha hacia la derecha
        if (puertaDerecha.Position.X < PosInicialDer.X + DistanciaMaxima)
        {
            puertaDerecha.Position += new Vector2(VelocidadApertura * (float)delta, 0);
            derechaMovida = true;
        }

        // Detener el movimiento si ambas puertas han alcanzado sus posiciones finales
        if (!izquierdaMovida && !derechaMovida)
        {
            EstaAbriendo = false;
        }
    }
}
