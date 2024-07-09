using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class odabir_selekcijeRepository : Repository<odabir_selekcije>
    {
        public odabir_selekcijeRepository() : base(new FOIHangoutModel())
        {
        }

        public IQueryable<odabir_selekcije> GetOdabirSelekcijeById(int id)
        {
            var query = from p in Entities where p.id == id select p;
            return query;
        }

        public override int Add(odabir_selekcije entity, bool saveChanges = true)
        {
            var odabirSelekcije = new odabir_selekcije();

            odabirSelekcije.odabir = entity.odabir;

            Entities.Add(odabirSelekcije);
            if (saveChanges)
            {
                return SaveChanges();
            }
            else
            {
                return 0;
            }
        }
        public override int Update(odabir_selekcije entity, bool saveChanges = true)
        {
            var odabirSelekcije = Entities.SingleOrDefault(u => u.id == entity.id);

            odabirSelekcije.odabir = entity.odabir;

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
