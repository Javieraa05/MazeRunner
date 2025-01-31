using Godot;
using System;
using System.Collections.Generic;
public partial class TrampaBase : Area2D
{
    public int Danno = 1;              // Daño que inflige la trampa
    public float TiempoEnfriamiento= 5; // Tiempo de espera entre activaciones
    protected bool EstaActiva = true;
    AudioStreamPlayer audioStreamPlayer;
    
    protected AnimatedSprite2D animatedSprite2D;

    public override void _Ready()
    {
        Visible = false;
        BodyEntered += OnBodyEntered;  // Conectar el evento cuando un cuerpo entra
        animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        audioStreamPlayer = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
    }

    // Método que pueden sobrescribir las subclases para añadir lógica particular.
    protected virtual void OnBodyEntered(Node body)
    {
    
        // Si la trampa no está activa, no hace nada
        if (!EstaActiva) 
            return;

        if (body is PlayerBase player)
        {
        GD.Print("Entro a la trampa");
        Visible = true;
        animatedSprite2D.Play("default");

        // Inicia el cooldown
        EstaActiva = false;
        GetTree().CreateTimer(TiempoEnfriamiento).Timeout += ResetTrap;
         
        if(!player.Escudo)
        {    
            player.ReducirVelocidad();
            if(player.Vida-1 <= 0)
            {
                player.EmitirNoticia("Te ha matado una trampa");
            }
            audioStreamPlayer.Play();
            player.TomarDano(Danno); 
            

        }
        }
    }

    protected virtual void ResetTrap()
    {
        Visible = false;
        EstaActiva = true;
    }
  
   

}