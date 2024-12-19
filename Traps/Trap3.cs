using Godot;

public partial class Trap3 : Area2D
{
    [Export] public int Damage = 20; // Daño que inflige la trampa
    [Export] public float ActivationCooldown = 2.0f; // Tiempo de espera entre activaciones
    AnimatedSprite2D animatedSprite2D;

    private bool _isActive = true; // Estado de la trampa

    public override void _Ready()
    {   Visible = false; // Oculta la trampa
        // Conectar el evento cuando un cuerpo entra en el área de la trampa
        this.BodyEntered += OnBodyEntered; // Godot 4 utiliza eventos
        animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
    }

    private void OnBodyEntered(Node body)
    {
        if (!_isActive)
            return;

        if (body is Player player) // Comprueba si el cuerpo es un jugador
        {
            
            // Aplica daño al jugador
            player.TakeDamage(Damage);
            Visible = true; // Muestra la trampa
            animatedSprite2D.Play("default");
            // Inicia el cooldown
            _isActive = false;
            var cooldownTimer = new Godot.Timer();
            cooldownTimer.WaitTime = ActivationCooldown;
            cooldownTimer.OneShot = true;
            cooldownTimer.Timeout += ResetTrap; // Godot 4 utiliza eventos
            AddChild(cooldownTimer);
            cooldownTimer.Start();
        }
    }

    private void ResetTrap()
    {
        Visible = false; // Oculta la trampa
        _isActive = true;
        
    }
}
