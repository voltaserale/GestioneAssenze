using System;
using System.Collections.Generic;

namespace GestioneAssenze.Models;

public partial class Studente
{
    public string Matricola { get; set; } = null!;

    public string Nome { get; set; } = null!;

    public string Cognome { get; set; } = null!;

    public DateTime Datanascita { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int Idclasse { get; set; }

    public virtual ICollection<Assenza> Assenzas { get; set; } = new List<Assenza>();

    public virtual Classe IdclasseNavigation { get; set; } = null!;
}
