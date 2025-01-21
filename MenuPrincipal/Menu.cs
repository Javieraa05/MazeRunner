using Godot;

public partial class Menu : Control
{
    private Button playButton;
    private Button quitButton;

    public override void _Ready()
    {
        // Obtener referencias a los botones
        playButton = GetNode<Button>("Jugar");
        quitButton = GetNode<Button>("Salir");

        // Conectar las señales de los botones
        playButton.Pressed += OnPlayButtonPressed;
        quitButton.Pressed += OnQuitButtonPressed;
    }

    private void OnPlayButtonPressed()
    {
        // Cambiar a la escena del juego
        GetTree().ChangeSceneToFile("res://MenuPrincipal/SeleccionPersonajes.tscn");
    }

    private void OnQuitButtonPressed()
    {
        // Salir del juego
        GD.Print("Salir presionado");
        GetTree().Quit();
    }
}
