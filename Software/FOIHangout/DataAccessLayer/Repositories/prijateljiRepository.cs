using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class prijateljiRepository : Repository<prijatelji>
    {
        public prijateljiRepository() : base(new FOIHangoutModel())
        {
        }
        public IQueryable<prijatelji> GetPrijateljById(int id)
        {
            var query = from p in Entities where p.id_korisnika1 == id || p.id_korisnika2==id select p;
            return query;
        }

        public override int Add(prijatelji entity, bool saveChanges = true)
        {
            var prijatelj = new prijatelji();

            prijatelj.id_korisnika1 = entity.id_korisnika1;
            prijatelj.id_korisnika2 = entity.id_korisnika2;

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

        public override int Update(prijatelji entity, bool saveChanges = true)
        {
            var prijatelj = Entities.SingleOrDefault(u => u.id == entity.id);

            prijatelj.id_korisnika1 = entity.id_korisnika1;
            prijatelj.id_korisnika2 = entity.id_korisnika2;

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
