using BusinessLogicLayer.Services;
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
    /// Interaction logic for ucRegisterThird.xaml
    /// </summary>
    public partial class ucRegisterThird : UserControl
    {
        drustveneMrezeService services = new drustveneMrezeService();
        korisnikService korisnikService = new korisnikService();
        korisnikDrustveneMrezeService korisnikDrustveneMrezeService = new korisnikDrustveneMrezeService();
        private korisnik _userData;
        public ucRegisterThird(korisnik userData)
        {
            InitializeComponent();
            _userData = userData;
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (_userData.KorisnikDrustvenaMreza != null)
            {
                foreach (var socialMedia in _userData.KorisnikDrustvenaMreza)
                {
                    var socialNetwork = services.GetSocialNetworkById((int)socialMedia.id_drustvene_mreze).FirstOrDefault().naziv;
                    dgSocialMedia.Items.Add(new
                    {
                        SocialMediaName = socialNetwork,
                        SocialMediaLink = socialMedia.naziv_racuna_na_drustvenoj_mrezi,
                        Data = socialMedia
                    });
                }
            }
        }

        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {
            _userData.KorisnikDrustvenaMreza.Clear();
            foreach (var item in dgSocialMedia.Items)
            {
                var itemType = item.GetType();
                var dataProperty = itemType.GetProperty("Data");
                if (dataProperty != null)
                {
                    var socialMedia = dataProperty.GetValue(item) as KorisnikDrustvenaMreza;
                    if (socialMedia != null)
                    {
                        _userData.KorisnikDrustvenaMreza.Add(socialMedia);
                    }
                }
            }
            ucRegisterSecond ucRegisterSecond = new ucRegisterSecond(_userData);
            ContentControl contentControl = MainWindow.GetWindow(this).FindName("contentControl") as ContentControl;
            if (contentControl != null)
            {
                contentControl.Content = ucRegisterSecond;
            }
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            if (dgSocialMedia.Items.Count == 0)
            {
                MessageBox.Show("Morate unijeti barem jednu drustvenu mrezu!");
                return;
            } else
            {
                StringBuilder socialMediaAccounts = new StringBuilder();
                foreach (var item in dgSocialMedia.Items)
                {
                    var itemType = item.GetType();
                    var socialMediaNameProperty = itemType.GetProperty("SocialMediaName");
                    var socialMediaLinkProperty = itemType.GetProperty("SocialMediaLink");
                    if (socialMediaNameProperty != null && socialMediaLinkProperty != null)
                    {
                        string socialMediaName = socialMediaNameProperty.GetValue(item).ToString();
                        string socialMediaLink = socialMediaLinkProperty.GetValue(item).ToString();
                        socialMediaAccounts.AppendLine($"{socialMediaName}: {socialMediaLink}");
                    }
                }
                var messageBoxResult = MessageBox.Show("Ime: " + _userData.ime + "\n" + "Prezime: " + _userData.prezime + "\n" +
                  "Email: " + _userData.email + "\n" + "Datum rođenja: " + _userData.datum_rodenja + "\n" +
                  "Spol: " + _userData.spol + "\n" + "Lozinka: " + _userData.lozinka + "\n" +
                  "Interesi: " + string.Join(", ", _userData.interesi.Select(x => x.naziv_interesa)) + "\n" +
                  "Drustvene mreze: \n" + socialMediaAccounts.ToString() + "\n" +
                  "Jeste li sigurni da želite završiti registraciju?", "Potvrda registracije", MessageBoxButton.YesNo);
                if(messageBoxResult == MessageBoxResult.Yes)
                {
                    addNewUserToDatabase(_userData);
                    MessageBox.Show("Registracija uspješna!");
                    ucLogin ucLogin = new ucLogin();
                    ContentControl contentControl = MainWindow.GetWindow(this).FindName("contentControl") as ContentControl;
                    if (contentControl != null)
                    {
                        contentControl.Content = ucLogin;
                    }
                }
            }
        }

        private void addNewUserToDatabase(korisnik userData)
        {
            userData.banan = false;
            userData.uloga_id = 3;
            korisnikService.AddUser(userData);
            var user = korisnikService.GetUserByEmail(userData.email).FirstOrDefault();
            var userId = user.id;
            foreach (var item in dgSocialMedia.Items)
            {
                var itemType = item.GetType();
                var dataProperty = itemType.GetProperty("Data");
                if (dataProperty != null)
                {
                    var socialMedia = dataProperty.GetValue(item) as KorisnikDrustvenaMreza;
                    if (socialMedia != null)
                    {
                        socialMedia.id_korisnika = userId;
                        korisnikDrustveneMrezeService.AddSocialNetwork(socialMedia.id_korisnika, socialMedia.id_drustvene_mreze, socialMedia.naziv_racuna_na_drustvenoj_mrezi);
                    }
                }
            }
        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            var messageBoxResult = MessageBox.Show("Jeste li sigurni da želite prekinuti registraciju?", "Potvrda prekida registracije", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                ucLogin ucLogin = new ucLogin();
                ContentControl contentControl = MainWindow.GetWindow(this).FindName("contentControl") as ContentControl;
                if (contentControl != null)
                {
                    contentControl.Content = ucLogin;
                }
            }
        }

        private void cbSocialMedia_Loaded(object sender, RoutedEventArgs e)
        {
            var allSocialNetworks = services.GetAllSocialNetworks();
            if (allSocialNetworks != null && allSocialNetworks.Any())
            {
                cbSocialMedia.ItemsSource = allSocialNetworks;
                cbSocialMedia.DisplayMemberPath = "naziv";
            }
        }

        private void btnAddSocialMediaLink_Click(object sender, RoutedEventArgs e)
        {
            if (cbSocialMedia.SelectedItem is drustvene_mreze selectedSocialMedia)
            {
                var link = txtSocialMediaLink.Text;

                if (!string.IsNullOrWhiteSpace(link))
                {
                    var existingItem = dgSocialMedia.Items.Cast<dynamic>().FirstOrDefault(item => item.SocialMediaName == selectedSocialMedia.naziv);

                    if (existingItem != null)
                    {
                        var updatedSocialMedia = new KorisnikDrustvenaMreza
                        {
                            id_drustvene_mreze = selectedSocialMedia.id,
                            id_korisnika = _userData.id,
                            naziv_racuna_na_drustvenoj_mrezi = link,
                        };

                        dgSocialMedia.Items.Remove(existingItem);
                        dgSocialMedia.Items.Add(new
                        {
                            SocialMediaName = selectedSocialMedia.naziv,
                            SocialMediaLink = link,
                            Data = updatedSocialMedia
                        });
                        dgSocialMedia.Items.Refresh();
                        MessageBox.Show("Stari profil je ažuriran sa novim podacima");
                    }
                    else
                    {
                        var korisnikDrustvenaMreza = new KorisnikDrustvenaMreza
                        {
                            id_drustvene_mreze = selectedSocialMedia.id,
                            id_korisnika = _userData.id,
                            naziv_racuna_na_drustvenoj_mrezi = link,
                        };
                        dgSocialMedia.Items.Add(new
                        {
                            SocialMediaName = selectedSocialMedia.naziv,
                            SocialMediaLink = link,
                            Data = korisnikDrustvenaMreza
                        });
                    }
                }
                else
                {
                    MessageBox.Show("Tekstualno polje je prazno. Molimo unesite tekst.");
                }
            }
            else
            {
                MessageBox.Show("Niste odabrali drustvenu mrezu!");
            }
        }

        private void btnDeleteSocialMediaLink_Click(object sender, RoutedEventArgs e)
        {
            if (dgSocialMedia.SelectedItem != null)
            {
                dgSocialMedia.Items.Remove(dgSocialMedia.SelectedItem);
            }
        }
    }
}
