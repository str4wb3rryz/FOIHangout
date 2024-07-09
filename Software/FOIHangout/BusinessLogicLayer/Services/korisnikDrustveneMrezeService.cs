using DataAccessLayer.Repositories;
using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class korisnikDrustveneMrezeService
    {
        public List<KorisnikDrustvenaMreza> GetSocialNetworksByUserId(int id)
        {
            using (var db = new KorisnikDrustvenaMrezaRepository())
            {
                return db.GetKorisnikDrustvenaMrezaByUserId(id).ToList();
            }
        }
        public void AddSocialNetwork(int? userId, int? socialMediaId, string socialMediaAccountName)
        {
            using (var db = new KorisnikDrustvenaMrezaRepository())
            {
                var socialNetwork = new KorisnikDrustvenaMreza
                {
                    id_korisnika = userId,
                    id_drustvene_mreze = socialMediaId,
                    naziv_racuna_na_drustvenoj_mrezi = socialMediaAccountName
                };

                db.Add(socialNetwork);
            }
        }
        public void UpdateSocialNetworkAccounts(int userId, List<KorisnikDrustvenaMreza> socialMediaAccounts)
        {
            using (var db = new KorisnikDrustvenaMrezaRepository())
            {
                db.UpdateAccounts(userId, socialMediaAccounts);
            }
        }
    }
}
