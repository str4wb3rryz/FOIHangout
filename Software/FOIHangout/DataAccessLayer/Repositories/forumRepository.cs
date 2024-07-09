using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class forumRepository : Repository<forum>
    {
        public forumRepository() : base(new FOIHangoutModel())
        {
        }
        public IQueryable<forum> GetForumById(int id)
        {
            var query = from p in Entities where p.id == id select p;
            return query;
        }
        public IQueryable<forum> GetForumOdgovorById(int idRoditelja)
        {
            var query = from p in Entities where p.id_roditeljskog_posta == idRoditelja select p;
            return query;
        }
        public override int Add(forum entity, bool saveChanges = true)
        {
            var forum = new forum();

            forum.id_korisnika = entity.id_korisnika;
            forum.naslov = entity.naslov;
            forum.opis = entity.opis;
            forum.datum_objave = entity.datum_objave;
            forum.id_roditeljskog_posta = entity.id_roditeljskog_posta;           

            Entities.Add(forum);
            if (saveChanges)
            {
                return SaveChanges();
            }
            else
            {
                return 0;
            }
        }

        public override int Update(forum entity, bool saveChanges = true)
        {
            var forum = Entities.SingleOrDefault(u => u.id == entity.id);
            forum.id_korisnika = entity.id_korisnika;
            forum.naslov = entity.naslov;
            forum.opis = entity.opis;
            forum.datum_objave = entity.datum_objave;
            forum.id_roditeljskog_posta = entity.id_roditeljskog_posta;

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
