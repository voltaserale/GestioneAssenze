using System;
using System.Collections.Generic;

namespace GestioneAssenze.Models;

public partial class Svolge
{
    public int Iddocente { get; set; }

    public int Idlezione { get; set; }

    public string? Tipologia { get; set; }

    public virtual Docente IddocenteNavigation { get; set; } = null!;

    public virtual Lezione IdlezioneNavigation { get; set; } = null!;
}
