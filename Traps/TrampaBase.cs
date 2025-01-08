using Godot;
using System;
using System.Collections.Generic;
public partial class TrampaBase : Area2D
{
    public int Damage = 1;              // Daño que inflige la trampa
    public float ActivationCooldown = 5; // Tiempo de espera entre activaciones
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

        if (body is PlayerBase player)
        {
        GD.Print("Entro a la trampa");
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
         
        
            player.TomarDano(1); // Daño fijo de 1
        }
    }

    protected virtual void ResetTrap()
    {
        Visible = false;
        _isActive = true;
    }
  
   

}