using Godot;

public partial class Menu : Control
{
    private Button BotonJugar;
    private Button BotonSalir;
    private Button BotonCreditos;


    public override void _Ready()
    {
        // Obtener referencias a los botones
        BotonJugar = GetNode<Button>("Container/Jugar");
        BotonSalir = GetNode<Button>("Container/Salir");
        BotonCreditos = GetNode<Button>("Container/Creditos");


        // Conectar las señales de los botones
        BotonJugar.Pressed += OnBotonJugarPressed;
        BotonSalir.Pressed += OnBotonSalirPressed;
        BotonCreditos.Pressed += OnBotonCreditosPressed;

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
     private void OnBotonCreditosPressed()
    {
        // Cambiar a la escena creditos
        GetTree().ChangeSceneToFile("res://MenuPrincipal/creditos.tscn");
    }
}
