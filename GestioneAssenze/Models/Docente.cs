using System;
using System.Collections.Generic;

namespace GestioneAssenze.Models;

public partial class Docente
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public string Cognome { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<Assenza> Assenzas { get; set; } = new List<Assenza>();

    public virtual ICollection<Svolge> Svolges { get; set; } = new List<Svolge>();
}
