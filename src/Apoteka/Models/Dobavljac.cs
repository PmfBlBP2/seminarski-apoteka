using System;
using System.Collections.Generic;

namespace Apoteka.Models
{
    public partial class Dobavljac
    {
        public Dobavljac()
        {
            Lijek = new HashSet<Lijek>();
        }

        public int DobavljacId { get; set; }
        public int GradId { get; set; }
        public string Naziv { get; set; }
        public string Adresa { get; set; }
        public string Telefon { get; set; }
        public string EMail { get; set; }

        public virtual Grad Grad { get; set; }
        public virtual ICollection<Lijek> Lijek { get; set; }
    }
}
