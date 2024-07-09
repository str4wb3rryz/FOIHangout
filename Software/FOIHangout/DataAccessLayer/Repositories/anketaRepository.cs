using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace DataAccessLayer.Repositories
{
    public class anketaRepository : Repository<anketa>
    {
        public anketaRepository() : base(new FOIHangoutModel())
        {
        }

        public IQueryable<anketa> GetAnketaById(int id)
        {
            var query = from p in Entities where p.id == id select p;
            return query;
        }

        public IQueryable<odabir_selekcije> GetPollChoicesById(int id)
        {
            var query = from anketa in Context.anketa
                        where anketa.id == id
                        from odabir_selekcije in anketa.odabir_selekcije
                        select odabir_selekcije;
            return query;
        }
        public override int Add(anketa entity, bool saveChanges = true)
        {
            int n = 0;
            var newAnketa = new anketa
            {
                naslov = entity.naslov,
                opis = entity.opis,
                izbor_da_ne = entity.izbor_da_ne,
                izbor_visestruki = entity.izbor_visestruki,
                id_korisnika = entity.id_korisnika,
                zatvoren = entity.zatvoren
            };

            if(entity.izbor_visestruki != null)
            {
                var choices = entity.izbor_visestruki.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                foreach(var choice in choices)
                {
                    n++;
                }
                var lastAddedChoices = Context.odabir_selekcije.OrderByDescending(o => o.id).Take(n).ToList();

                foreach (var odabir in lastAddedChoices)
                {
                    newAnketa.odabir_selekcije.Add(odabir);
                }
            } else if (entity.izbor_da_ne != null)
            {
                newAnketa.odabir_selekcije.Add(Context.odabir_selekcije.SingleOrDefault(o => o.id == 14));
                newAnketa.odabir_selekcije.Add(Context.odabir_selekcije.SingleOrDefault(o => o.id == 15));
            }

            Entities.Add(newAnketa);
            if (saveChanges)
            {
                return SaveChanges();
            }
            return 0;
        }

        public override int Update(anketa entity, bool saveChanges = true)
        {
            var korisnik = Context.korisnik.SingleOrDefault(i => i.id == entity.id_korisnika);

            var anketa = Entities.SingleOrDefault(u => u.id == entity.id);

            anketa.naslov = entity.naslov;
            anketa.opis = entity.opis;
            anketa.izbor_da_ne = entity.izbor_da_ne;
            anketa.izbor_visestruki = entity.izbor_visestruki;
            anketa.id_korisnika = entity.id_korisnika;
            anketa.zatvoren = entity.zatvoren;

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
