using System;
using System.Collections.Generic;

namespace Apoteka.Models
{
    public partial class Osiguranik
    {
        public int OsiguranikId { get; set; }
        public string Jmbg { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public int GradId { get; set; }
        public string Adresa { get; set; }
        public string BrojTelefona { get; set; }

        public virtual Grad Grad { get; set; }
    }
}
