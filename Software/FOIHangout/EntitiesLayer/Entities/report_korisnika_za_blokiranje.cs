namespace EntitiesLayer.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class report_korisnika_za_blokiranje
    {
        public int id { get; set; }

        public int? id_korisnika_koji_prijavljuje_korisnika { get; set; }

        public int? id_korisnika_koji_je_prijavljen { get; set; }

        public virtual korisnik korisnik { get; set; }

        public virtual korisnik korisnik1 { get; set; }
    }
}
