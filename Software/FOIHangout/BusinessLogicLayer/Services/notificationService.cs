using DataAccessLayer.Repositories;
using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class notificationService
    {
        private korisnikRepository userRepo = new korisnikRepository();

        public async Task<List<obavijest>> GetObavijesti()
        {
            using (var repo = new obavijestiRepository())
            {
                List<obavijest> notifications = (await repo.GetAllAsync()).ToList();
                return notifications as List<obavijest>;
            }
        }
        public string GetUserEmailById(int userId)
        {
            var user = userRepo.GetKorisnikById(userId).FirstOrDefault();
            return user?.email ?? string.Empty;
        }

        public bool AddNewGlobalNotification(obavijest notification)
        {
            bool isSuccessful = false;
            using (var repo = new obavijestiRepository())
            {
                int affectedRows = repo.Add(notification);
                isSuccessful = affectedRows > 0;
            }
            return isSuccessful;
        }
        public bool UpdateGlobalNotification(obavijest notification)
        {
            bool isSuccessful = false;
            using (var repo = new obavijestiRepository())
            {
                int affectedRows = repo.Update(notification);
                isSuccessful = affectedRows > 0;
            }
            return !isSuccessful;
        }
        public bool RemoveGlobalNotification(obavijest notification)
        {
            bool isSuccessful = false;
            using (var repo = new obavijestiRepository())
            {
                int affectedRows = repo.Remove(notification);
                isSuccessful = true;
            }
            return isSuccessful;
        }
        public obavijest GetSpecificNotificationById(int id)
        {
            using (var repo = new obavijestiRepository())
            {
                obavijest notification = repo.GetSpecificObavijestById(id);
                return notification;
            }
        }
    }
}
