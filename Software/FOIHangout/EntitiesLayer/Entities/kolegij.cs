namespace EntitiesLayer.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("kolegij")]
    public partial class kolegij
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public kolegij()
        {
            tim = new HashSet<tim>();
        }

        public int id { get; set; }

        [StringLength(255)]
        public string naziv { get; set; }

        [Column(TypeName = "text")]
        public string opis { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tim> tim { get; set; }
    }
}
