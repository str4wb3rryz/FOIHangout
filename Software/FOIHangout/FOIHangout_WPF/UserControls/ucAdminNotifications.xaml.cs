using BusinessLogicLayer;
using BusinessLogicLayer.Services;
using EntitiesLayer.Entities;
using FOIHangout_WPF.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FOIHangout_WPF.UserControls
{
    /// <summary>
    /// Interaction logic for ucAdminNotifications.xaml
    /// </summary>
    public partial class ucAdminNotifications : UserControl
    {
        private notificationService notificationService = new notificationService();
        private korisnik _loggedUser;
        public ucAdminNotifications(korisnik loggedUser)
        {
            InitializeComponent();
            _loggedUser = loggedUser;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshGUI();
        }

        private void btnDeleteNotification_Click(object sender, RoutedEventArgs e)
        {
            var notification = GetSelectedNotification();
            if (notification != null)
            {
                if (MessageBox.Show("Jeste li sigurno da želite obrisati odabranu notifikacijuj", "Brisanje notifikacije", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    notificationService.RemoveGlobalNotification(notification);
                    MessageBox.Show("Notifikacija je uspješno obrisan!");
                    RefreshGUI();
                }
            }
            else
            {
                MessageBox.Show("Niste odabrali notifikaciju za brisanje.\nOdaberite notifikaciju koju želite obrisati.");
            }
        }

        private obavijest GetSelectedNotification()
        {
            return dgShowNotifications.SelectedItem as obavijest;
        }

        private obavijest GetNotification(int id)
        {
            return notificationService.GetSpecificNotificationById(id);
        }
        private async void RefreshGUI()
        {
            var notification = await notificationService.GetObavijesti();
            dgShowNotifications.ItemsSource = notification;
        }
    }
}
