using BusinessLogicLayer.Services;
using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOIHangout_WPF.HelperClasses
{
    public class NotificationTemplate
    {

        public void CreateNewNotification(int id_korisnika, string naziv, string opis)
        {
            var newNotification = new obavijest
            {
                id_korisnika = id_korisnika,
                naziv = naziv,
                opis = opis,
                datum_i_vrijeme_kreiranja = DateTime.Now
            };
            var createNewNotification = new notificationService();
            createNewNotification.AddNewGlobalNotification(newNotification);
            
        }
    }
}
