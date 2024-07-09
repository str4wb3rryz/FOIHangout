using EntitiesLayer.Entities;
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
    /// Interaction logic for ucAdminOptions.xaml
    /// </summary>
    public partial class ucAdminOptions : UserControl
    {
        private korisnik _loggedUser;
        public ucAdminOptions(korisnik loggedUser)
        {
            InitializeComponent();
            _loggedUser = loggedUser;
        }

        private void btnDogadjaj_Click(object sender, RoutedEventArgs e)
        {
            ucAdminEvents adminEvent=new ucAdminEvents(_loggedUser);
            contentControl.Content = adminEvent;
        }

        private void btnPolls_Click(object sender, RoutedEventArgs e)
        {
            ucAdminPolls ucAdminPolls = new ucAdminPolls(_loggedUser);
            contentControl.Content = ucAdminPolls;
        }

        private void btnNotifications_Click(object sender, RoutedEventArgs e)
        {
            ucAdminNotifications ucAdminNotifications = new ucAdminNotifications(_loggedUser);
            contentControl.Content = ucAdminNotifications;
        }

        private void btnNotificationsForREPORTS_Click(object sender, RoutedEventArgs e)
        {
            ucAdminNotificationREPORT ucAdminNotificationREPORT = new ucAdminNotificationREPORT(_loggedUser);
            contentControl.Content = ucAdminNotificationREPORT;
        }
    }
}
