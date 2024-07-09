using DataAccessLayer;
using DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLayer.Entities
{
    public class zahtjev_za_prijateljstvoRepository : Repository<zahtjev_za_prijateljstvo>
    {
        public zahtjev_za_prijateljstvoRepository() : base(new FOIHangoutModel())
        {
        }

        public IQueryable<zahtjev_za_prijateljstvo> GetZahtjevZaPrijateljstvoById(int id)
        {
            var query = from p in Entities where p.id == id select p;
            return query;
        }
        public IQueryable<zahtjev_za_prijateljstvo> GetZahtjevZaPrijateljstvoByUserId(int id)
        {
            var query = from p in Entities where p.primalac_id == id select p;
            return query;
        }

        public IQueryable<zahtjev_za_prijateljstvo> GetZahtjevZaPrijateljstvoByUserId2(int id)
        {
            var query = from p in Entities where p.posiljatelj_id == id select p;
            return query;
        }

        public override int Add(zahtjev_za_prijateljstvo entity, bool saveChanges = true)
        {
            var korisnikPosiljatelj = Context.korisnik.SingleOrDefault(i => i.id == entity.posiljatelj_id);
            var korisnikPrimalac = Context.korisnik.SingleOrDefault(i => i.id == entity.primalac_id);

            var prijatelj = new zahtjev_za_prijateljstvo();

            prijatelj.posiljatelj_id = korisnikPosiljatelj.id;
            prijatelj.primalac_id = korisnikPrimalac.id;
            prijatelj.status = entity.status;
            prijatelj.prihvacen = entity.prihvacen;
            prijatelj.datum_slanja = entity.datum_slanja;
            prijatelj.datum_odgovora = entity.datum_odgovora;

            Entities.Add(prijatelj);
            if (saveChanges)
            {
                return SaveChanges();
            }
            else
            {
                return 0;
            }
        }

        public override int Update(zahtjev_za_prijateljstvo entity, bool saveChanges = true)
        {
            var korisnikPosiljatelj = Context.korisnik.SingleOrDefault(i => i.id == entity.posiljatelj_id);
            var korisnikPrimalac = Context.korisnik.SingleOrDefault(i => i.id == entity.primalac_id);

            var prijatelj = Entities.SingleOrDefault(u => u.id == entity.id);

            prijatelj.posiljatelj_id = korisnikPosiljatelj.id;
            prijatelj.primalac_id = korisnikPrimalac.id;
            prijatelj.status = entity.status;
            prijatelj.prihvacen = entity.prihvacen;
            prijatelj.datum_slanja = entity.datum_slanja;
            prijatelj.datum_odgovora = entity.datum_odgovora;

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
