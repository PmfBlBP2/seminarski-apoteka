using System;
using System.Collections.Generic;

namespace Apoteka.Models
{
    public partial class Lijek
    {
        public Lijek()
        {
            Kupovina = new HashSet<Kupovina>();
        }

        public int LijekId { get; set; }
        public int DobavljacId { get; set; }
        public string Naziv { get; set; }
        public byte? NaRecept { get; set; }
        public decimal? Cijena { get; set; }
        public int? Kolicina { get; set; }

        public virtual Dobavljac Dobavljac { get; set; }
        public virtual ICollection<Kupovina> Kupovina { get; set; }
    }
}
