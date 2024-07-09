using BusinessLogicLayer.Services;
using EntitiesLayer.Entities;
using FOIHangout_WPF.Windows;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FOIHangout_WPF.UserControls
{
    /// <summary>
    /// Interaction logic for ucProfile.xaml
    /// </summary>
    public partial class ucProfile : UserControl
    {
        drustveneMrezeService drustveneMrezeService = new drustveneMrezeService();
        korisnikService korisnikService = new korisnikService();
        korisnikDrustveneMrezeService korisnikDrustveneMrezeService = new korisnikDrustveneMrezeService();
        blockUserService blockUserService = new blockUserService();
        timService teamService = new timService();
        private korisnik _loggedUser;
        private List<interesi> _interests;
        private List<KorisnikDrustvenaMreza> _socialMediaAccounts;
        public delegate void UserNameChangedEventHandler(string newName);
        public event UserNameChangedEventHandler UserNameChanged;

        public ucProfile(korisnik loggedUser, List<interesi> interests, List<KorisnikDrustvenaMreza> socialMediaAccounts)
        {
            InitializeComponent();
            _loggedUser = loggedUser;
            _interests = interests;
            _socialMediaAccounts = socialMediaAccounts;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            _loggedUser = korisnikService.GetUserById(_loggedUser.id).FirstOrDefault();
            loadUserData();
            loadInterests();
            loadSocialMediaAccounts();
            loadBlockedUsers();
            
        }

        private void btnShowPassword_Click(object sender, RoutedEventArgs e)
        {
            if (txtPassword.Visibility == Visibility.Collapsed)
            {
                txtPassword.Visibility = Visibility.Visible;
                txtPassword.Text = pbPassword.Password;
                pbPassword.Visibility = Visibility.Hidden;
                imgPassword.Source = new BitmapImage(new Uri("pack://application:,,,/PicturesAndIcons/hide.png"));
            }
            else
            {
                pbPassword.Visibility = Visibility.Visible;
                pbPassword.Password = txtPassword.Text;
                txtPassword.Visibility = Visibility.Collapsed;
                imgPassword.Source = new BitmapImage(new Uri("pack://application:,,,/PicturesAndIcons/show.png"));
            }
        }

        private void btnEditInterests_Click(object sender, RoutedEventArgs e)
        {
            EditInterests editInterests = new EditInterests(_loggedUser);
            editInterests.InterestsUpdated += OnInterestsUpdated;
            editInterests.ShowDialog();
        }

        private void btnEditSocialMedia_Click(object sender, RoutedEventArgs e)
        {
            EditSocialMediaAccounts editSocialMediaAccounts = new EditSocialMediaAccounts(_loggedUser);
            editSocialMediaAccounts.SocialMediaUpdated += OnSocialMediaUpdated;
            editSocialMediaAccounts.ShowDialog();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            loadUserData();
            loadInterests();
            loadSocialMediaAccounts();
        }

        private void btnSaveChanges_Click(object sender, RoutedEventArgs e)
        {
            var messageBoxResult = MessageBox.Show("Jeste li sigurni da želite spremiti promjene?", "Potvrda promjena", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                _loggedUser = updateUserData(_loggedUser);
                UserNameChanged?.Invoke($"{_loggedUser.ime} {_loggedUser.prezime}");
                _interests = lbInterests.Items.Cast<string>().Select(i => new interesi { naziv_interesa = i }).ToList();
                korisnikService.UpdateUserInterests(_loggedUser.id, _interests);
                _socialMediaAccounts = dgSocialMedia.Items.Cast<dynamic>().Select(i => i.Data as KorisnikDrustvenaMreza).ToList();
                korisnikDrustveneMrezeService.UpdateSocialNetworkAccounts(_loggedUser.id, _socialMediaAccounts);
                loadUserData();
                loadInterests();
                loadSocialMediaAccounts();
                MessageBox.Show("Promjene spremljene");
            }
        }

        private korisnik updateUserData(korisnik loggedUser)
        {
            var updatedUser = new korisnik
            {
                id = loggedUser.id,
                ime = txtName.Text,
                prezime = txtSurname.Text,
                email = txtEmail.Text,
                lozinka = txtPassword.Visibility == Visibility.Visible ? txtPassword.Text : pbPassword.Password,
                spol = txtGender.Text,
                datum_rodenja = dpDateOfBirth.SelectedDate
            };
            korisnikService.UpdateUser(updatedUser);
            return korisnikService.GetUserById(loggedUser.id).FirstOrDefault();

        }

        private void OnInterestsUpdated(List<interesi> updatedInterests)
        {
            _interests = updatedInterests;
            lbInterests.Items.Clear();
            foreach (var interest in updatedInterests)
            {
                lbInterests.Items.Add(interest.naziv_interesa);
            }
        }

        private void OnSocialMediaUpdated(List<KorisnikDrustvenaMreza> updatedSocialMedia)
        {
            _socialMediaAccounts = updatedSocialMedia;
            dgSocialMedia.Items.Clear();
            foreach (var social in updatedSocialMedia)
            {
                var socialNetwork = drustveneMrezeService.GetSocialNetworkById((int)social.id_drustvene_mreze).FirstOrDefault();
                if (socialNetwork != null)
                {
                    dgSocialMedia.Items.Add(new
                    {
                        SocialMediaName = socialNetwork.naziv,
                        SocialMediaLink = social.naziv_racuna_na_drustvenoj_mrezi,
                        Data = social
                    });
                }
            }
        }

        private void loadSocialMediaAccounts()
        {
            dgSocialMedia.Items.Clear();
            var socialMedia = korisnikDrustveneMrezeService.GetSocialNetworksByUserId(_loggedUser.id);
            foreach (var social in socialMedia)
            {
                var socialNetwork = drustveneMrezeService.GetSocialNetworkById((int)social.id_drustvene_mreze).FirstOrDefault().naziv;
                dgSocialMedia.Items.Add(new
                {
                    SocialMediaName = socialNetwork,
                    SocialMediaLink = social.naziv_racuna_na_drustvenoj_mrezi,
                    Data = social
                });
            }
        }

        private void loadInterests()
        {
            lbInterests.Items.Clear();
            var interests = korisnikService.GetUserInterests(_loggedUser.id);
            foreach (var interest in interests)
            {
                lbInterests.Items.Add(interest.naziv_interesa);
            }
        }

        private void loadUserData()
        {
            txtName.Text = _loggedUser.ime;
            txtSurname.Text = _loggedUser.prezime;
            dpDateOfBirth.SelectedDate = _loggedUser.datum_rodenja;
            txtGender.Text = _loggedUser.spol;
            txtEmail.Text = _loggedUser.email;
            pbPassword.Password = _loggedUser.lozinka;
        }

        private void loadBlockedUsers()
        {
            lbBlockedUsers.Items.Clear();
            var blockedUsers = blockUserService.GetUsersConnectedToId(_loggedUser.id);
            foreach (var user in blockedUsers)
            {
                    lbBlockedUsers.Items.Add(
                    new korisnik
                    {
                        email = korisnikService.GetUserById(Convert.ToInt16(user.id_korisnika_koji_je_prijavljen)).FirstOrDefault().email,
                    }
                    ) ;
            }
        }

        private void lbBlockedUsers_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedUser = lbBlockedUsers.SelectedItem as korisnik;
            if(selectedUser != null)
            {
                var user = korisnikService.GetUserByEmail(selectedUser.email).FirstOrDefault();
                CheckOtherUserInfo checkOtherUserInfo = new CheckOtherUserInfo(user, _loggedUser);
                checkOtherUserInfo.ShowDialog();
                loadBlockedUsers();
            }
        }

   


        private void lvTeam_Loaded(object sender, RoutedEventArgs e)
        {
            lvTeam.Items.Clear();
            var teams = teamService.GetTeamsByUserId(_loggedUser.id);
            foreach (var team in teams)
            {
                lvTeam.Items.Add(team.naziv);
            }
        }




        private void btnAddTeam_Click(object sender, RoutedEventArgs e)
        {
            CreateNewTeam createNewTeam = new CreateNewTeam(_loggedUser);
            createNewTeam.ShowDialog();
        }
    }
}
