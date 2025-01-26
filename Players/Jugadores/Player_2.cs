using Godot;

public partial class Player_2 : PlayerBase
{
    public Player_1 player1;
    public override void _Ready()
    {   
        player1 = GetNode<Player_1>("/root/Main/Viewports/ViewportContainer1/Viewport1/World/Player_1");
        areaAtaque = GetNode<Area2D>("AreaAtaque");
        


        if(SelectedCharacter2 == 0) SelectedCharacter2=2;
        characterScene = GD.Load<PackedScene>($"res://Players/Personaje{SelectedCharacter2}/personaje_{SelectedCharacter2}.tscn");
        base._Ready();
        // Asignar habilidad según el personaje seleccionado
        if (habilidadesPorPersonaje.ContainsKey(SelectedCharacter2))
        {
            AsignarHabilidad(habilidadesPorPersonaje[SelectedCharacter2]);
        }
    }    
    protected override Vector2 GetInput()
    {
        return Input.GetVector("izquierda1", "derecha1", "arriba1", "abajo1");
    }

    protected override void SetInitialPosition()
    {
        Position = new Vector2(1637, 1603);
        //Position = new Vector2(45, 1600);

    }
    public override void _Process(double delta)
    {
        base._Process(delta);

        if (Input.IsActionJustPressed("habilidad_player2")) // Jugador 1
        {   
            GD.Print("P2 presiono la tecla de la habilidad");
            UsarHabilidad();
        }
        if(Input.IsActionJustPressed("atacar_player2") && areaAtaque.OverlapsBody(player1) )
        {
            if(!player1.Escudo)
            {  
                if(player1.Experiencia < this.Experiencia)
                {
                    if(player1.Health-2 < 1 && player1.llaves.Count >= 1)
                    {
                        this.llaves.Add(player1.llaves[0]);
                        CantidadLlaves++;
                        EmitSignal(nameof(KeysChanged), CantidadLlaves);
                        player1.llaves.RemoveAt(0);
                    }
                
                    if(player1.Health-2 <= 0)
                    {
                        player1.EmitirNoticia("Te ha matado el Jugador 1");
                    }
                    player1.TomarDano(2);

                }
                else
                {
                    EmitirNoticia("No tienes suficiente experiencia");
                } 
            }
            else
            {
                EmitirNoticia("El Jugador 1 tiene un escudo activo");
            }   
        }
        if(areaAtaque.OverlapsBody(player1))
        {
            botonAtaque = true;
        }
        else botonAtaque = false;
        
    }



    public override void UsarHabilidad()
    {
        if(habilidadActual == null) GD.Print("Player2 no tiene habilidad asignada");
        if (habilidadActual != null && habilidadActual.DisponibleP2)
        {
            GD.Print("Player2 mando a activar la habilidad");
            habilidadActual.Activar(this);
            EmitSignal(nameof(ActivarHabilidad), habilidadActual.Cooldown);
        }
        else
        {
            GD.Print("No se puede usar la habilidad");
        }
    }
}
