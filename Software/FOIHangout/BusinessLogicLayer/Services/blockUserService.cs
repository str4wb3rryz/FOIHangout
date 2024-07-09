using DataAccessLayer.Repositories;
using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class blockUserService
    {

        private report_korisnika_za_blokiranjeRepository userForBlockRepo = new report_korisnika_za_blokiranjeRepository();

        public List<report_korisnika_za_blokiranje> GetReportKorisnikZaBlokiranjeById(int id)
        {
            using (var repo = new report_korisnika_za_blokiranjeRepository())
            {
                List<report_korisnika_za_blokiranje> userBlock = repo.GetReportKorisnikZaBlokiranjeById(id).ToList();
                return userBlock;
            }
        }

        public List<report_korisnika_za_blokiranje> GetUsersConnectedToId(int id)
        {
            using (var repo = new report_korisnika_za_blokiranjeRepository())
            {
                List<report_korisnika_za_blokiranje> userBlock = repo.GetUsersConnectedToId(id).ToList();
                return userBlock;
            }
        }
        public bool AddNewBlockUser(report_korisnika_za_blokiranje UserBlock)
        {
            bool isSuccessful = false;
            using (var repo = new report_korisnika_za_blokiranjeRepository())
            {
                int affectedRows = repo.Add(UserBlock);
                isSuccessful = affectedRows > 0;
            }
            return isSuccessful;
        }
        public bool UpdateBlockUser(report_korisnika_za_blokiranje UserBlock)
        {
            bool isSuccessful = false;
            using (var repo = new report_korisnika_za_blokiranjeRepository())
            {
                int affectedRows = repo.Update(UserBlock);
                isSuccessful = affectedRows > 0;
            }
            return !isSuccessful;
        }
        public bool RemoveBlockUser(report_korisnika_za_blokiranje UserBlock)
        {
            bool isSuccessful = false;
            using (var repo = new report_korisnika_za_blokiranjeRepository())
            {
                int affectedRows = repo.Remove(UserBlock);
                isSuccessful = true;
            }
            
            return isSuccessful;
        }

        public bool IsUserBlocked(int id1, int id2)
        {
            using (var repo = new report_korisnika_za_blokiranjeRepository())
            {
                bool isBlocked = repo.IsUserBlocked(id1, id2);
                return isBlocked;
            }
        }

        public void UnblockUser(int id1, int id2)
        {
            using (var repo = new report_korisnika_za_blokiranjeRepository())
            {
                repo.UnblockUser(id1, id2);
            }
        }

        public static async Task<bool> IsBlockedAsync(int id, int value)
        {
            using (var repo = new report_korisnika_za_blokiranjeRepository())
            {
                bool isBlocked = await repo.IsUserBlockedAsync(id, value);
                return isBlocked;
            }
        }
    }
}
