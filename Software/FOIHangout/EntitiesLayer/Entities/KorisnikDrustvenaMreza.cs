namespace EntitiesLayer.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("KorisnikDrustvenaMreza")]
    public partial class KorisnikDrustvenaMreza
    {
        public int id { get; set; }

        public int? id_korisnika { get; set; }

        public int? id_drustvene_mreze { get; set; }

        [StringLength(255)]
        public string naziv_racuna_na_drustvenoj_mrezi { get; set; }

        public virtual drustvene_mreze drustvene_mreze { get; set; }

        public virtual korisnik korisnik { get; set; }
    }
}
