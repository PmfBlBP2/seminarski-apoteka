using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Apoteka.Models
{
    public partial class Lijek
    {
        public Lijek()
        {
            Kupovina = new HashSet<Kupovina>();
        }

        public int LijekId { get; set; }
        [Required]
        public int DobavljacId { get; set; }
        public string Naziv { get; set; }
        [Required]

        public byte? NaRecept { get; set; }
        [Required]
        public decimal? Cijena { get; set; }
        public int? Kolicina { get; set; }

        public virtual Dobavljac Dobavljac { get; set; }
        public virtual ICollection<Kupovina> Kupovina { get; set; }
    }
}
