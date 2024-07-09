using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class interesiRepository : Repository<interesi>
    {
        public interesiRepository() : base(new FOIHangoutModel())
        {

        }

        public IQueryable<interesi> GetInteresById(int id)
        {
            var query = from e in Entities where e.id == id select e;
            return query;
        }

        public override int Add(interesi entity, bool saveChanges = true)
        {
            var interes = new interesi();
            interes.naziv_interesa = entity.naziv_interesa;

            Entities.Add(interes);

            if (saveChanges)
            {
                return SaveChanges();
            }
            else
            {
                return 0;
            }
        }

        public override int Update(interesi entity, bool saveChanges = true)
        {
            var interes = Entities.SingleOrDefault(i => i.id == entity.id);
            interes.naziv_interesa = entity.naziv_interesa;

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
