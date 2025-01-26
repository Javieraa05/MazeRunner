using Godot;

public partial class Main : Node
{
    private Viewport viewport1;
    private Viewport viewport2;
    private Camera2d1 Camara1;
    private Camera2d1 Camara2;
    private Node Mundo;
    private Sprite2D puntoRojo;
    private Sprite2D puntoAzul;
    private Sprite2D llave1MM;
    private Sprite2D llave2MM;
    private Sprite2D llave3MM;
    private Sprite2D llave4MM;
    private PlayerBase player1;
    private PlayerBase player2;
    float posX1;
    float posY1;
    float posX2;
    float posY2;
    Area2D llave1;
    Area2D llave2;
    Area2D llave3;
    Area2D llave4;
    

  public override void _Ready()
{
    // Obtener referencias a los Viewports, cámaras y el mundo
    viewport1 = GetNode<Viewport>("Viewports/ViewportContainer1/Viewport1");
    viewport2 = GetNode<Viewport>("Viewports/ViewportContainer2/Viewport2");
    Camara1 = GetNode<Camera2d1>("Viewports/ViewportContainer1/Viewport1/Camera2D1");
    Camara2 = GetNode<Camera2d1>("Viewports/ViewportContainer2/Viewport2/Camera2D2");
    Mundo = GetNode<Node>("Viewports/ViewportContainer1/Viewport1/World");
    llave1 = GetNode<Area2D>("Viewports/ViewportContainer1/Viewport1/World/TileMapLayer/Room1/TileMapLayer/Llave1");
    llave2 = GetNode<Area2D>("Viewports/ViewportContainer1/Viewport1/World/TileMapLayer/Room2/TileMapLayer/Llave2");
    llave3 = GetNode<Area2D>("Viewports/ViewportContainer1/Viewport1/World/TileMapLayer/Room3/TileMapLayer/Llave3");
    llave4 = GetNode<Area2D>("Viewports/ViewportContainer1/Viewport1/World/TileMapLayer/Room4/TileMapLayer/Llave4");
    
    // Obtener referencias a las imagenes del minimapa
    puntoRojo = GetNode<Sprite2D>("Viewports/MiniMapa/PuntoRojo");
    puntoAzul = GetNode<Sprite2D>("Viewports/MiniMapa/PuntoAzul");
    llave1MM = GetNode<Sprite2D>("Viewports/MiniMapa/Llave1");
    llave2MM = GetNode<Sprite2D>("Viewports/MiniMapa/Llave2");
    llave3MM = GetNode<Sprite2D>("Viewports/MiniMapa/Llave3");
    llave4MM = GetNode<Sprite2D>("Viewports/MiniMapa/Llave4");
    
    //Asignar las posiciones de las llaves en el minimapa
    llave1MM.Position = new Vector2(llave1.Position.X / 2.24157216f,llave1.Position.Y / 2.24157216f);
    llave2MM.Position = new Vector2(((llave2.Position.X+934) / 2.24157216f),llave2.Position.Y / 2.24157216f);
    llave3MM.Position = new Vector2((llave3.Position.X / 2.24157216f),(llave3.Position.Y+906) / 2.24157216f);
    llave4MM.Position = new Vector2(((llave4.Position.X+934) / 2.24157216f),(llave4.Position.Y+906) / 2.24157216f);


    // Compartir el mismo World2D entre ambos Viewports
    viewport2.World2D = viewport1.World2D;

    // Configurar los objetivos de las cámaras
    player1 = Mundo.GetNodeOrNull<PlayerBase>("Player_1");
    player2 = Mundo.GetNodeOrNull<PlayerBase>("Player_2");

    //Asignar las imagenes de los jugadores en el minimapa
    puntoRojo.Texture = (Texture2D)GD.Load<Texture2D>($"res://Imagenes/Personajes/Personaje{player1.SelectedCharacter1}.png");
    puntoAzul.Texture = (Texture2D)GD.Load<Texture2D>($"res://Imagenes/Personajes/Personaje{player2.SelectedCharacter2}.png");

    // Vincular HUDs
    var hudPlayer1 = GetNodeOrNull<HUD_Player>("Viewports/ViewportContainer1/HUD_Player1");
    var hudPlayer2 = GetNodeOrNull<HUD_Player>("Viewports/ViewportContainer2/HUD_Player2");
    var texturaIcono1 = GD.Load<Texture>($"res://Imagenes/Hearts/habilidad{player1.SelectedCharacter1}.png");
    var texturaIcono2 = GD.Load<Texture>($"res://Imagenes/Hearts/habilidad{player2.SelectedCharacter2}.png");

    if (player1 != null && hudPlayer1 != null && player2 != null && hudPlayer2 != null)
    {
        hudPlayer1.SeleccionarJugador(player1);
        hudPlayer2.SeleccionarJugador(player2);
        hudPlayer1.ImagenHabilidad(texturaIcono1);
        hudPlayer2.ImagenHabilidad(texturaIcono2);
        hudPlayer1.TeclaHabilidad("E");
        hudPlayer2.TeclaHabilidad("M");
    }
   
    
    if (player1 != null && player2 != null)
    {
        Camara1.SetTarget(player1);
        Camara2.SetTarget(player2);
        
    }


    else
    {
        GD.PrintErr("No se encontraron los nodos Player_1 o Player_2.");
    }
}



 public override void _Process(double delta)
 {
    // Actualizar la posición de los jugadores en el minimapa
    posX1 = player1.Position.X / 2.24157216f;
    posY1 = player1.Position.Y / 2.24462962f;
    posX2 = player2.Position.X / 2.25299f;
    posY2 = player2.Position.Y / 2.2533666f;
    puntoRojo.Position = new Vector2(posX1,posY1);
    puntoAzul.Position = new Vector2(posX2,posY2);

    //Actualizar la visibilidad de las llaves en el minimapa
    llave1MM.Visible = llave1.Visible;
    llave2MM.Visible = llave2.Visible;
    llave3MM.Visible = llave3.Visible;
    llave4MM.Visible = llave4.Visible;
    
 }
   
}
