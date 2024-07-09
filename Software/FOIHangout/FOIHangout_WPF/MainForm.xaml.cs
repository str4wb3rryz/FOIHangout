using BusinessLogicLayer.Services;
using EntitiesLayer.Entities;
using FOIHangout_WPF.UserControls;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;

namespace FOIHangout_WPF
{
    /// <summary>
    /// Interaction logic for MainForm.xaml
    /// </summary>
    public partial class MainForm : Window
    {
        private korisnikService korisnikService = new korisnikService();
        private korisnikDrustveneMrezeService korisnikDrustveneMrezeService = new korisnikDrustveneMrezeService();
        private korisnik _loggedUser;

        public MainForm(korisnik loggedUser)
        {
            InitializeComponent();
            lblLoggedUser.DataContext = new { LoggedUser = $"{loggedUser.ime} {loggedUser.prezime}" };
            _loggedUser = loggedUser;
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Jeste li sigurni da se želite odjaviti?", "Odjava", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                MainWindow loginForm = new MainWindow();
                loginForm.Show();
                this.Close();
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ucHome ucHome = new ucHome(_loggedUser);
            contentControl.Content = ucHome;
            checkAdminRole();
        }

        private void checkAdminRole()
        {
            if (ucLogin.isAdmin == true)
            {
                btnAdminOptions.Visibility = Visibility.Visible;
            }
            else
            {
                btnAdminOptions.Visibility = Visibility.Collapsed;
            }
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            ucHome ucHome = new ucHome(_loggedUser);
            contentControl.Content = ucHome;
        }

        private void btnFriends_Click(object sender, RoutedEventArgs e)
        {
            ucFriends ucFriends = new ucFriends(_loggedUser);
            contentControl.Content = ucFriends;
        }

        private void btnAdminOptions_Click(object sender, RoutedEventArgs e)
        {
            ucAdminOptions ucAdminOptions = new ucAdminOptions(_loggedUser);
            contentControl.Content = ucAdminOptions;
        }

        private void imgProfile_MouseDown(object sender, MouseButtonEventArgs e)
        {
            List<interesi> userInterests = korisnikService.GetUserInterests(_loggedUser.id);
            List<KorisnikDrustvenaMreza> userSocialMedia = korisnikDrustveneMrezeService.GetSocialNetworksByUserId(_loggedUser.id);
            ucProfile ucProfile = new ucProfile(_loggedUser, userInterests, userSocialMedia);
            ucProfile.UserNameChanged += UcProfile_UserNameChanged;
            contentControl.Content = ucProfile;
        }

        private void UcProfile_UserNameChanged(string newName)
        {
            lblLoggedUser.DataContext = new { LoggedUser = newName };
        }

        private void btnEvents_Click(object sender, RoutedEventArgs e)
        {
            ucEvents ucEvents = new ucEvents(contentControl);
            contentControl.Content=ucEvents;
        }

        private void btnPolls_Click(object sender, RoutedEventArgs e)
        {
            ucPolls ucPolls = new ucPolls(_loggedUser);
            contentControl.Content = ucPolls;
        }

        private void btnNotifications_Click(object sender, RoutedEventArgs e)
        {
            ucGlobalNotifications ucGlobalNotifications = new ucGlobalNotifications(contentControl);
            contentControl.Content = ucGlobalNotifications;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F1)
            {
                if (contentControl.Content is UserControl userControl)
                {
                    string userControlName = userControl.GetType().Name;

                    string topic = string.Empty;
                    switch (userControlName)
                    {
                        case "ucEvents":
                            topic = "events.htm";
                            break;
                        case "ucFriends":
                            topic = "friends.htm";
                            break;
                        case "ucGlobalNotifications":
                            topic = "notifications.htm";
                            break;
                        case "ucHome":
                            topic = "forum.htm";
                            break;
                        case "ucPolls":
                            topic = "polls.htm";
                            break;
                        case "ucProfile":
                            topic = "profile.htm";
                            break;
                        case "ucAdminOptions":
                            topic = "adminoptions.htm";
                            break;
                        default:
                            topic = "main.htm";
                            break;
                    }

                    string currentDirectory = Directory.GetCurrentDirectory();
                    string filePath = System.IO.Path.Combine(currentDirectory, "UserGuide.chm");
                    System.Windows.Forms.Help.ShowHelp(null, filePath, System.Windows.Forms.HelpNavigator.Topic, topic);
                }
            }
        }
    }
}
