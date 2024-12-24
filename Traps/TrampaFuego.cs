using Godot;
using System;
using System.Collections.Generic;
public partial class TrampaFuego : TrampaBase
{
    // Si necesitas lógica adicional o distinta para la trampa de fuego,
    // puedes sobrescribir los métodos base.
    protected override void OnBodyEntered(Node body)
    {
        // Ejemplo: Podrías hacer algo diferente antes o después
        base.OnBodyEntered(body);
    }
}