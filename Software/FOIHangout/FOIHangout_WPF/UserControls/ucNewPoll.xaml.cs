using BusinessLogicLayer.Services;
using EntitiesLayer.Entities;
using FOIHangout_WPF.HelperClasses;
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
    /// Interaction logic for ucNewPoll.xaml
    /// </summary>
    public partial class ucNewPoll : UserControl
    {
        private korisnik _loggedUser;
        private Action _hideContentControlAction;
        public event Action PoolAdded;
        anketaService anketaService = new anketaService();
        odabirService odabirService = new odabirService();
        private NotificationTemplate notificationTemplate = new NotificationTemplate();
        public ucNewPoll(korisnik loggedUser, Action hideContentControlAction)
        {
            InitializeComponent();
            _loggedUser = loggedUser;
            _hideContentControlAction = hideContentControlAction;
        }

        private void btnAddNewPoll_Click(object sender, RoutedEventArgs e)
        {
            if (!IsPollDataValid())
            {
                return;
            }
            var result = MessageBox.Show("Jeste li sigurni da želite dodati novu anketu?", "Dodavanje nove ankete", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                addNewPollToDatabase();
            }
        }

        private bool IsPollDataValid()
        {
            if (anketaService.GetAllAnketa().Any(p => p.naslov == txtPollTitle.Text))
            {
                MessageBox.Show("Anketa s tim naslovom već postoji!");
                return false;
            }

            if (cbMultipleChoices.IsChecked == true && !txtPollMultipleChoices.Text.Contains(";"))
            {
                MessageBox.Show("Molimo unesite više izbora odvojenih znakom ;");
                return false;
            }

            if (String.IsNullOrWhiteSpace(txtPollTitle.Text) || String.IsNullOrWhiteSpace(txtPollDesc.Text))
            {
                MessageBox.Show("Molimo popunite sva polja!");
                return false;
            }

            return true;
        }
        private void addNewPollToDatabase()
        {
            var poll = new anketa
            {
                id_korisnika = _loggedUser.id,
                naslov = txtPollTitle.Text,
                opis = txtPollDesc.Text,
                izbor_da_ne = cbMultipleChoices.IsChecked == false ? true : false,
                izbor_visestruki = cbMultipleChoices.IsChecked == true ? txtPollMultipleChoices.Text : null,
                zatvoren = false
            };
            addChoicesToDatabase();
            anketaService.AddAnketa(poll);
            notificationTemplate.CreateNewNotification(_loggedUser.id, "Kreiranje nove ankete od strane " + _loggedUser.ime + " " + _loggedUser.prezime, "Naslov ankete: " + txtPollTitle.Text);
            MessageBox.Show("Nova anketa uspješno dodana!");
            _hideContentControlAction?.Invoke();
            PoolAdded?.Invoke();
        }

        private void addChoicesToDatabase()
        {
            if (cbMultipleChoices.IsChecked == true)
            {
                var choices = txtPollMultipleChoices.Text.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var choice in choices)
                {
                    var trimmedChoice = choice.Trim();
                    var odabirSelekcije = new odabir_selekcije
                    {
                        odabir = trimmedChoice,
                    };

                    odabirService.AddOdabirSelekcije(odabirSelekcije);
                }
            }
        }

        private void btnAddPollExit_Click(object sender, RoutedEventArgs e)
        {
            _hideContentControlAction?.Invoke();
        }
    }
}
