using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Apoteka.Models
{
    public partial class Kupovina
    {
        public int RacunId { get; set; }
        public int LijekId { get; set; }

        [Required(ErrorMessage ="Polje Kolicina je obavezno!")]
        [RegularExpression("([0-9]+)",ErrorMessage ="Unesite cijeli broj")]
        public int? Kolicina { get; set; }
        public decimal? Iznos { get; set; }

        public virtual Lijek Lijek { get; set; }
        public virtual Racun Racun { get; set; }
    }
}
