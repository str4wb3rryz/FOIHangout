namespace EntitiesLayer.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class zahtjev_za_prijateljstvo
    {
        public int id { get; set; }

        public int? posiljatelj_id { get; set; }

        public int? primalac_id { get; set; }

        [StringLength(50)]
        public string status { get; set; }

        public bool? prihvacen { get; set; }

        public DateTime? datum_slanja { get; set; }

        public DateTime? datum_odgovora { get; set; }

        public virtual korisnik korisnik { get; set; }

        public virtual korisnik korisnik1 { get; set; }
    }
}
