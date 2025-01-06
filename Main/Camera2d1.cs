using Godot;
using System;

public partial class Camera2d1 : Camera2D
{
	private Node2D target;

    public override void _Ready()
    {
         MakeCurrent();
         LimitLeft = 164;
         LimitRight = 1843;
         LimitTop = 274;
         LimitBottom = 1923;
    }
   public override void _Process(double delta)
{
    if (target != null)
    {
         // Calcula la nueva posición
        var newPosition = target.GlobalPosition;

        // Restringe la posición según los límites
        newPosition.X = Mathf.Clamp(newPosition.X, LimitLeft, LimitRight);
        newPosition.Y = Mathf.Clamp(newPosition.Y, LimitTop, LimitBottom);

        Position = newPosition;
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
