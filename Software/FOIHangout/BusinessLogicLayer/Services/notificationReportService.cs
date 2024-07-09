using DataAccessLayer.Repositories;
using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class notificationReportService
    {
        private obavijestReportRepository obavijestReportRepository = new obavijestReportRepository();
        private korisnikRepository korisnikRepo = new korisnikRepository();

        public async Task<List<obavijestREPORT>> GetNotificationREPORT()
        {
            using (var repo = new obavijestReportRepository())
            {
                List<obavijestREPORT> notificationREPORT = (await repo.GetAllAsync()).ToList();

                return notificationREPORT as List<obavijestREPORT>;
            }
        }

        public List<obavijestREPORT> GetNotificationREPORTById(int id)
        {
            using (var repo = new obavijestReportRepository())
            {
                List<obavijestREPORT> notificationREPORT = repo.GetObavijestReportById(id).ToList();
                return notificationREPORT;
            }
        }

        public obavijestREPORT GetSpecificNotificationREPORTById(int id)
        {
            using (var repo = new obavijestReportRepository())
            {
                obavijestREPORT notificationREPORT = repo.GetSpecificNotificationREPORTById(id);
                return notificationREPORT;
            }
        }

        public bool AddNewNotificationREPORT(obavijestREPORT notificationREPORT_Add)
        {
            bool isSuccessful = false;
            using (var repo = new obavijestReportRepository())
            {
                int affectedRows = repo.Add(notificationREPORT_Add);
                isSuccessful = affectedRows > 0;
            }
            return isSuccessful;
        }
        public bool UpdateNotificationREPORT(obavijestREPORT notificationREPORT_Update)
        {
            bool isSuccessful = false;
            using (var repo = new obavijestReportRepository())
            {
                int affectedRows = repo.Update(notificationREPORT_Update);
                isSuccessful = affectedRows > 0;
            }
            return !isSuccessful;
        }
        public bool RemoveNotificationREPORT(obavijestREPORT notificationREPORT_Remove)
        {
            bool isSuccessful = false;
                using (var repo = new obavijestReportRepository())
                {
                    int affectedRows = repo.Remove(notificationREPORT_Remove);
                    isSuccessful = true;
                }
            
            return isSuccessful;
        }
        public string GetUserEmailById(int userId)
        {
            // Assuming korisnikRepository has a method to get user email by ID
            var user = korisnikRepo.GetKorisnikById(userId).FirstOrDefault();
            return user?.email ?? string.Empty;
        }
    }
}
