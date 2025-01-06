using Godot;

public partial class Player_1 : PlayerBase
{
    
    public override void _Ready()
{   
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
        Position = new Vector2(205, 1877);
    }
    public override void _Process(double delta)
{
    base._Process(delta);

    if (Input.IsActionJustPressed("habilidad_player1")) // Jugador 1
    {   
        GD.Print("Se presiono la tecla");
        UsarHabilidad();
    }
}
}
