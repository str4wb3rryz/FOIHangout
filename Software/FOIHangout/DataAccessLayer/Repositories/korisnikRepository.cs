using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class korisnikRepository : Repository<korisnik>
    {
        public korisnikRepository() : base (new FOIHangoutModel())
        {
            
        }

        public IQueryable<korisnik> GetKorisnikById(int id)
        {
            var query = from p in Entities where p.id == id select p;
            return query;
        }

        public IQueryable<korisnik> GetKorisnikByEmail(string email)
        {
            var query = from p in Entities where p.email == email select p;
            return query;
        }

        public IQueryable<interesi> GetInterestsByUserId(int id)
        {
            var query = from korisnik in Context.korisnik
                        where korisnik.id == id
                        from interesi in korisnik.interesi
                        select interesi;

            return query;
        }
        

        public override int Add(korisnik entity, bool saveChanges = true)
        {
            
            var interesi = Context.interesi.SingleOrDefault(i => i.id == entity.id);

            var user = new korisnik();

            user.ime = entity.ime;
            user.prezime = entity.prezime;
            user.email = entity.email;
            user.lozinka = entity.lozinka;
            user.spol = entity.spol;
            user.datum_rodenja = entity.datum_rodenja;
            user.uloga = entity.uloga;
            user.banan = entity.banan;
            user.uloga_id = entity.uloga_id;

            foreach (var i in entity.interesi)
            {
                var postojeciInteres = Context.interesi.SingleOrDefault(interes => interes.id == i.id);
                if (postojeciInteres != null)
                {
                    user.interesi.Add(postojeciInteres);
                }
            }

            Entities.Add(user);
            if (saveChanges)
            {
                return SaveChanges();
            }
            else
            {
                return 0;
            }
        }


        public override int Update(korisnik entity, bool saveChanges = true)
        {
            var korisnik = Entities.SingleOrDefault(u=>u.id == entity.id);
            korisnik.ime = entity.ime;
            korisnik.prezime = entity.prezime;
            korisnik.email = entity.email;
            korisnik.lozinka = entity.lozinka;
            korisnik.spol=entity.spol;
            korisnik.datum_rodenja = entity.datum_rodenja;
            korisnik.uloga = entity.uloga;

            korisnik.interesi.Clear();
            var selectedInterests = Context.interesi.Where(i => entity.interesi.Select(e => e.id).Contains(i.id)).ToList();

            foreach (var interest in selectedInterests)
            {
                korisnik.interesi.Add(interest);
            }

            if (saveChanges)
            {
                return SaveChanges();
            }
            else
            {
                return 0;
            }
        }

        public int UpdateProfile(korisnik entity, bool saveChanges = true)
        {
            var korisnik = Entities.SingleOrDefault(u => u.id == entity.id);
            korisnik.ime = entity.ime;
            korisnik.prezime = entity.prezime;
            korisnik.lozinka = entity.lozinka;
            korisnik.spol = entity.spol;
            korisnik.datum_rodenja = entity.datum_rodenja;
            if (saveChanges)
            {
                return SaveChanges();
            }
            else
            {
                return 0;
            }
        }

        public int UpdateBanned(korisnik entity, bool saveChanges = true)
        {
            var korisnik = Entities.SingleOrDefault(u => u.id == entity.id);

            korisnik.banan=entity.banan;

            if (saveChanges)
            {
                return SaveChanges();
            }
            else
            {
                return 0;
            }
        }
        public int UpdateRole(korisnik entity, bool saveChanges = true)
        {
            var korisnik = Entities.SingleOrDefault(u => u.id == entity.id);

            korisnik.uloga = entity.uloga;

            if (saveChanges)
            {
                return SaveChanges();
            }
            else
            {
                return 0;
            }
        }

        public void UpdateUserInterests(int id, List<interesi> interests, bool saveChanges = true)
        {
            var user = Entities.SingleOrDefault(u => u.id == id);
            var interestNames = interests.Select(e => e.naziv_interesa).ToList();
            var selectedInterests = Context.interesi.Where(i => interestNames.Contains(i.naziv_interesa)).ToList();

            user.interesi.Clear();
            foreach (var interest in selectedInterests)
            {
                user.interesi.Add(interest);
            }

            if (saveChanges)
            {
                SaveChanges();
            }
        }
     

    }
}
