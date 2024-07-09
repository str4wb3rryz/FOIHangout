using BusinessLogicLayer;
using BusinessLogicLayer.Services;
using EntitiesLayer.Entities;
using FOIHangout_WPF.UserControls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FOIHangout_WPF.Windows
{
    /// <summary>
    /// Interaction logic for CheckOtherUserInfo.xaml
    /// </summary>
    public partial class CheckOtherUserInfo : Window
    {
        private korisnikService korisnikService = new korisnikService();
        private blockUserService blockUser = new blockUserService();
        private korisnik _poster;
        private korisnik _loggedUser;
        public CheckOtherUserInfo(korisnik poster, korisnik loggedUser)
        {
            InitializeComponent();
            _poster = poster;
            _loggedUser = loggedUser;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            checkAdminRole();
            tbName.Text = _poster.ime;
            tbSurname.Text = _poster.prezime;

            groupAdminButtons.Visibility = Visibility.Visible;
            groupButtons.Visibility = Visibility.Visible;
            groupUserProfile.Visibility = Visibility.Visible;
            ccReportUserToAdminFormDisplay.Visibility = Visibility.Collapsed;

            bool isBlocked = blockUser.IsUserBlocked(_loggedUser.id, _poster.id);
            if (isBlocked)
            {
                btnBlockUser.Content = "Unblock korisnika";
            }
            else
            {
                btnBlockUser.Content = "Block korisnika";
            }
        }

        private void btnReportUser_Click(object sender, RoutedEventArgs e)
        {
            //MessageBoxResult result = MessageBox.Show("Jeste li sigurni da želite prijaviti korisnika " + _poster.ime + " " + _poster.prezime + "?", "Potvrda", MessageBoxButton.YesNo, MessageBoxImage.Question);
            //if(result == MessageBoxResult.Yes)
            //{
            ucReportUserToAdmin ucReportUserToAdmin = new ucReportUserToAdmin(_poster, ccReportUserToAdminFormDisplay, groupAdminButtons, groupButtons, groupUserProfile, _loggedUser);
            ccReportUserToAdminFormDisplay.Content = ucReportUserToAdmin;

            groupAdminButtons.Visibility = Visibility.Collapsed;
            groupButtons.Visibility = Visibility.Collapsed;
            ccReportUserToAdminFormDisplay.Visibility = Visibility.Visible;

            //MessageBox.Show("Korisnik je uspješno prijavljen", "Obavijest", MessageBoxButton.OK, MessageBoxImage.Information);
            //this.Close();
            //}
        }
        private bool checkAdminRole()
        {
            if (ucLogin.isAdmin == true)
            {
                btnDeniedAccessToApplication.Visibility = Visibility.Visible;
                return true;
            }
            else
            {
                btnDeniedAccessToApplication.Visibility = Visibility.Collapsed;
                return false;
            }
        }

        private void btnBlockUser_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result;
            bool isBlocked = blockUser.IsUserBlocked(_loggedUser.id, _poster.id);
            if (isBlocked)
            {
                result = MessageBox.Show("Jeste li sigurni da želite odblokirati korisnika " + _poster.ime + " " + _poster.prezime + "?", "Potvrda", MessageBoxButton.YesNo, MessageBoxImage.Question);
            }
            else
            {
                result = MessageBox.Show("Jeste li sigurni da želite blokirati korisnika " + _poster.ime + " " + _poster.prezime + "?", "Potvrda", MessageBoxButton.YesNo, MessageBoxImage.Question);
            }
            if (result == MessageBoxResult.Yes)
            {
                if (isBlocked)
                {
                    blockUser.UnblockUser(_loggedUser.id, _poster.id);
                    MessageBox.Show("Korisnik je uspješno odblokiran", "Obavijest", MessageBoxButton.OK, MessageBoxImage.Information);
                    btnBlockUser.Content = "Block korisnika";
                }
                else
                {
                    var blockUser = new report_korisnika_za_blokiranje()
                    {
                        id_korisnika_koji_prijavljuje_korisnika = _loggedUser.id,
                        id_korisnika_koji_je_prijavljen = _poster.id
                    };
                    if (blockUser != null)
                    {
                        var newBlockedUser = new blockUserService();
                        newBlockedUser.AddNewBlockUser(blockUser);
                        MessageBox.Show("Korisnik je uspješno blokiran", "Obavijest", MessageBoxButton.OK, MessageBoxImage.Information);
                        btnBlockUser.Content = "Unblock korisnika";
                    }
                }
            }
        }

        private void btnDeniedAccessToApplication_Click(object sender, RoutedEventArgs e)
        {
            if (checkAdminRole() == true)
            {

                if (_poster.banan == false)
                {
                    MessageBoxResult result = MessageBox.Show("Jeste li sigurni da želite zabraniti pristup aplikaciji korisniku " + _poster.ime + " " + _poster.prezime + "?", "Potvrda", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        _poster.banan = true;
                        korisnikService.UpdateBanned(_poster);
                        MessageBox.Show("Korisniku je uspješno zabranjen pristup aplikaciji.", "Obavijest", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.Close();
                    }
                }
                else
                {
                    MessageBoxResult result = MessageBox.Show("Jeste li sigurni da želite maknuti zabranu pristupa aplikaciji korisniku " + _poster.ime + " " + _poster.prezime + "?", "Potvrda", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        _poster.banan = false;
                        korisnikService.UpdateBanned(_poster);
                        MessageBox.Show("Korisniku je uspješno makknuta zabrana pristupa aplikaciji.", "Obavijest", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.Close();
                    }
                }
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F1)
            {
                string currentDirectory = Directory.GetCurrentDirectory();
                string filePath = System.IO.Path.Combine(currentDirectory, "UserGuide.chm");
                System.Windows.Forms.Help.ShowHelp(null, filePath, System.Windows.Forms.HelpNavigator.Topic, "blockreport.htm");
            }
        }
    }
}
