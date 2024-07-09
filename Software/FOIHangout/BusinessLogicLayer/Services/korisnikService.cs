using DataAccessLayer.Repositories;
using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class korisnikService
    {
        //get all users
        public List<korisnik> GetAllUsers()
        {
            using (var db = new korisnikRepository())
            {
                var result = db.GetAll();
                return result.ToList();
            }
        }
        //get user by email
        public List<korisnik> GetUserByEmail(string email)
        {
            using (var db = new korisnikRepository())
            {
                var result = db.GetKorisnikByEmail(email);
                return result.ToList();
            }
        }
        //get user by id
        public List<korisnik> GetUserById(int id)
        {
            using (var db = new korisnikRepository())
            {
                var result = db.GetKorisnikById(id);
                return result.ToList();
            }
        }
        //update user info
        public void UpdateUser(korisnik korisnik)
        {
            //dodati provjeru za podatke
            using (var db = new korisnikRepository())
            {
                db.UpdateProfile(korisnik);
            }
        }
        //delete user
        public void DeleteUser(korisnik korisnik)
        {
            using (var db = new korisnikRepository())
            {
                db.Remove(korisnik);
            }
        }
        //add user
        public void AddUser(korisnik korisnik)
        {
            using (var db = new korisnikRepository())
            {
                db.Add(korisnik);
            }
        }
        //get user interests
        public List<interesi> GetUserInterests(int id)
        {
            using (var db = new korisnikRepository())
            {
                return db.GetInterestsByUserId(id).ToList();
            }
        }

        public void UpdateUserInterests(int id, List<interesi> interests)
        {
            using (var db = new korisnikRepository())
            {
                db.UpdateUserInterests(id, interests);
            }
        }
        public async Task<List<korisnik>> GetUsersAsync()
        {
            using (var db = new korisnikRepository())
            {
                var result = await db.GetAllAsync();
                return result.ToList();
            }
        }

        public async Task<List<korisnik>> GetUserByIdAsync(int id)
        {
            using (var db = new korisnikRepository())
            {
                return await Task.Run(() => db.GetKorisnikById(id).ToList());
            }
        }

        public void UpdateBanned(korisnik user)
        {
            //dodati provjeru za podatke
            using (var db = new korisnikRepository())
            {
                db.UpdateBanned(user);
            }
        }

     
    }
}
