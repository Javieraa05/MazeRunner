using Godot;
public class Curacion : HabilidadBase
{
   public override string Nombre => "Curación";
    public override float Cooldown => 30f;

    protected override void Efecto(Node jugador)
    {
        if (jugador is PlayerBase player)
        {
            player.AjustarSalud(2); // Incrementa la salud en 2 puntos
        }
    }
}
