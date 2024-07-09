using EntitiesLayer.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace DataAccessLayer
{
    public partial class FOIHangoutModel : DbContext
    {
        public FOIHangoutModel()
            : base("name=FOIHangoutModel")
        {
        }

        public virtual DbSet<anketa> anketa { get; set; }
        public virtual DbSet<dogadjaj> dogadjaj { get; set; }
        public virtual DbSet<drustvene_mreze> drustvene_mreze { get; set; }
        public virtual DbSet<forum> forum { get; set; }
        public virtual DbSet<interesi> interesi { get; set; }
        public virtual DbSet<kolegij> kolegij { get; set; }
        public virtual DbSet<korisnik> korisnik { get; set; }
        public virtual DbSet<korisnik_odabir_anketa> korisnik_odabir_anketa { get; set; }
        public virtual DbSet<KorisnikDrustvenaMreza> KorisnikDrustvenaMreza { get; set; }
        public virtual DbSet<obavijest> obavijest { get; set; }
        public virtual DbSet<obavijestREPORT> obavijestREPORT { get; set; }
        public virtual DbSet<odabir_selekcije> odabir_selekcije { get; set; }
        public virtual DbSet<prijatelji> prijatelji { get; set; }
        public virtual DbSet<report_korisnika_za_blokiranje> report_korisnika_za_blokiranje { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<tim> tim { get; set; }
        public virtual DbSet<uloga> uloga { get; set; }
        public virtual DbSet<zahtjev_za_prijateljstvo> zahtjev_za_prijateljstvo { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<anketa>()
                .Property(e => e.naslov)
                .IsUnicode(false);

            modelBuilder.Entity<anketa>()
                .Property(e => e.opis)
                .IsUnicode(false);

            modelBuilder.Entity<anketa>()
                .Property(e => e.izbor_visestruki)
                .IsUnicode(false);

            modelBuilder.Entity<anketa>()
                .HasMany(e => e.korisnik_odabir_anketa)
                .WithRequired(e => e.anketa)
                .HasForeignKey(e => e.id_anketa)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<anketa>()
                .HasMany(e => e.odabir_selekcije)
                .WithMany(e => e.anketa)
                .Map(m => m.ToTable("anketa_odabir_selekcije").MapLeftKey("id_anketa").MapRightKey("id_odabira"));

            modelBuilder.Entity<dogadjaj>()
                .Property(e => e.naziv)
                .IsUnicode(false);

            modelBuilder.Entity<dogadjaj>()
                .Property(e => e.opis)
                .IsUnicode(false);

            modelBuilder.Entity<dogadjaj>()
                .Property(e => e.poseban_dogadjaj)
                .IsUnicode(false);

            modelBuilder.Entity<drustvene_mreze>()
                .Property(e => e.naziv)
                .IsUnicode(false);

            modelBuilder.Entity<drustvene_mreze>()
                .HasMany(e => e.KorisnikDrustvenaMreza)
                .WithOptional(e => e.drustvene_mreze)
                .HasForeignKey(e => e.id_drustvene_mreze);

            modelBuilder.Entity<forum>()
                .Property(e => e.naslov)
                .IsUnicode(false);

            modelBuilder.Entity<forum>()
                .Property(e => e.opis)
                .IsUnicode(false);

            modelBuilder.Entity<forum>()
                .HasMany(e => e.forum1)
                .WithOptional(e => e.forum2)
                .HasForeignKey(e => e.id_roditeljskog_posta);

            modelBuilder.Entity<interesi>()
                .Property(e => e.naziv_interesa)
                .IsUnicode(false);

            modelBuilder.Entity<interesi>()
                .HasMany(e => e.korisnik)
                .WithMany(e => e.interesi)
                .Map(m => m.ToTable("korisnik_interesi").MapLeftKey("id_interesa").MapRightKey("id_korisnika"));

            modelBuilder.Entity<kolegij>()
                .Property(e => e.naziv)
                .IsUnicode(false);

            modelBuilder.Entity<kolegij>()
                .Property(e => e.opis)
                .IsUnicode(false);

            modelBuilder.Entity<kolegij>()
                .HasMany(e => e.tim)
                .WithOptional(e => e.kolegij)
                .HasForeignKey(e => e.kolegij_id);

            modelBuilder.Entity<korisnik>()
                .Property(e => e.ime)
                .IsUnicode(false);

            modelBuilder.Entity<korisnik>()
                .Property(e => e.prezime)
                .IsUnicode(false);

            modelBuilder.Entity<korisnik>()
                .Property(e => e.lozinka)
                .IsUnicode(false);

            modelBuilder.Entity<korisnik>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<korisnik>()
                .Property(e => e.spol)
                .IsUnicode(false);

            modelBuilder.Entity<korisnik>()
                .HasMany(e => e.anketa)
                .WithOptional(e => e.korisnik)
                .HasForeignKey(e => e.id_korisnika);

            modelBuilder.Entity<korisnik>()
                .HasMany(e => e.dogadjaj)
                .WithOptional(e => e.korisnik)
                .HasForeignKey(e => e.id_korisnika);

            modelBuilder.Entity<korisnik>()
                .HasMany(e => e.forum)
                .WithOptional(e => e.korisnik)
                .HasForeignKey(e => e.id_korisnika);

            modelBuilder.Entity<korisnik>()
                .HasMany(e => e.korisnik_odabir_anketa)
                .WithRequired(e => e.korisnik)
                .HasForeignKey(e => e.id_korisnik)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<korisnik>()
                .HasMany(e => e.KorisnikDrustvenaMreza)
                .WithOptional(e => e.korisnik)
                .HasForeignKey(e => e.id_korisnika);

            modelBuilder.Entity<korisnik>()
                .HasMany(e => e.obavijest)
                .WithOptional(e => e.korisnik)
                .HasForeignKey(e => e.id_korisnika);

            modelBuilder.Entity<korisnik>()
                .HasMany(e => e.obavijestREPORT)
                .WithOptional(e => e.korisnik)
                .HasForeignKey(e => e.id_korisnika_koji_prijavljuje_korisnika);

            modelBuilder.Entity<korisnik>()
                .HasMany(e => e.obavijestREPORT1)
                .WithOptional(e => e.korisnik1)
                .HasForeignKey(e => e.id_korisnika_koji_je_prijavljen);

            modelBuilder.Entity<korisnik>()
                .HasMany(e => e.prijatelji)
                .WithOptional(e => e.korisnik)
                .HasForeignKey(e => e.id_korisnika1);

            modelBuilder.Entity<korisnik>()
                .HasMany(e => e.prijatelji1)
                .WithOptional(e => e.korisnik1)
                .HasForeignKey(e => e.id_korisnika2);

            modelBuilder.Entity<korisnik>()
                .HasMany(e => e.report_korisnika_za_blokiranje)
                .WithOptional(e => e.korisnik)
                .HasForeignKey(e => e.id_korisnika_koji_prijavljuje_korisnika);

            modelBuilder.Entity<korisnik>()
                .HasMany(e => e.report_korisnika_za_blokiranje1)
                .WithOptional(e => e.korisnik1)
                .HasForeignKey(e => e.id_korisnika_koji_je_prijavljen);

            modelBuilder.Entity<korisnik>()
                .HasMany(e => e.zahtjev_za_prijateljstvo)
                .WithOptional(e => e.korisnik)
                .HasForeignKey(e => e.posiljatelj_id);

            modelBuilder.Entity<korisnik>()
                .HasMany(e => e.zahtjev_za_prijateljstvo1)
                .WithOptional(e => e.korisnik1)
                .HasForeignKey(e => e.primalac_id);

            modelBuilder.Entity<korisnik>()
                .HasMany(e => e.tim)
                .WithMany(e => e.korisnik)
                .Map(m => m.ToTable("clanovi").MapLeftKey("id_korisnika").MapRightKey("id_tim"));

            modelBuilder.Entity<KorisnikDrustvenaMreza>()
                .Property(e => e.naziv_racuna_na_drustvenoj_mrezi)
                .IsUnicode(false);

            modelBuilder.Entity<obavijest>()
                .Property(e => e.naziv)
                .IsUnicode(false);

            modelBuilder.Entity<obavijest>()
                .Property(e => e.opis)
                .IsUnicode(false);

            modelBuilder.Entity<obavijestREPORT>()
                .Property(e => e.report_opis)
                .IsUnicode(false);

            modelBuilder.Entity<obavijestREPORT>()
                .Property(e => e.report_naslov)
                .IsUnicode(false);

            modelBuilder.Entity<odabir_selekcije>()
                .Property(e => e.odabir)
                .IsUnicode(false);

            modelBuilder.Entity<odabir_selekcije>()
                .HasMany(e => e.korisnik_odabir_anketa)
                .WithRequired(e => e.odabir_selekcije)
                .HasForeignKey(e => e.id_odabira)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tim>()
                .Property(e => e.naziv)
                .IsUnicode(false);

            modelBuilder.Entity<uloga>()
                .Property(e => e.naziv)
                .IsUnicode(false);

            modelBuilder.Entity<uloga>()
                .Property(e => e.opis)
                .IsUnicode(false);

            modelBuilder.Entity<uloga>()
                .HasMany(e => e.korisnik)
                .WithOptional(e => e.uloga)
                .HasForeignKey(e => e.uloga_id);

            modelBuilder.Entity<zahtjev_za_prijateljstvo>()
                .Property(e => e.status)
                .IsUnicode(false);
        }
    }
}
