using Godot;
using System;

public partial class Camera2d1 : Camera2D
{
	private PlayerBase Objetivo;
    

    public override void _Ready()
    {
         MakeCurrent();
         LimitLeft = 0;
         LimitRight = 1680;
         LimitTop = 0;
         LimitBottom = 1650;
    }
   public override void _Process(double delta)
{
    if (Objetivo != null)
    {
         // Calcula la nueva posición
        var newPosition = Objetivo.GlobalPosition;

        // Restringe la posición según los límites
        newPosition.X = Mathf.Clamp(newPosition.X, LimitLeft, LimitRight);
        newPosition.Y = Mathf.Clamp(newPosition.Y, LimitTop, LimitBottom);

        Position = newPosition;
        Zoom = Objetivo.Zoom;
        
    }
    else
    {
        GD.PrintErr("El objetivo (Objetivo) no está asignado.");
    }
}

    public void SetTarget(PlayerBase newTarget)
    {
        Objetivo = newTarget; 
    }
}
