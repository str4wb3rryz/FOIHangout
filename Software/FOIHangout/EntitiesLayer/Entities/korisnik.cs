namespace EntitiesLayer.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("korisnik")]
    public partial class korisnik
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public korisnik()
        {
            anketa = new HashSet<anketa>();
            dogadjaj = new HashSet<dogadjaj>();
            forum = new HashSet<forum>();
            korisnik_odabir_anketa = new HashSet<korisnik_odabir_anketa>();
            KorisnikDrustvenaMreza = new HashSet<KorisnikDrustvenaMreza>();
            obavijest = new HashSet<obavijest>();
            obavijestREPORT = new HashSet<obavijestREPORT>();
            obavijestREPORT1 = new HashSet<obavijestREPORT>();
            prijatelji = new HashSet<prijatelji>();
            prijatelji1 = new HashSet<prijatelji>();
            report_korisnika_za_blokiranje = new HashSet<report_korisnika_za_blokiranje>();
            report_korisnika_za_blokiranje1 = new HashSet<report_korisnika_za_blokiranje>();
            zahtjev_za_prijateljstvo = new HashSet<zahtjev_za_prijateljstvo>();
            zahtjev_za_prijateljstvo1 = new HashSet<zahtjev_za_prijateljstvo>();
            tim = new HashSet<tim>();
            interesi = new HashSet<interesi>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(255)]
        public string ime { get; set; }

        [Required]
        [StringLength(255)]
        public string prezime { get; set; }

        [Required]
        [StringLength(255)]
        public string lozinka { get; set; }

        [Required]
        [StringLength(255)]
        public string email { get; set; }

        [StringLength(100)]
        public string spol { get; set; }

        [Column(TypeName = "date")]
        public DateTime? datum_rodenja { get; set; }

        public bool? banan { get; set; }

        public int? uloga_id { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<anketa> anketa { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<dogadjaj> dogadjaj { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<forum> forum { get; set; }

        public virtual uloga uloga { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KorisnikDrustvenaMreza> KorisnikDrustvenaMreza { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<obavijest> obavijest { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<obavijestREPORT> obavijestREPORT { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<obavijestREPORT> obavijestREPORT1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<prijatelji> prijatelji { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<prijatelji> prijatelji1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<report_korisnika_za_blokiranje> report_korisnika_za_blokiranje { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<report_korisnika_za_blokiranje> report_korisnika_za_blokiranje1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<zahtjev_za_prijateljstvo> zahtjev_za_prijateljstvo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<zahtjev_za_prijateljstvo> zahtjev_za_prijateljstvo1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tim> tim { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<interesi> interesi { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<korisnik_odabir_anketa> korisnik_odabir_anketa { get; set; }
    }
}
