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
using System.Windows.Shapes;

namespace FOIHangout_WPF.Windows
{
    /// <summary>
    /// Interaction logic for EditSocialMediaAccounts.xaml
    /// </summary>
    public partial class EditSocialMediaAccounts : Window
    {
        private korisnik _loggedUser;
        drustveneMrezeService drustveneMrezeService = new drustveneMrezeService();
        korisnikDrustveneMrezeService korisnikDrustveneMrezeService = new korisnikDrustveneMrezeService();
        public delegate void SocialMediaUpdatedEventHandler(List<KorisnikDrustvenaMreza> updatedSocialMedia);
        public event SocialMediaUpdatedEventHandler SocialMediaUpdated;
        public EditSocialMediaAccounts(korisnik loggedUser)
        {
            InitializeComponent();
            _loggedUser = loggedUser;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
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

        private void cbSocialMedia_Loaded(object sender, RoutedEventArgs e)
        {
            var allSocialNetworks = drustveneMrezeService.GetAllSocialNetworks();
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
                            id_korisnika = _loggedUser.id,
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
                            id_korisnika = _loggedUser.id,
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

        private void btnSaveChanges_Click(object sender, RoutedEventArgs e)
        {
            List<KorisnikDrustvenaMreza> updatedSocialMedia = new List<KorisnikDrustvenaMreza>();

            foreach (var item in dgSocialMedia.Items)
            {
                var dataItem = item as dynamic;
                if (dataItem?.Data is KorisnikDrustvenaMreza socialMedia)
                {
                    updatedSocialMedia.Add(socialMedia);
                }
            }

            SocialMediaUpdated?.Invoke(updatedSocialMedia);

            this.Close();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
