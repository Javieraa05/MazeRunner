using Godot;
public class RevelacionMapa : HabilidadBase
{
    public override string Nombre => "Revelación de mapa";
    public override float Cooldown => 20f;

    protected override void Efecto(PlayerBase jugador)
    {
        
        jugador.Zoom = new Vector2(2,2);
        jugador.GetTree().CreateTimer(10).Timeout += () => { jugador.Zoom = new Vector2(5,5); };
    }
}
