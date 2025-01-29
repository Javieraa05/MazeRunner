using Godot;
public class Sprint : HabilidadBase
{
    public override string Nombre => "Sprint";
    public override float Enfriamiento=> 35f;

    protected override void Efecto(PlayerBase jugador)
    {
        if (jugador is PlayerBase player)
        {   
            GD.Print("Se sprinteo");
            player.Velocidad *= 3; 
            player.EmitirNoticia("Haz aumentado tu velocidad");
            player.GetTree().CreateTimer(5).Timeout += () => player.Velocidad /= 3;  // Vuelve a la velocidad normal después de 5 segundos
        }
    }
}
