using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Apoteka.Models
{
    public partial class Racun
    {
        public Racun()
        {
            Kupovina = new HashSet<Kupovina>();
        }

        public int RacunId { get; set; }
        public decimal? Iznos { get; set; }
        public DateTime? DatumIzdavanja { get; set; }
       
       
        [MinLength(13, ErrorMessage ="Unesite 13 cifara")]
        [MaxLength(13, ErrorMessage="Unesite 13 cifara")] 
        [RegularExpression("([0-9]+)", ErrorMessage ="Unesite samo cifre")]
        public string Jmbg { get; set; }

        public virtual ICollection<Kupovina> Kupovina { get; set; }
    }
}
