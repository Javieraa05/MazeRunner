using Godot;
using System;

public partial class room4 : TileMapLayer
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_keyScene = GD.Load<PackedScene>("res://LLaves/LLave4.tscn");
		base._Ready();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
