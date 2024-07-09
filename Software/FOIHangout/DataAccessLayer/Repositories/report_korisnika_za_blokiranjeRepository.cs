using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class report_korisnika_za_blokiranjeRepository : Repository<report_korisnika_za_blokiranje>
    {
        public report_korisnika_za_blokiranjeRepository() : base(new FOIHangoutModel())
        {
        }

        public IQueryable<report_korisnika_za_blokiranje> GetReportKorisnikZaBlokiranjeById(int id)
        {
            var query = from p in Entities where p.id == id select p;
            return query;
        }

        public override int Add(report_korisnika_za_blokiranje entity, bool saveChanges = true)
        {
            var reportKorisnikaZaBlokiranje = new report_korisnika_za_blokiranje();

            reportKorisnikaZaBlokiranje.id_korisnika_koji_prijavljuje_korisnika = entity.id_korisnika_koji_prijavljuje_korisnika;
            reportKorisnikaZaBlokiranje.id_korisnika_koji_je_prijavljen = entity.id_korisnika_koji_je_prijavljen;           

            Entities.Add(reportKorisnikaZaBlokiranje);
            if (saveChanges)
            {
                return SaveChanges();
            }
            else
            {
                return 0;
            }
        }

        public override int Update(report_korisnika_za_blokiranje entity, bool saveChanges = true)
        {
            var reportKorisnikaZaBlokiranje = Entities.SingleOrDefault(u => u.id == entity.id);

            reportKorisnikaZaBlokiranje.id_korisnika_koji_prijavljuje_korisnika = entity.id_korisnika_koji_prijavljuje_korisnika;
            reportKorisnikaZaBlokiranje.id_korisnika_koji_je_prijavljen = entity.id_korisnika_koji_je_prijavljen;

            if (saveChanges)
            {
                return SaveChanges();
            }
            else
            {
                return 0;
            }
        }

        public IQueryable<report_korisnika_za_blokiranje> GetUsersConnectedToId(int id_korisnika_koji_prijavljuje_korisnika)
        {
            var query = from p in Entities where p.id_korisnika_koji_prijavljuje_korisnika == id_korisnika_koji_prijavljuje_korisnika select p;
            return query;
        }

        public bool IsUserBlocked(int id1, int id2)
        {
            var query = from p in Entities
                        where (p.id_korisnika_koji_prijavljuje_korisnika == id1 && p.id_korisnika_koji_je_prijavljen == id2)
                        select p;

            return query.Any();
        }

        public void UnblockUser(int id1, int id2)
        {
            var reportKorisnikaZaBlokiranje = Entities.SingleOrDefault(u => u.id_korisnika_koji_prijavljuje_korisnika == id1 && u.id_korisnika_koji_je_prijavljen == id2);

            if (reportKorisnikaZaBlokiranje != null)
            {
                Entities.Remove(reportKorisnikaZaBlokiranje);
                SaveChanges();
            }
        }

        public async Task<bool> IsUserBlockedAsync(int id, int value)
        {
            var query = await Task.Run(() =>
            {
                return from p in Entities
                       where (p.id_korisnika_koji_prijavljuje_korisnika == id && p.id_korisnika_koji_je_prijavljen == value)
                       select p;
            });

            return query.Any();
        }
    }
}
