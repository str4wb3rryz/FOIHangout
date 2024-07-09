namespace EntitiesLayer.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class drustvene_mreze
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public drustvene_mreze()
        {
            KorisnikDrustvenaMreza = new HashSet<KorisnikDrustvenaMreza>();
        }

        public int id { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string naziv { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KorisnikDrustvenaMreza> KorisnikDrustvenaMreza { get; set; }
    }
}
