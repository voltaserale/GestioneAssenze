using System;
using System.Collections.Generic;

namespace GestioneAssenze.Models;

public partial class Classe
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public string Tipologia { get; set; } = null!;

    public string Indirizzo { get; set; } = null!;

    public virtual ICollection<Lezione> Leziones { get; set; } = new List<Lezione>();

    public virtual ICollection<Studente> Studentes { get; set; } = new List<Studente>();
}
