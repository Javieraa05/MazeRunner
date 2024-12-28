using Godot;

public partial class Camera2d : Camera2D
{
    private Node2D target;

    public override void _PhysicsProcess(double delta)
    {
        if (target != null)
        {
            Position = target.Position;
        }
    }

    public void SetTarget(Node2D newTarget)
    {
        target = newTarget;
    }
}
