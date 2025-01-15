using Godot;
public class IntercambiaPosicion : HabilidadBase
{
    public override string Nombre => "Radar de llaves";
    public override float Cooldown => 30f;

    protected override void Efecto(PlayerBase jugador)
    {
        
            if(jugador is Player_1 player1 && jugador.GetParent().GetNodeOrNull<Player_2>("Player_2") is Player_2 player2)
            {
                // Guarda las posiciones actuales
                Vector2 posicionPlayer1 = player1.Position;
                Vector2 posicionPlayer2 = player2.Position;

                // Intercambia las posiciones
                player1.Position = posicionPlayer2;
                player2.Position = posicionPlayer1;
                GD.Print("Intercambio la posicion");
            }
             else if (jugador is Player_2 player_2 && jugador.GetParent().GetNodeOrNull<Player_1>("Player_1") is Player_1 player_1)
        {
            // Guarda las posiciones actuales
            Vector2 posicionPlayer1 = player_1.Position;
            Vector2 posicionPlayer2 = player_2.Position;

            // Intercambia las posiciones
            player_1.Position = posicionPlayer2;
            player_2.Position = posicionPlayer1;
            GD.Print("Intercambio la posicion");
        }
    }
}
