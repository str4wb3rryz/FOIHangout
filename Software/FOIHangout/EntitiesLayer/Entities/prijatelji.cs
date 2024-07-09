namespace EntitiesLayer.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("prijatelji")]
    public partial class prijatelji
    {
        public int id { get; set; }

        public int? id_korisnika1 { get; set; }

        public int? id_korisnika2 { get; set; }

        public virtual korisnik korisnik { get; set; }

        public virtual korisnik korisnik1 { get; set; }
    }
}
