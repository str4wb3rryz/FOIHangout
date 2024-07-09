using BusinessLogicLayer;
using BusinessLogicLayer.Services;
using EntitiesLayer.Entities;
using FOIHangout_WPF.HelperClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for ucAdminEventAddNew.xaml
    /// </summary>
    public partial class ucAdminEventAddNew : UserControl
    {
        private DataGrid dgShowEvents;
        private dogadjajService dogadjajService;
        private Action _hideContentControlAction;
        public event Action EventAdded;
        private korisnik _loggedUser;
        private obavijestiService _notificationService;
        private NotificationTemplate notificationTemplate = new NotificationTemplate();


        public ucAdminEventAddNew(DataGrid dgShowEvents, dogadjajService dogadjajService, Action hideContentControl, korisnik loggedUser)
        {
            InitializeComponent();
            this.dgShowEvents = dgShowEvents;
            this.dogadjajService = dogadjajService;
            _hideContentControlAction = hideContentControl;
            _loggedUser = loggedUser;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            _hideContentControlAction?.Invoke();
        }        

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (!EventInputValidator.ValidateInput(
                    txtNazivNovogEventa.Text,
                    txtOpisNovogEventa.Text,
                    txtPosebanDogadaj.Text,
                    datePocetakNovogEventa.SelectedDate,
                    dateZavrsetakNovogEventa.SelectedDate)
                    )
            {
                return;
            }
            var noviDogadjaj = new dogadjaj
            {
                naziv = txtNazivNovogEventa.Text,
                opis = txtOpisNovogEventa.Text,
                datum_pocetka = datePocetakNovogEventa.SelectedDate,
                datum_zavrsetka = dateZavrsetakNovogEventa.SelectedDate,
                poseban_dogadjaj = txtPosebanDogadaj.Text,
                zavrsen = bool.Parse(((ComboBoxItem)pickZavrseno.SelectedItem).Tag.ToString()),
                //id_korisnika=1,//Ovo treba promijeniti!!!!
                id_korisnika = _loggedUser.id,
            };
            if(noviDogadjaj != null)
            {
                var kreiranjeNovogDogadjaja = new dogadjajService();
                kreiranjeNovogDogadjaja.AddNewDogadjaj(noviDogadjaj);
                MessageBox.Show("Uspješno ste dodali novi događaj!");
                RefreshGUI();
                _hideContentControlAction?.Invoke();
                EventAdded?.Invoke();

                /*var newNotification = new obavijest
                {
                    id_korisnika = _loggedUser.id,
                    naziv = "Kreiranje novog događaja",
                    opis = txtOpisNovogEventa.Text,
                    datum_i_vrijeme_kreiranja = DateTime.Now
                };
                var createNewNotification = new notificationService();
                createNewNotification.AddNewGlobalNotification(newNotification);*/
                //how do i call CreateNewNotification in here?

                notificationTemplate.CreateNewNotification(_loggedUser.id, "Kreiranje novog događaja", "Naziv događaja: " + txtNazivNovogEventa.Text + ".\nOpis događaja: " + txtOpisNovogEventa.Text);


            }
            else
            {
                MessageBox.Show("Unesite validne podatke!");
            }
        }
        /*private async void RefreshGUI()
        {
            var prikazDogadaja = await DohvatiDogadjaje();
            dgShowEvents.ItemsSource = prikazDogadaja;
        }*/
        
        private async void RefreshGUI()
        {
            var events = await dogadjajService.GetDogadjaj();
            var displayEvents = events.Select(e => new EventDisplayModel
            {
                EventId = e.id,
                EventName = e.naziv,
                EventDescription = e.opis,
                StartDate = e.datum_pocetka,
                EndDate = e.datum_zavrsetka,
                SpecialEvent = e.poseban_dogadjaj,
                IsFinished = e.zavrsen,
                UserEmail = GetUserEmail(e.id_korisnika)
            }).ToList();
            dgShowEvents.ItemsSource = displayEvents;
        }
        private async Task<List<dogadjaj>> DohvatiDogadjaje()
        {
            return await dogadjajService.GetDogadjaj();
        }
        private string GetUserEmail(int? userId)
        {
            if (userId.HasValue)
            {
                return dogadjajService.GetUserEmailById(userId.Value);
            }
            return string.Empty;
        }

    }
}
