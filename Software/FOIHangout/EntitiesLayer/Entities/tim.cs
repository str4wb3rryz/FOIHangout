namespace EntitiesLayer.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("tim")]
    public partial class tim
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tim()
        {
            korisnik = new HashSet<korisnik>();
        }

        public int id { get; set; }

        public int? kolegij_id { get; set; }

        [Required]
        [StringLength(255)]
        public string naziv { get; set; }

        public virtual kolegij kolegij { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<korisnik> korisnik { get; set; }
    }
}
