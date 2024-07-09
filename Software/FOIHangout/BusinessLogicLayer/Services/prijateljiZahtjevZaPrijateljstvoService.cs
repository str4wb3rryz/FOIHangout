using DataAccessLayer.Repositories;
using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class prijateljiZahtjevZaPrijateljstvoService
    {
        public void SendFriendRequest(int loggedUserId, int userId)
        {
            using (var db = new zahtjev_za_prijateljstvoRepository())
            {
                var zahtjev = new zahtjev_za_prijateljstvo
                {
                    posiljatelj_id = loggedUserId,
                    primalac_id = userId,
                    status = "Poslano",
                    prihvacen = false,
                    datum_slanja = DateTime.Now
                };
                db.Add(zahtjev);
            }
        }

        public List<zahtjev_za_prijateljstvo> GetRecievedFriendRequests(int id)
        {
            using (var db = new zahtjev_za_prijateljstvoRepository())
            {
                var result = db.GetZahtjevZaPrijateljstvoById(id);
                return result.ToList();
            }
        }

        public List<zahtjev_za_prijateljstvo> GetRecievedFriendRequestsById(int id)
        {
            using (var db = new zahtjev_za_prijateljstvoRepository())
            {
                var result = db.GetZahtjevZaPrijateljstvoByUserId(id);
                return result.ToList();
            }
        }

        public void Update(zahtjev_za_prijateljstvo requestToUpdate)
        {
            using (var db = new zahtjev_za_prijateljstvoRepository())
            {
                db.Update(requestToUpdate);
            }
        }

        public List<zahtjev_za_prijateljstvo> GetSentFriendRequestsById(int id)
        {
            using (var db = new zahtjev_za_prijateljstvoRepository())
            {
                var result = db.GetZahtjevZaPrijateljstvoByUserId(id);
                return result.ToList();
            }
        }
        public List<zahtjev_za_prijateljstvo> GetSentFriendRequestsById2(int id)
        {
            using (var db = new zahtjev_za_prijateljstvoRepository())
            {
                var result = db.GetZahtjevZaPrijateljstvoByUserId2(id);
                return result.ToList();
            }
        }
    }
}
