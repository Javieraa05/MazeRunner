using Godot;

public abstract class HabilidadBase
{
    public abstract string Nombre { get; }
    public abstract float Cooldown { get; }
    public bool Disponible { get; private set; } = true;

    public void Activar(Node jugador)
    {
        if (!Disponible)
            return;

        Efecto(jugador);
        Disponible = false;
        jugador.GetTree().CreateTimer(Cooldown).Timeout += () => Disponible = true;
    }

    protected abstract void Efecto(Node jugador);
}
