using DataAccessLayer.Repositories;
using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class obavijestiService
    {
        public async Task<List<obavijest>> GetObavijesti()
        {
            using (var repo = new obavijestiRepository())
            {
                List<obavijest> dogadjajs = (await repo.GetAllAsync()).ToList();
                return dogadjajs;
            }
        }

        public List<obavijest> GeObavijestById(int id)
        {
            using (var repo = new obavijestiRepository())
            {
                List<obavijest> dogadjajs = repo.GetObavijestiById(id).ToList();
                return dogadjajs;
            }
        }
        public bool AddNewObavijest(obavijest obavijest)
        {
            bool isSuccessful = false;
            using (var repo = new obavijestiRepository())
            {
                int affectedRows = repo.Add(obavijest);
                isSuccessful = affectedRows > 0;
            }
            return isSuccessful;
        }
        public bool UpdateObavijest(obavijest obavijest)
        {
            bool isSuccessful = false;
            using (var repo = new obavijestiRepository())
            {
                int affectedRows = repo.Update(obavijest);
                isSuccessful = affectedRows > 0;
            }
            return !isSuccessful;
        }
        public bool RemoveDogadjaj(obavijest obavijest)
        {
            bool isSuccessful = false;
            bool canRemove = CheckIfObavijestCanBeRemoved(obavijest);
            if (canRemove)
            {
                using (var repo = new obavijestiRepository())
                {
                    int affectedRows = repo.Remove(obavijest);
                    isSuccessful = affectedRows > 0;
                }
            }
            return isSuccessful;
        }

        private bool CheckIfObavijestCanBeRemoved(obavijest obavijest)
        {
            /*if (obavijest == null)
            {
                return false;
            }
            else
            {*/
                return true;
            //}
        }
    }
}
