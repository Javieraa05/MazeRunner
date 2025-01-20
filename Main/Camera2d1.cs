using Godot;
using System;

public partial class Camera2d1 : Camera2D
{
	private PlayerBase target;
    

    public override void _Ready()
    {
         MakeCurrent();
         LimitLeft = 0;
         LimitRight = 1843;
         LimitTop = 0;
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
        Zoom = target.Zoom;
        
    }
    else
    {
        GD.PrintErr("El objetivo (target) no está asignado.");
    }
}

    public void SetTarget(PlayerBase newTarget)
    {
        target = newTarget; 
    }
}
