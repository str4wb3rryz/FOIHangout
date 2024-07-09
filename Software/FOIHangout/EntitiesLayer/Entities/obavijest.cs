namespace EntitiesLayer.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("obavijest")]
    public partial class obavijest
    {
        public int id { get; set; }

        public int? id_korisnika { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string naziv { get; set; }

        [Column(TypeName = "text")]
        public string opis { get; set; }

        public DateTime? datum_i_vrijeme_kreiranja { get; set; }

        public virtual korisnik korisnik { get; set; }
    }
}
