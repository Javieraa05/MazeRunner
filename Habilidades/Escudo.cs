using Godot;
using System;
using System.Collections.Generic;
public class Escudo : HabilidadBase
{
    public override string Nombre => "Escudo";
    public override float Enfriamiento=> 30f;

    protected override void Efecto(PlayerBase jugador)
    {
        if (jugador is PlayerBase)
        {
            // Implementa una lógica de escudo, como ignorar trampas
            GD.Print("activo el escudo");
            jugador.Escudo = true;
            jugador.GetTree().CreateTimer(20).Timeout += () => jugador.Escudo = false;
            jugador.EmitirNoticia("Haz activado el escudo");
        }
    }
}
