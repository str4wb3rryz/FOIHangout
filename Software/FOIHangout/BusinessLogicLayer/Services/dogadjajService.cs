using DataAccessLayer.Repositories;
using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class dogadjajService
    {
        private korisnikRepository korisnikRepo = new korisnikRepository();

        public async Task<List<dogadjaj>> GetDogadjaj()
        {
            using (var repo = new dogadjajRepository())
            {
                //List<dogadjaj> dogadjajs = (await Task.Run(() =>repo.GetAllAsync().Result.ToList())); //repo.GetAll()).ToList();
                List<dogadjaj> dogadjajs = (await repo.GetAllAsync()).ToList();
                
                return dogadjajs as List<dogadjaj>;
            }
        }
        public string GetUserEmailById(int userId)
        {
            // Assuming korisnikRepository has a method to get user email by ID
            var user = korisnikRepo.GetKorisnikById(userId).FirstOrDefault();
            return user?.email ?? string.Empty;
        }

        public List<dogadjaj> GetDogadjajById(int id)
        {
            using (var repo = new dogadjajRepository())
            {
                List<dogadjaj> dogadjajs = repo.GetDogadjajById(id).ToList();
                return dogadjajs;
            }
        }

        public dogadjaj GetSpecificDogadjajById(int id)
        {
            using (var repo = new dogadjajRepository())
            {
                dogadjaj dogadjajs = repo.GetSpecificDogadjajById(id);
                return dogadjajs;
            }
        }

        public bool AddNewDogadjaj(dogadjaj dogadjaj)
        {
            bool isSuccessful = false;
            using(var repo = new dogadjajRepository())
            {
                int affectedRows=repo.Add(dogadjaj);
                isSuccessful=affectedRows > 0;
            }
            return isSuccessful;
        }
        public bool UpdateDogadjaj(dogadjaj dogadjaj)
        {
            bool isSuccessful = false;
            using(var repo=new dogadjajRepository())
            {
                int affectedRows=repo.Update(dogadjaj);
                isSuccessful = affectedRows > 0;
            }
            return !isSuccessful;
        }
        public bool RemoveDogadjaj(dogadjaj dogadjaj)
        {
            bool isSuccessful = false;
            bool canRemove = CheckIfDogadjajCanBeRemoved(dogadjaj);
            if (canRemove)
            {
                using (var repo = new dogadjajRepository())
                {
                    int affectedRows = repo.Remove(dogadjaj);
                    isSuccessful = true;
                }
            }
            return isSuccessful;
        }

        private bool CheckIfDogadjajCanBeRemoved(dogadjaj dogadjaj)
        {
            if(dogadjaj.zavrsen == false)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
