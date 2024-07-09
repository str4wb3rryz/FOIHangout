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
using System.Windows.Shapes;

namespace FOIHangout_WPF.Windows
{
    /// <summary>
    /// Interaction logic for EditInterests.xaml
    /// </summary>
    public partial class EditInterests : Window
    {
        private interesiService interestsService = new interesiService();
        private  korisnikService korisnikService = new korisnikService();
        private korisnik _loggedUser;
        public delegate void InterestsUpdatedEventHandler(List<interesi> updatedInterests);
        public event InterestsUpdatedEventHandler InterestsUpdated;
        public EditInterests(korisnik loggedUser)
        {
            InitializeComponent();
            _loggedUser = loggedUser;

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var interests = korisnikService.GetUserInterests(_loggedUser.id);
            foreach (var interest in interests)
            {
                lbInterests.Items.Add(interest);
            }
        }

        private void cbInterests_Loaded(object sender, RoutedEventArgs e)
        {
            var allInterests = interestsService.GetAllInterests();
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
                lbInterests.Items.Add(selectedInteres);
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

        private void btnSaveChanges_Click(object sender, RoutedEventArgs e)
        {
            var oldInterests = korisnikService.GetUserInterests(_loggedUser.id);
            var oldInterestNames = oldInterests.Select(i => i.naziv_interesa).ToList();
            List<interesi> newInterests = new List<interesi>();
            List<interesi> removedInterests = new List<interesi>();
            foreach (interesi item in lbInterests.Items)
            {
                if (!oldInterestNames.Contains(item.naziv_interesa))
                {
                    newInterests.Add(item);
                }
            }
            foreach (var oldInterest in oldInterests)
            {
                if (!lbInterests.Items.Cast<interesi>().Any(item => item.naziv_interesa == oldInterest.naziv_interesa))
                {
                    removedInterests.Add(oldInterest);
                }
            }
            var combinedInterests = oldInterests.Except(removedInterests).Union(newInterests).ToList();
            if (newInterests.Any() || removedInterests.Any())
            {
                this.Close();
                InterestsUpdated?.Invoke(combinedInterests);
            }
            else
            {
                this.Close();
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
