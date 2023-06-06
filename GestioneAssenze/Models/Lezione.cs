using System;
using System.Collections.Generic;

namespace GestioneAssenze.Models;

public partial class Lezione
{
    public int Id { get; set; }

    public DateTime Data { get; set; }

    public TimeSpan Ora { get; set; }

    public string Materia { get; set; } = null!;

    public string? Argomento { get; set; }

    public int Idclasse { get; set; }

    public virtual Classe IdclasseNavigation { get; set; } = null!;

    public virtual ICollection<Svolge> Svolges { get; set; } = new List<Svolge>();
}
