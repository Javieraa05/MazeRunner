using Godot;

public partial class Player_1 : PlayerBase
{
    

    private Player_2 player2;
    public override void _Ready()
{   
    areaAtaque = GetNode<Area2D>("AreaAtaque");
    player2 = GetNode<Player_2>("/root/Main/Viewports/ViewportContainer1/Viewport1/World/Player_2");

    
    if(SelectedCharacter1 == 0) SelectedCharacter1=1;
    characterScene = GD.Load<PackedScene>($"res://Players/Personaje{SelectedCharacter1}/personaje_{SelectedCharacter1}.tscn");
    
    base._Ready();
     
     // Asignar habilidad según el personaje seleccionado
    if (habilidadesPorPersonaje.ContainsKey(SelectedCharacter1))
    {
        AsignarHabilidad(habilidadesPorPersonaje[SelectedCharacter1]);
    }
}


    protected override Vector2 GetInput()
    {
        return Input.GetVector("izquierda2", "derecha2", "arriba2", "abajo2");
    }

    protected override void SetInitialPosition()
    {
        Position = new Vector2(45, 1603);
    }
    
    public override void _Process(double delta)
    {   
        base._Process(delta);

        if (Input.IsActionJustPressed("habilidad_player1")) // Jugador 1
        {   
            GD.Print("P1 presiono la tecla de la habilidad");
            UsarHabilidad();
        }
        if(Input.IsActionJustPressed("atacar_player1") && areaAtaque.OverlapsBody(player2) )
        {
            if(player2.Experiencia < this.Experiencia)
            {
                
                if(player2.Health-2 < 1 && player2.llaves.Count >= 1)
                {
                    this.llaves.Add(player2.llaves[0]);
                    CantidadLlaves++;
                    EmitSignal(nameof(KeysChanged), CantidadLlaves);
                    player2.llaves.RemoveAt(0);
                }
                
                if(player2.Health-2 <= 0)
                {
                    player2.EmitirNoticia("Te ha matado el Jugador 1");
                }
                player2.TomarDano(2);
            }
            else
            {
                EmitirNoticia("No tienes suficiente experiencia");
            }    
        }
        if(areaAtaque.OverlapsBody(player2))
        {
            botonAtaque = true;
        }
        else botonAtaque = false;
        
        

    }
    public override void UsarHabilidad()
    {
        if(habilidadActual == null) GD.Print("Player1 no tiene habilidad asignada");
        if (habilidadActual != null && habilidadActual.DisponibleP1)
        {
            GD.Print("Player1 mando a activar la habilidad");
            habilidadActual.Activar(this);
            EmitSignal(nameof(ActivarHabilidad), habilidadActual.Cooldown);
        }
        else
        {
            GD.Print("No se puede usar la habilidad");
        }
    }





}

