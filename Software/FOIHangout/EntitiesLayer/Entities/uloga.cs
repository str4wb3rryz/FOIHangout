namespace EntitiesLayer.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("uloga")]
    public partial class uloga
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public uloga()
        {
            korisnik = new HashSet<korisnik>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(255)]
        public string naziv { get; set; }

        [Column(TypeName = "text")]
        public string opis { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<korisnik> korisnik { get; set; }
    }
}
