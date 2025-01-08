using Godot;

public partial class Main : Node
{
    private Viewport viewport1;
    private Viewport viewport2;
    private Camera2d1 camera1;
    private Camera2d1 camera2;
    private Node world;

  public override void _Ready()
{
    // Obtener referencias a los Viewports, cámaras y el mundo
    viewport1 = GetNode<Viewport>("Viewports/ViewportContainer1/Viewport1");
    viewport2 = GetNode<Viewport>("Viewports/ViewportContainer2/Viewport2");
    camera1 = GetNode<Camera2d1>("Viewports/ViewportContainer1/Viewport1/Camera2D1");
    camera2 = GetNode<Camera2d1>("Viewports/ViewportContainer2/Viewport2/Camera2D2");
    world = GetNode<Node>("Viewports/ViewportContainer1/Viewport1/World");

    // Compartir el mismo World2D entre ambos Viewports
    viewport2.World2D = viewport1.World2D;

    // Configurar los objetivos de las cámaras
    var player1 = world.GetNodeOrNull<PlayerBase>("Player_1");
    var player2 = world.GetNodeOrNull<PlayerBase>("Player_2");
    // Vincular HUDs
    var hudPlayer1 = GetNodeOrNull<HUD_Player>("Viewports/ViewportContainer1/HUD_Player1");
    var hudPlayer2 = GetNodeOrNull<HUD_Player>("Viewports/ViewportContainer2/HUD_Player2");
    var texturaIcono1 = GD.Load<Texture>($"res://Imagenes/Hearts/habilidad{player1.SelectedCharacter1}.png");
    var texturaIcono2 = GD.Load<Texture>($"res://Imagenes/Hearts/habilidad{player2.SelectedCharacter2}.png");

    if (player1 != null && hudPlayer1 != null && player2 != null && hudPlayer2 != null)
    {
        hudPlayer1.SetPlayer(player1);
        hudPlayer2.SetPlayer(player2);
    }
   
    
    if (player1 != null && player2 != null)
    {
        camera1.SetTarget(player1);
        camera2.SetTarget(player2);
        
    }


    else
    {
        GD.PrintErr("No se encontraron los nodos Player_1 o Player_2.");
    }
}
   
}
