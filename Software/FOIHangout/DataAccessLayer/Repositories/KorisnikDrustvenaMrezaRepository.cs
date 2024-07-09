using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class KorisnikDrustvenaMrezaRepository : Repository<KorisnikDrustvenaMreza>
    {
        public KorisnikDrustvenaMrezaRepository() : base(new FOIHangoutModel())
        {

        }

        public IQueryable<KorisnikDrustvenaMreza> GetKorisnikDrustvenaMrezaById(int id)
        {
            var query = from p in Entities where p.id == id select p;
            return query;
        }

        public IQueryable<KorisnikDrustvenaMreza> GetKorisnikDrustvenaMrezaByUserId(int id)
        {
            var query = from p in Entities where p.id_korisnika == id select p;
            return query;
        }

        public override int Add(KorisnikDrustvenaMreza entity, bool saveChanges = true)
        {
            var korisnik = Context.korisnik.SingleOrDefault(u => u.id == entity.id_korisnika);
            var drustvenaMreza = Context.drustvene_mreze.SingleOrDefault(m => m.id == entity.id_drustvene_mreze);

            var existingRecord = Entities.SingleOrDefault(u => u.id_korisnika == entity.id_korisnika && u.id_drustvene_mreze == entity.id_drustvene_mreze);

            if (existingRecord == null)
            {
                var newRecord = new KorisnikDrustvenaMreza
                {
                    id_korisnika = korisnik?.id,
                    id_drustvene_mreze = drustvenaMreza?.id,
                    naziv_racuna_na_drustvenoj_mrezi = entity.naziv_racuna_na_drustvenoj_mrezi
                };

                Entities.Add(newRecord);

                if (saveChanges)
                {
                    return SaveChanges();
                }
            }

            return 0;
        }

        public override int Update(KorisnikDrustvenaMreza entity, bool saveChanges = true)
        {
            var existingRecord = Entities.SingleOrDefault(u => u.id == entity.id);

            if (existingRecord != null)
            {
                existingRecord.id_korisnika = entity.id_korisnika;
                existingRecord.id_drustvene_mreze = entity.id_drustvene_mreze;
                existingRecord.naziv_racuna_na_drustvenoj_mrezi = entity.naziv_racuna_na_drustvenoj_mrezi;

                if (saveChanges)
                {
                    return SaveChanges();
                }
            }

            return 0;
        }

        public void UpdateAccounts(int userId, List<KorisnikDrustvenaMreza> socialMediaAccounts, bool saveChanges = true)
        {
            var existingAccounts = GetKorisnikDrustvenaMrezaByUserId(userId).ToList();
            
            foreach (var account in socialMediaAccounts)
            {
                var existingAccount = existingAccounts.FirstOrDefault(ea => ea.id_drustvene_mreze == account.id_drustvene_mreze);
                if (existingAccount != null)
                {

                    existingAccount.naziv_racuna_na_drustvenoj_mrezi = account.naziv_racuna_na_drustvenoj_mrezi;
                    Update(existingAccount, false);
                }
                else
                {
                    Add(account, false);
                }
            }
            var accountsToRemove = existingAccounts.Where(ea => !socialMediaAccounts.Any(ua => ua.id_drustvene_mreze == ea.id_drustvene_mreze)).ToList();
            foreach (var account in accountsToRemove)
            {
                Remove(account, false);
            }

            if (saveChanges)
            {
                SaveChanges();
            }
        }
    }
}
