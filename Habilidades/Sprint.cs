using Godot;
public class Sprint : HabilidadBase
{
    public override string Nombre => "Sprint";
    public override float Cooldown => 30f;

    protected override void Efecto(PlayerBase jugador)
    {
        if (jugador is PlayerBase player)
        {   
            GD.Print("Se sprinteo");
            player.Speed *= 3; 
            player.EmitirNoticia("Haz aumentado tu velocidad");
            player.GetTree().CreateTimer(5).Timeout += () => player.Speed /= 3;  // Vuelve a la velocidad normal después de 5 segundos
        }
    }
}
