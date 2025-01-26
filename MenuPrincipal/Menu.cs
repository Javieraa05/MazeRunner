using Godot;

public partial class Menu : Control
{
    private Button BotonJugar;
    private Button BotonSalir;

    public override void _Ready()
    {
        // Obtener referencias a los botones
        BotonJugar = GetNode<Button>("Jugar");
        BotonSalir = GetNode<Button>("Salir");

        // Conectar las señales de los botones
        BotonJugar.Pressed += OnBotonJugarPressed;
        BotonSalir.Pressed += OnBotonSalirPressed;
    }

    private void OnBotonJugarPressed()
    {
        // Cambiar a la escena del juego
        GetTree().ChangeSceneToFile("res://MenuPrincipal/Historia.tscn");
    }

    private void OnBotonSalirPressed()
    {
        // Salir del juego
        GD.Print("Salir presionado");
        GetTree().Quit();
    }
}
