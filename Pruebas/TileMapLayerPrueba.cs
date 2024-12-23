using Godot;
using System;

public partial class TileMapLayerPrueba : TileMapLayer
{
	PackedScene packedScene1;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		packedScene1 = GD.Load<PackedScene>("res://Traps/Trap3.tscn");

		var trapInstance = (Trap3)packedScene1.Instantiate();
        trapInstance.Position = MapToLocal(new Vector2I(2, 1));
        AddChild(trapInstance);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
