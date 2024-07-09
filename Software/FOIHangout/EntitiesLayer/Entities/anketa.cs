namespace EntitiesLayer.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("anketa")]
    public partial class anketa
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public anketa()
        {
            korisnik_odabir_anketa = new HashSet<korisnik_odabir_anketa>();
            odabir_selekcije = new HashSet<odabir_selekcije>();
        }

        public int id { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string naslov { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string opis { get; set; }

        public bool? izbor_da_ne { get; set; }

        [Column(TypeName = "text")]
        public string izbor_visestruki { get; set; }

        public int? id_korisnika { get; set; }

        public bool? zatvoren { get; set; }

        public virtual korisnik korisnik { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<korisnik_odabir_anketa> korisnik_odabir_anketa { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<odabir_selekcije> odabir_selekcije { get; set; }
    }
}
