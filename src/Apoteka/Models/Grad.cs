using System;
using System.Collections.Generic;

namespace Apoteka.Models
{
    public partial class Grad
    {
        public Grad()
        {
            Dobavljac = new HashSet<Dobavljac>();
            Osiguranik = new HashSet<Osiguranik>();
        }

        public int GradId { get; set; }
        public int DrzavaId { get; set; }
        public string Naziv { get; set; }
        public string PostanskiBroj { get; set; }

        public virtual Drzava Drzava { get; set; }
        public virtual ICollection<Dobavljac> Dobavljac { get; set; }
        public virtual ICollection<Osiguranik> Osiguranik { get; set; }
    }
}
