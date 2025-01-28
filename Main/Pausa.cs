using Godot;
using System;

public partial class Pausa : CanvasLayer
{
	bool pausa = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(Input.IsActionJustPressed("pausa"))
		{
			if(!pausa) 
			{
				GetTree().Paused = true;
				Visible = true;
				pausa = true;
			}
			else
			{
				GetTree().Paused = false;
				Visible = false;
				pausa = false;
			}
		}
	}

}
