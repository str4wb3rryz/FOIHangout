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
    /// Interaction logic for ucNewPost.xaml
    /// </summary>
    public partial class ucNewPost : UserControl
    {
        private korisnik _loggedUser;
        public ucNewPost(korisnik loggedUser)
        {
            InitializeComponent();
            _loggedUser = loggedUser;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            txtDate.Text = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
        }
    }
}
