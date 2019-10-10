using System;
using System.Collections.Generic;

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

        public virtual ICollection<Kupovina> Kupovina { get; set; }
    }
}
