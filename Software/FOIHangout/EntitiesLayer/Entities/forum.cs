namespace EntitiesLayer.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("forum")]
    public partial class forum
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public forum()
        {
            forum1 = new HashSet<forum>();
        }

        public int id { get; set; }

        public int? id_korisnika { get; set; }

        [Column(TypeName = "text")]
        public string naslov { get; set; }

        [Column(TypeName = "text")]
        public string opis { get; set; }

        public DateTime? datum_objave { get; set; }

        public int? id_roditeljskog_posta { get; set; }

        public virtual korisnik korisnik { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<forum> forum1 { get; set; }

        public virtual forum forum2 { get; set; }
    }
}
