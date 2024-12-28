using Godot;
using System;
using System.Collections.Generic;
public partial class TrampaBase : Area2D
{
    [Export] public int Damage = 20;              // Daño que inflige la trampa
    [Export] public float ActivationCooldown = 2; // Tiempo de espera entre activaciones
    protected bool _isActive = true;
    
    protected AnimatedSprite2D animatedSprite2D;

    public override void _Ready()
    {
        Visible = false;
        BodyEntered += OnBodyEntered;  // Conectar el evento cuando un cuerpo entra
        animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
    }

    // Método que pueden sobrescribir las subclases para añadir lógica particular.
    protected virtual void OnBodyEntered(Node body)
    {
        // Si la trampa no está activa, no hace nada
        if (!_isActive) 
            return;

        // Si el objeto que entra es el jugador, aplica daño
        if (body is Player1 player)
        {
            player.TakeDamage(Damage);
            Visible = true;
            animatedSprite2D.Play("default");

            // Inicia el cooldown
            _isActive = false;
            var cooldownTimer = new Godot.Timer
            {
                WaitTime = ActivationCooldown,
                OneShot = true
            };
            cooldownTimer.Timeout += ResetTrap;
            AddChild(cooldownTimer);
            cooldownTimer.Start();
        }
    }

    protected virtual void ResetTrap()
    {
        Visible = false;
        _isActive = true;
    }
}