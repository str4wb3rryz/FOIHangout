using BusinessLogicLayer.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Interaction logic for ucLogin.xaml
    /// </summary>
    public partial class ucLogin : UserControl
    {
        private korisnikService services = new korisnikService();
        public static bool isAdmin;

        public ucLogin()
        {
            InitializeComponent();
        }

        private void checkIfAdmin()
        {
            var uloga_korisnika = services.GetUserByEmail(txtEmail.Text).First().uloga_id;
            int uloga_id = Convert.ToInt32(uloga_korisnika);
            if (uloga_id == 1)
            {
                isAdmin = true;
            }
            else
            {
                isAdmin = false;
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if(txtEmail.Text == "" || pbPassword.Password == "" && txtPassword.Text == " ")
            {
                MessageBox.Show("Niste unijeli sve podatke!");
                return;
            } else
            {
                string password = txtPassword.Visibility == Visibility.Visible ? txtPassword.Text : pbPassword.Password;
                checkUsernameAndPassword(txtEmail.Text, password);
            }
            
        }
        private void checkUsernameAndPassword(string email, string password)
        {
            var user = services.GetUserByEmail(email).FirstOrDefault();
            if (user == null)
            {
                MessageBox.Show("Ne postoji korisnik s tim emailom!");
                return;
            } else
            {
                if (user.email == email)
                {
                    if (user.lozinka == password)
                    {
                        if (user.banan == false)
                        {
                            checkIfAdmin();
                            MainForm mainForm = new MainForm(user);
                            mainForm.Show();
                            Window.GetWindow(this).Close();
                        }
                        else if (user.banan == true)
                        {
                            MainWindowIfUserIsBannedFromApp bannedWindow = new MainWindowIfUserIsBannedFromApp(user);
                            bannedWindow.Show();
                            Window.GetWindow(this).Close();
                        }
                        
                    }
                    else
                    {
                        MessageBox.Show("Pogrešna lozinka!");
                        return;
                    }
                }
            }
           
        }

        private void btnShowPassword_Click(object sender, RoutedEventArgs e)
        {
            //klikom na gumbom se prikazuje lozinka
            if (txtPassword.Visibility == Visibility.Collapsed)
            {

                txtPassword.Visibility = Visibility.Visible;
                txtPassword.Text = pbPassword.Password;
                pbPassword.Visibility = Visibility.Hidden;
                imgPassword.Source = new BitmapImage(new Uri("pack://application:,,,/PicturesAndIcons/hide.png"));
            }
            //klikom na gumbom se sakriva lozinka
            else
            {
                pbPassword.Visibility = Visibility.Visible;
                pbPassword.Password = txtPassword.Text;
                txtPassword.Visibility = Visibility.Collapsed;
                imgPassword.Source = new BitmapImage(new Uri("pack://application:,,,/PicturesAndIcons/show.png"));
            }
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            ucRegister ucRegister = new ucRegister();
            ContentControl contentControl = MainWindow.GetWindow(this).FindName("contentControl") as ContentControl;
            if (contentControl != null)
            {
                contentControl.Content = ucRegister;
            }
            else
            {
                MessageBox.Show("Greška!");
            }
        }
    }
}
