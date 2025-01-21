using Godot;

public partial class Player_2 : PlayerBase
{
    Player_1 player1;
    public override void _Ready()
    {   
        player1 = GetNode<Player_1>("/root/Main/Viewports/ViewportContainer1/Viewport1/World/Player_1");
        areaAtaque = GetNode<Area2D>("AreaAtaque");
        areaAtaque.BodyEntered += OnBodyEntered;


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
    }
    public override void _Process(double delta)
    {
        base._Process(delta);

        if (Input.IsActionJustPressed("habilidad_player2")) // Jugador 1
        {   
            GD.Print("Se presiono la tecla");
            UsarHabilidad();
        }
        if(Input.IsActionJustPressed("atacar_player2") && areaAtaque.OverlapsBody(player1) && player1.Experiencia < this.Experiencia)
        {
            EliminarP();
        }
        if(areaAtaque.OverlapsBody(player1))
        {
            botonAtaque = true;
        }
        else botonAtaque = false;
        
    }

     private void OnBodyEntered(Node body)
    {
        if(body is Player_1 player1)
        {
            botonAtaque = true;
        }
    }

     public override void EliminarP()
     {
        player1.ResetPosition();
     }
}
