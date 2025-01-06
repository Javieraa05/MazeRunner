using Godot;
public class RadarLlaves : HabilidadBase
{
    public override string Nombre => "Radar de llaves";
    public override float Cooldown => 20f;

    protected override void Efecto(Node jugador)
    {
        /*if (jugador.GetParent() is TileMapLayer mapa)
        {
            mapa.MostrarFlechasLlaves(10);  // Implementa esta función para mostrar las flechas
        }*/
    }
}
