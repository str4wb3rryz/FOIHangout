using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class drustvene_mrezeRepository : Repository<drustvene_mreze>
    {
        public drustvene_mrezeRepository() : base(new FOIHangoutModel())
        {

        }

        public IQueryable<drustvene_mreze> GetDrustvenaMrezaById(int id)
        {
            var query = from e in Entities where e.id == id select e;
            return query;
        }

        public override int Add(drustvene_mreze entity, bool saveChanges = true)
        {
            var drustvenaMreza = new drustvene_mreze();
            drustvenaMreza.naziv = entity.naziv;
           
            Entities.Add(drustvenaMreza);

            if (saveChanges)
            {
                return SaveChanges();
            }
            else
            {
                return 0;
            }
        }

        public override int Update(drustvene_mreze entity, bool saveChanges = true)
        {
            var drustvenaMreza = Entities.SingleOrDefault(u => u.id == entity.id);
            drustvenaMreza.naziv = entity.naziv;
            
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
