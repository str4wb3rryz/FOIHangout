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
    /// Interaction logic for ucAdminNotificationREPORT.xaml
    /// </summary>
    public partial class ucAdminNotificationREPORT : UserControl
    {
        private notificationReportService reportService = new notificationReportService();
        private korisnik _loggedUser;
        public ucAdminNotificationREPORT(korisnik loggedUser)
        {
            InitializeComponent();
            _loggedUser = loggedUser;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshGUI();
        }

        private void btnEditNotificationREPORT_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDeleteNotificationREPORT_Click(object sender, RoutedEventArgs e)
        {

        }
        private string GetUserEmail(int? userId)
        {
            if (userId.HasValue)
            {
                return reportService.GetUserEmailById(userId.Value);
            }
            return string.Empty;
        }
        private async void RefreshGUI()
        {
            //var prikazDogadaja = await DohvatiDogadjaje();
            // dgShowEvents.ItemsSource = prikazDogadaja;

            var events = await reportService.GetNotificationREPORT();
            var displayREPORTS = events.Select(e => new NotificationREPORTDisplayModel
            {
                Id = e.id,
                Email_UserThatSendedReport = GetUserEmail(e.id_korisnika_koji_prijavljuje_korisnika),
                Email_UserThatWasReported = GetUserEmail(e.id_korisnika_koji_je_prijavljen),
                Description = e.report_opis,
                Title = e.report_naslov
            }).ToList();
            dgShowREPORTS.ItemsSource = displayREPORTS;
        }
    }
    
}
