using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class timRepository : Repository<tim>
    {
        public timRepository() : base(new FOIHangoutModel())
        {
        }

        public IQueryable<tim> GetTimById(int id)
        {
            var query = from e in Entities where e.id == id select e;
            return query;
        }

        public override int Add(tim entity, bool saveChanges = true)
        {
            var tim = new tim
            {
                kolegij_id = entity.kolegij_id,
                naziv = entity.naziv
            };

            Entities.Add(tim);

            if (saveChanges)
            {
                return SaveChanges();
            }
            else
            {
                return 0;
            }
        }

        public int AddClanoveTima(tim team, korisnik user, bool saveChanges = true)
        {
            var existingTeam = Entities.SingleOrDefault(t => t.id == team.id);
            if (existingTeam != null)
            {
                var existingUser = Context.korisnik.SingleOrDefault(u => u.id == user.id);
                if (existingUser != null)
                {
                    existingTeam.korisnik.Add(existingUser);
                }

                if (saveChanges)
                {
                    return SaveChanges();
                }
            }
            return 0;
        }

        public override int Update(tim entity, bool saveChanges = true)
        {
            var tim = Entities.SingleOrDefault(i => i.id == entity.id);
            if (tim != null)
            {
                tim.naziv = entity.naziv;
                tim.kolegij_id = entity.kolegij_id;

                if (saveChanges)
                {
                    return SaveChanges();
                }
            }
            return 0;
        }
    }
}
