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

namespace FOIHangout_WPF
{
    /// <summary>
    /// Interaction logic for MainWindowIfUserIsBannedFromApp.xaml
    /// </summary>
    public partial class MainWindowIfUserIsBannedFromApp : Window
    {
        private korisnik _loggedUser;
        public MainWindowIfUserIsBannedFromApp(korisnik loggedUser)
        {
            InitializeComponent();
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
    }
}
