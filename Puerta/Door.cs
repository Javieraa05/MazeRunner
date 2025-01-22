using Godot;

public partial class Door : Node2D
{
    public float OpenSpeed = 50f; // Velocidad de apertura
    public float MaxDistance = 30f; // Distancia máxima de movimiento de cada bloque
    private StaticBody2D puertaIzquierda;
    private StaticBody2D puertaDerecha;
    private Area2D _Area2DAbrir;

    private bool _isOpening = false;
    private bool _isClosing = false;
    private Vector2 _leftStartPos;
    private Vector2 _rightStartPos;

    public override void _Ready()
    {
        // Obtener referencias a los nodos
        puertaIzquierda = GetNode<StaticBody2D>("PuertaIzquierda");
        puertaDerecha = GetNode<StaticBody2D>("PuertaDerecha");
        _Area2DAbrir = GetNode<Area2D>("Area2DAbrir");

        // Guardar posiciones iniciales
        _leftStartPos = puertaIzquierda.Position;
        _rightStartPos = puertaDerecha.Position;

        // Conectar eventos
        _Area2DAbrir.BodyEntered += OnBodyEntered;
    }

    private void OnBodyEntered(Node body)
    {
        if (body is PlayerBase player && player.GetCantidadLlaves()>=3)
        {
            _isOpening = true;
        }
        else if(body is PlayerBase players )
        {
            players.EmitirNoticia("No tienes suficientes llaves");
            GD.Print("No tienes suficientes llaves para abrir la puerta.");
        }
        
    }

    public override void _Process(double delta)
    {
        if (_isOpening)
        {
            AbrirPuerta(delta);
        }
    }

    private void AbrirPuerta(double delta)
    {
        bool izquierdaMovida = false;
        bool derechaMovida = false;

        // Mover la puerta izquierda hacia la izquierda
        if (puertaIzquierda.Position.X > _leftStartPos.X - MaxDistance)
        {
            puertaIzquierda.Position += new Vector2(-OpenSpeed * (float)delta, 0);
            izquierdaMovida = true;
        }

        // Mover la puerta derecha hacia la derecha
        if (puertaDerecha.Position.X < _rightStartPos.X + MaxDistance)
        {
            puertaDerecha.Position += new Vector2(OpenSpeed * (float)delta, 0);
            derechaMovida = true;
        }

        // Detener el movimiento si ambas puertas han alcanzado sus posiciones finales
        if (!izquierdaMovida && !derechaMovida)
        {
            _isOpening = false;
        }
    }
}
