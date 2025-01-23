using Godot;
using System;

public partial class Historia : Control
{
	 Button button;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		button = GetNode<Button>("Button");
		button.Pressed += OnPressed;
	}
	public void OnPressed()
	{
		GetTree().ChangeSceneToFile("res://MenuPrincipal/SeleccionPersonajes.tscn");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
