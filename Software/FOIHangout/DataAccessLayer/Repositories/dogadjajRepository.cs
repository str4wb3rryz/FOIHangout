using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class dogadjajRepository : Repository<dogadjaj>
    {
        public dogadjajRepository() : base(new FOIHangoutModel())
        {

        }
        public IQueryable<dogadjaj> GetDogadjajById(int id)
        {
            var query = from p in Entities where p.id == id select p;
            return query;
        }
        public dogadjaj GetSpecificDogadjajById(int id)
        {
            return Entities.FirstOrDefault(p => p.id == id);
        }

        public override int Add(dogadjaj entity, bool saveChanges = true)
        {
            var interesi = Context.interesi.SingleOrDefault(i => i.id == entity.id);
            var korisnik = Context.korisnik.SingleOrDefault(i=>i.id == entity.id_korisnika);

            var dogadjaj = new dogadjaj();

            dogadjaj.naziv = entity.naziv;
            dogadjaj.opis = entity.opis;
            dogadjaj.datum_pocetka = entity.datum_pocetka;
            dogadjaj.datum_zavrsetka = entity.datum_zavrsetka;
            dogadjaj.poseban_dogadjaj = entity.poseban_dogadjaj;
            dogadjaj.zavrsen = entity.zavrsen;
            dogadjaj.id_korisnika = korisnik.id;                       

            Entities.Add(dogadjaj);
            if (saveChanges)
            {
                return SaveChanges();
            }
            else
            {
                return 0;
            }
        }

        public override int Update(dogadjaj entity, bool saveChanges = true)
        {
            var dogadjaj = Entities.SingleOrDefault(u => u.id == entity.id);
            var korisnik = Context.korisnik.SingleOrDefault(i => i.id == entity.id_korisnika);

            dogadjaj.naziv = entity.naziv;
            dogadjaj.opis = entity.opis;
            dogadjaj.datum_pocetka = entity.datum_pocetka;
            dogadjaj.datum_zavrsetka = entity.datum_zavrsetka;
            dogadjaj.poseban_dogadjaj = entity.poseban_dogadjaj;
            dogadjaj.zavrsen = entity.zavrsen;
            dogadjaj.id_korisnika = korisnik.id;           

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
