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
    /// Interaction logic for ucRegisterSecond.xaml
    /// </summary>
    public partial class ucRegisterSecond : UserControl
    {
        private interesiService services = new interesiService();
        private korisnik _userData;

        public ucRegisterSecond(korisnik userData)
        {
            InitializeComponent();
            _userData = userData;
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (_userData != null && _userData.interesi != null)
            {
                lbInterests.Items.Clear();
                foreach (var interest in _userData.interesi)
                {
                    lbInterests.Items.Add(interest);
                }
            }
        }

        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {
            _userData.interesi = lbInterests.Items.Cast<interesi>().ToList();
            ucRegister ucRegister = new ucRegister(_userData);
            ContentControl contentControl = MainWindow.GetWindow(this).FindName("contentControl") as ContentControl;
            if (contentControl != null)
            {
                contentControl.Content = ucRegister;
            }
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (lbInterests.Items.Count == 0 || lbInterests.Items.Count < 5)
            {
                MessageBox.Show($"Morate odabrati barem jos {5 - lbInterests.Items.Count} interesa!");
            } else
            {
                _userData.interesi = lbInterests.Items.Cast<interesi>().ToList();
                ucRegisterThird ucRegisterThird = new ucRegisterThird(_userData);
                ContentControl contentControl = MainWindow.GetWindow(this).FindName("contentControl") as ContentControl;
                if (contentControl != null)
                {
                    contentControl.Content = ucRegisterThird;
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
        private void cbInterests_Loaded(object sender, RoutedEventArgs e)
        {
            var allInterests = services.GetAllInterests();
            if (allInterests != null && allInterests.Any())
            {
                cbInterests.ItemsSource = allInterests;
                cbInterests.DisplayMemberPath = "naziv_interesa";
            }
        }

        private void btnAddInterests_Click(object sender, RoutedEventArgs e)
        {
            if (cbInterests.SelectedItem is interesi selectedInteres)
            {
                bool isDuplicate = lbInterests.Items.Cast<interesi>().Any(item => item.naziv_interesa == selectedInteres.naziv_interesa);

                if (!isDuplicate)
                {
                    lbInterests.Items.Add(selectedInteres);
                }
                else
                {
                    MessageBox.Show("Već ste dodali interest u listu!");
                }
            }
            else
            {
                MessageBox.Show("Niste odabrali interes!");
            }
        }

        private void btnDeleteInterest_Click(object sender, RoutedEventArgs e)
        {
            if (lbInterests.SelectedItem != null)
            {
                lbInterests.Items.Remove(lbInterests.SelectedItem);
            }
        }
    }
}
