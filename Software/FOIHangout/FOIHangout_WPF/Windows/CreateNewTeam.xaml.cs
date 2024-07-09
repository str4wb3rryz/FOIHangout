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
    /// Interaction logic for CreateNewTeam.xaml
    /// </summary>
    public partial class CreateNewTeam : Window
    {
        private korisnik _loggedUser;
        private timService teamService = new timService();
        korisnikService korisnikService = new korisnikService();
        private kolegijiService kolegijiService = new kolegijiService();
        public CreateNewTeam(korisnik loggedUser)
        {
            InitializeComponent();
            _loggedUser = loggedUser;
        }

        private void cbKolegiji_Loaded(object sender, RoutedEventArgs e)
        {
            var kolegiji = kolegijiService.GetAllUserCourses();
            if (kolegiji != null && kolegiji.Any())
            {
                cbKolegiji.ItemsSource = kolegiji;
                cbKolegiji.DisplayMemberPath = "naziv";
            }
        }

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            string email = txtUser.Text.Trim();

            if (string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Molimo unesite email adresu.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var user = korisnikService.GetUserByEmail(email).FirstOrDefault();

            if (user != null)
            {
                lvTeammates.Items.Add(new
                {
                    V = lvTeammates.DisplayMemberPath = "naziv_teammates",
                    naziv_teammates = user.email
                });

                txtUser.Clear();
            }
            else
            {
                MessageBox.Show("Osoba s tim emailom nije registrirana na FOIHangout!", "User Not Found", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnRemoveUser_Click(object sender, RoutedEventArgs e)
        {
            if (lvTeammates.SelectedItem != null)
            {
                lvTeammates.Items.Remove(lvTeammates.SelectedItem);
            }
            else
            {
                MessageBox.Show("Niste odabrali korisnika za uklanjanje!", "User Not Selected", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void btnSaveChanges_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtTeamName.Text))
            {
                MessageBox.Show("Molimo unesite naziv tima.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (cbKolegiji.SelectedItem == null)
            {
                MessageBox.Show("Niste odabrali kolegij!", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (lvTeammates.Items.Count == 0)
            {
                MessageBox.Show("Molimo dodajte barem jednog člana tima.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (cbKolegiji.SelectedItem is kolegij selectedCourse)
            {
                tim team = new tim
                {
                    naziv = txtTeamName.Text,
                    kolegij_id = selectedCourse.id
                };
                int teamId = teamService.Add(team);

                if (teamId > 0)
                {
                    foreach (var item in lvTeammates.Items)
                    {
                        var teammateEmail = item.GetType().GetProperty("naziv_teammates").GetValue(item, null).ToString();
                        var teammate = korisnikService.GetUserByEmail(teammateEmail).FirstOrDefault();
                        if (teammate != null)
                        {
                            teamService.AddClanoveTima(new tim { id = teamId }, teammate);
                        }
                    }

                    MessageBox.Show("Tim uspješno kreiran!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Došlo je do greške prilikom kreiranja tima!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        


        private void btnOdustani_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
