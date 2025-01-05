using Godot;

public partial class Player_2 : PlayerBase
{
    public override void _Ready()
    {   
        if(SelectedCharacter2 == 0) SelectedCharacter2=2;
        characterScene = GD.Load<PackedScene>($"res://Players/Personaje{SelectedCharacter2}/personaje_{SelectedCharacter2}.tscn");
        base._Ready();
    }    
    protected override Vector2 GetInput()
    {
        return Input.GetVector("izquierda1", "derecha1", "arriba1", "abajo1");
    }

    protected override void SetInitialPosition()
    {
        Position = new Vector2(1795, 1877);
    }
}
