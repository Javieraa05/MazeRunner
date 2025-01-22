using Godot;

public abstract class HabilidadBase
{
    public abstract string Nombre { get; }
    public abstract float Cooldown { get; }
    public bool DisponibleP1 { get; private set; } = true;
    public bool DisponibleP2 { get; private set; } = true;

    private float tiempoRestanteCooldown = 0;

    public void Activar(PlayerBase jugador)
    {
        
        if(jugador is Player_1 player_1)
        {
        Efecto(player_1);
        DisponibleP1 = false;
        tiempoRestanteCooldown = Cooldown;
        jugador.GetTree().CreateTimer(Cooldown).Timeout += () => DisponibleP1 = true;
        }
        if(jugador is Player_2 player_2)
        {
        Efecto(player_2);
        DisponibleP2 = false;
        tiempoRestanteCooldown = Cooldown;
        jugador.GetTree().CreateTimer(Cooldown).Timeout += () => DisponibleP2 = true;
        }
    }

    protected abstract void Efecto(PlayerBase jugador);

}
