using Godot;
public class Curacion : HabilidadBase
{
   public override string Nombre => "Curación";
    public override float Cooldown => 30f;

    protected override void Efecto(PlayerBase jugador)
    {
        if (jugador is PlayerBase player)
        {
            GD.Print("Se curo");
            player.AjustarSalud(2); // Incrementa la salud en 2 puntos
        }
    }
}
