using Godot;
using System;

public partial class Camera2d1 : Camera2D
{
	private Node2D target;
    public override void _Ready()
    {
         MakeCurrent();
    }
   public override void _Process(double delta)
{
    if (target != null)
    {
        Position = target.GlobalPosition;
    }
    else
    {
        GD.PrintErr("El objetivo (target) no está asignado.");
    }
}

    public void SetTarget(Node2D newTarget)
    {
        target = newTarget; 
    }
}
