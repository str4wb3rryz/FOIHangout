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
    /// Interaction logic for ucReportUserToAdmin.xaml
    /// </summary>
    public partial class ucReportUserToAdmin : UserControl
    {
        private korisnik _selectedUser;
        private ContentControl _content;
        private GroupItem _groupAdminButtons;
        private GroupItem _groupButtons;
        private GroupItem _groupUserProfile;
        private korisnik _loggedUser;
        public ucReportUserToAdmin(korisnik selectedUser, ContentControl ccReportUserToAdminForm, GroupItem groupAdminButtons, GroupItem groupButtons, GroupItem groupUserProfile, korisnik loggedUser)
        {
            InitializeComponent();
            _selectedUser = selectedUser;
            _content = ccReportUserToAdminForm;
            _groupAdminButtons = groupAdminButtons;
            _groupButtons = groupButtons;
            _groupUserProfile = groupUserProfile;
            _loggedUser = loggedUser;
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            txtDescriptionOfAReport.Text = _selectedUser.ime +" "+ _selectedUser.prezime;
        }

        private void btnSendReport_Click(object sender, RoutedEventArgs e)
        {
            

            /*if (!notificationREPORTInputValidator.ValidateInput(
                    txtTitleOfAReport,
                    txtDescriptionOfAReport.Text)
                    )
            {
                return;
            }*/
            var newNotificationREPORT = new obavijestREPORT
            {
                report_naslov = txtTitleOfAReport.Text,
                report_opis = txtDescriptionOfAReport.Text,
                id_korisnika_koji_je_prijavljen = _selectedUser.id,
                id_korisnika_koji_prijavljuje_korisnika = _loggedUser.id
            };
            if (newNotificationREPORT != null)
            {
                var creatingNewNotificationREPORT = new notificationReportService();
                creatingNewNotificationREPORT.AddNewNotificationREPORT(newNotificationREPORT);
                MessageBox.Show("Uspješno ste poslali prijavu korisnika "+_selectedUser.ime+" "+_selectedUser.prezime);

                _groupAdminButtons.Visibility = Visibility.Visible;
                _groupButtons.Visibility = Visibility.Visible;
                _groupUserProfile.Visibility = Visibility.Visible;
                _content.Visibility = Visibility.Collapsed;
            }
            else
            {
                MessageBox.Show("Unesite validne podatke!");
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            _groupAdminButtons.Visibility = Visibility.Visible;
            _groupButtons.Visibility = Visibility.Visible;
            _groupUserProfile.Visibility = Visibility.Visible;
            _content.Visibility = Visibility.Collapsed;
        }
    }
}
