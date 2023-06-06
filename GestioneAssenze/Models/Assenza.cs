using System;
using System.Collections.Generic;

namespace GestioneAssenze.Models;

public partial class Assenza
{
    public int Id { get; set; }

    public DateTime? Data { get; set; }

    public string Tipo { get; set; } = null!;

    public string Matrstudente { get; set; } = null!;

    public int Iddocente { get; set; }

    public TimeSpan? Ora { get; set; }

    public string Durata { get; set; } = null!;

    public virtual Docente IddocenteNavigation { get; set; } = null!;

    public virtual Studente MatrstudenteNavigation { get; set; } = null!;
}
