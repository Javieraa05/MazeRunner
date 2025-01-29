using Godot;
public class IntercambiaPosicion : HabilidadBase
{
    public override string Nombre => "Radar de llaves";
    public override float Enfriamiento=> 45f;

    protected override void Efecto(PlayerBase jugador)
    {
        
        if (jugador is Player_1 player1 && jugador.GetParent().GetNodeOrNull<Player_2>("Player_2") is Player_2 player2)
        {
            IntercambiarPosiciones(player1, player2);
        }
        else if (jugador is Player_2 player_2 && jugador.GetParent().GetNodeOrNull<Player_1>("Player_1") is Player_1 player_1)
        {
            IntercambiarPosiciones(player_2, player_1);
        }
    }
    private void IntercambiarPosiciones(PlayerBase jugador1, PlayerBase jugador2)
{
        Vector2 posicionJugador1 = jugador1.Position;
        jugador1.Position = jugador2.Position;
        jugador2.Position = posicionJugador1;

        jugador2.EmitirNoticia("Te han intercambiado la posición");
        GD.Print("Intercambio la posición");
}
}
