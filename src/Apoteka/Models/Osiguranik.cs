using System;
using System.Collections.Generic;

namespace Apoteka.Models
{
    public partial class Osiguranik
    {
        public Osiguranik()
        {
            Racun = new HashSet<Racun>();
        }

        public int OsiguranikId { get; set; }
        public string Jmbg { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public int GradId { get; set; }
        public string Adresa { get; set; }
        public string BrojTelefona { get; set; }

        public virtual Grad Grad { get; set; }
        public virtual ICollection<Racun> Racun { get; set; }
    }
}
