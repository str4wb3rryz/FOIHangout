using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class kolegijRepository : Repository<kolegij>
    {
        public kolegijRepository() : base(new FOIHangoutModel())
        {

        }

        public IQueryable<kolegij> GetKolegijById(int id)
        {
            var query = from e in Entities where e.id == id select e;
            return query;
        }

        public override int Add(kolegij entity, bool saveChanges = true)
        {
            var kolegij = new kolegij();
            kolegij.naziv = entity.naziv;
            kolegij.opis = entity.opis;

            Entities.Add(kolegij);

            if (saveChanges)
            {
                return SaveChanges();
            }
            else
            {
                return 0;
            }
        }

        public override int Update(kolegij entity, bool saveChanges = true)
        {
            var kolegij = Entities.SingleOrDefault(i => i.id == entity.id);
            kolegij.naziv = entity.naziv;
            kolegij.opis = entity.opis;


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
