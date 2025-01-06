using Godot;
public class RevelacionMapa : HabilidadBase
{
    public override string Nombre => "Revelación de mapa";
    public override float Cooldown => 20f;

    protected override void Efecto(Node jugador)
    {

    }
}
