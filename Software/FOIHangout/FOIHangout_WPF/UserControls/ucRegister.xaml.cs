using BusinessLogicLayer.Services;
using EntitiesLayer.Entities;
using FOIHangout_WPF.UserControls;
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

namespace FOIHangout_WPF
{
    /// <summary>
    /// Interaction logic for ucRegister.xaml
    /// </summary>
    public partial class ucRegister : UserControl
    {
        private korisnik _userData;
        public ucRegister(korisnik userData = null)
        {
            InitializeComponent();
            cmbGender.ItemsSource = new List<string> { "Muško", "Žensko", "Ne želim odabrati"};
            _userData = userData;
        }


        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (_userData != null)
            {
                txtName.Text = _userData.ime;
                txtSurname.Text = _userData.prezime;
                dpDateOfBirth.Text = _userData.datum_rodenja.ToString();
                cmbGender.Text = _userData.spol;
                txtEmail.Text = _userData.email;
                pbPassword.Password = _userData.lozinka;
            }
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

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if(checkIfAllFilled() && checkIfUserExists())
            {
                if(_userData != null)
                {
                    _userData.ime = txtName.Text;
                    _userData.prezime = txtSurname.Text;
                    _userData.datum_rodenja = DateTime.TryParse(dpDateOfBirth.Text, out DateTime date) ? date : (DateTime?)null;
                    _userData.spol = cmbGender.Text;
                    _userData.email = txtEmail.Text;
                    _userData.lozinka = txtPassword.Visibility == Visibility.Visible ? txtPassword.Text : pbPassword.Password;
                    ucRegisterSecond ucRegisterSecond = new ucRegisterSecond(_userData);
                    ContentControl contentControl = MainWindow.GetWindow(this).FindName("contentControl") as ContentControl;
                    if (contentControl != null)
                    {
                        contentControl.Content = ucRegisterSecond;
                    }
                }
                else
                {
                    korisnik korisnik = new korisnik
                    {
                        ime = txtName.Text,
                        prezime = txtSurname.Text,
                        datum_rodenja = DateTime.TryParse(dpDateOfBirth.Text, out DateTime date) ? date : (DateTime?)null,
                        spol = cmbGender.Text,
                        email = txtEmail.Text,
                        lozinka = txtPassword.Visibility == Visibility.Visible ? txtPassword.Text : pbPassword.Password
                    };
                    ucRegisterSecond ucRegisterSecond = new ucRegisterSecond(korisnik);
                    ContentControl contentControl = MainWindow.GetWindow(this).FindName("contentControl") as ContentControl;
                    if (contentControl != null)
                    {
                        contentControl.Content = ucRegisterSecond;
                    }
                }
            }
        }

        private bool checkIfUserExists()
        {
            korisnikService korisnikService = new korisnikService();
            var user = korisnikService.GetUserByEmail(txtEmail.Text);
            if (user.Count > 0)
            {
                MessageBox.Show("Korisnik s ovom email adresom već postoji!");
                return false;
            }
            else { return true; }
        }

        private bool checkIfAllFilled()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtSurname.Text) ||
                string.IsNullOrWhiteSpace(dpDateOfBirth.Text) ||
                string.IsNullOrWhiteSpace(cmbGender.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(pbPassword.Password) &&
                string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Niste ispunili sva polja!");
                return false;
            }
            else { return true; }
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
    }
}
