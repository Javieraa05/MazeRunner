using Godot;
public class Curacion : HabilidadBase
{
   public override string Nombre => "Curación";
    public override float Enfriamiento=> 30f;

    protected override void Efecto(PlayerBase jugador)
    {
        if (jugador is PlayerBase)
        {
            GD.Print("P1 Se curo");
            jugador.AjustarSalud(2); // Incrementa la salud en 2 puntos
            jugador.EmitirNoticia("Te haz curado");
        }
       
    }
}
