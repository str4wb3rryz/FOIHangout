namespace EntitiesLayer.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("dogadjaj")]
    public partial class dogadjaj
    {
        public int id { get; set; }

        [Required]
        [StringLength(255)]
        public string naziv { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string opis { get; set; }

        public DateTime? datum_pocetka { get; set; }

        public DateTime? datum_zavrsetka { get; set; }

        [StringLength(255)]
        public string poseban_dogadjaj { get; set; }

        public bool? zavrsen { get; set; }

        public int? id_korisnika { get; set; }

        public virtual korisnik korisnik { get; set; }
    }
}
