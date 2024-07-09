namespace EntitiesLayer.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class odabir_selekcije
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public odabir_selekcije()
        {
            korisnik_odabir_anketa = new HashSet<korisnik_odabir_anketa>();
            anketa = new HashSet<anketa>();
        }

        public int id { get; set; }

        [Column(TypeName = "text")]
        public string odabir { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<korisnik_odabir_anketa> korisnik_odabir_anketa { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<anketa> anketa { get; set; }
    }
}
