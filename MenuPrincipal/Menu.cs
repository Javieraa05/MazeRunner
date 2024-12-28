using Godot;

public partial class Menu : Control
{
    private Button playButton;
    private Button optionsButton;
    private Button quitButton;

    public override void _Ready()
    {
        // Obtener referencias a los botones
        playButton = GetNode<Button>("Container/Jugar");
        optionsButton = GetNode<Button>("Container/Opciones");
        quitButton = GetNode<Button>("Container/Salir");

        // Conectar las señales de los botones
        playButton.Pressed += OnPlayButtonPressed;
        optionsButton.Pressed += OnOptionsButtonPressed;
        quitButton.Pressed += OnQuitButtonPressed;
    }

    private void OnPlayButtonPressed()
    {
        // Cambiar a la escena del juego
        GetTree().ChangeSceneToFile("res://Main/main.tscn");
    }

    private void OnOptionsButtonPressed()
    {
        // Cambiar a la escena de opciones o abrir un menú de opciones
        GD.Print("Opciones presionado");
      //  GetTree().ChangeScene("res://Scenes/OptionsMenu.tscn");
    }

    private void OnQuitButtonPressed()
    {
        // Salir del juego
        GD.Print("Salir presionado");
        GetTree().Quit();
    }
}
