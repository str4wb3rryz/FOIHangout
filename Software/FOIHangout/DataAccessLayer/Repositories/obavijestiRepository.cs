using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class obavijestiRepository : Repository<obavijest>
    {
        public obavijestiRepository() : base(new FOIHangoutModel())
        {
        }

        public IQueryable<obavijest> GetObavijestiById(int id)
        {
            var query = from p in Entities where p.id == id select p;
            return query;
        }
        public obavijest GetSpecificObavijestById(int id)
        {
            return Entities.FirstOrDefault(p => p.id == id);
        }
        public override int Add(obavijest entity, bool saveChanges = true)
        {
            var korisnik = Context.korisnik.SingleOrDefault(i => i.id == entity.id_korisnika);

            var obavijesti = new obavijest();

            obavijesti.id_korisnika = korisnik.id;
            obavijesti.naziv = entity.naziv;
            obavijesti.opis = entity.opis;
            obavijesti.datum_i_vrijeme_kreiranja = entity.datum_i_vrijeme_kreiranja;

            Entities.Add(obavijesti);
            if (saveChanges)
            {
                return SaveChanges();
            }
            else
            {
                return 0;
            }
        }

        public override int Update(obavijest entity, bool saveChanges = true)
        {
            var korisnik = Context.korisnik.SingleOrDefault(i => i.id == entity.id_korisnika);

            var obavijesti = Entities.SingleOrDefault(i => i.id == entity.id);

            obavijesti.id_korisnika = korisnik.id;
            obavijesti.naziv = entity.naziv;
            obavijesti.opis = entity.opis;
            obavijesti.datum_i_vrijeme_kreiranja = entity.datum_i_vrijeme_kreiranja;

            if (saveChanges)
            {
                return SaveChanges();
            }
            else
            {
                return 0;
            }
        }
    }
}
