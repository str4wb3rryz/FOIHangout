namespace EntitiesLayer.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("obavijestREPORT")]
    public partial class obavijestREPORT
    {
        public int id { get; set; }

        public int? id_korisnika_koji_prijavljuje_korisnika { get; set; }

        public int? id_korisnika_koji_je_prijavljen { get; set; }

        [Column(TypeName = "text")]
        public string report_opis { get; set; }

        [StringLength(255)]
        public string report_naslov { get; set; }

        public virtual korisnik korisnik { get; set; }

        public virtual korisnik korisnik1 { get; set; }
    }
}
