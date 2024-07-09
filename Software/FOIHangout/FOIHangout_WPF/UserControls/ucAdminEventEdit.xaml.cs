using BusinessLogicLayer;
using EntitiesLayer.Entities;
using FOIHangout_WPF.HelperClasses;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
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
    /// Interaction logic for ucAdminEventEdit.xaml
    /// </summary>
    public partial class ucAdminEventEdit : UserControl
    {
        private DataGrid dgShowEvents;
        private dogadjajService dogadjajService;
        private dogadjaj selectedEvent;
        private Action _hideContentControlAction;
        public event Action EventUpdated;
        private korisnik _loggedUser;
        private NotificationTemplate notificationTemplate = new NotificationTemplate();
        public ucAdminEventEdit(DataGrid dgShowEvents, dogadjajService dogadjajService, dogadjaj selectedEvent, Action hideContentControl, korisnik loggedUser)
        {
            InitializeComponent();
            this.dgShowEvents = dgShowEvents;
            this.dogadjajService = dogadjajService;
            this.selectedEvent = selectedEvent;
            _hideContentControlAction = hideContentControl;
            _loggedUser = loggedUser;
        }
        private dogadjaj GetDogadjajFromEvent(int id)
        {
            return dogadjajService.GetSpecificDogadjajById(id);
        }
        
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (selectedEvent != null)
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

                selectedEvent.naziv = txtNazivNovogEventa.Text;
                selectedEvent.opis = txtOpisNovogEventa.Text;
                selectedEvent.poseban_dogadjaj = txtPosebanDogadaj.Text;
                selectedEvent.datum_pocetka = datePocetakNovogEventa.SelectedDate;
                selectedEvent.datum_zavrsetka = dateZavrsetakNovogEventa.SelectedDate;
                selectedEvent.zavrsen = bool.Parse(((ComboBoxItem)pickZavrseno.SelectedItem).Tag.ToString());
                selectedEvent.id_korisnika = _loggedUser.id;

                if (MessageBox.Show("Jeste li sigurno da želite ažurirati odabrani događaj", "Ažuriranje događaja", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    var EditEvent = new dogadjajService();
                    EditEvent.UpdateDogadjaj(selectedEvent);
                    MessageBox.Show("Uspješno ste ažurirali događaj!");
                    notificationTemplate.CreateNewNotification(_loggedUser.id, "Uređivanje postojećeg događaja","Naziv događaja: "+txtNazivNovogEventa.Text+".\nOpis događaja: "+txtOpisNovogEventa.Text);
                    RefreshGUI();
                    _hideContentControlAction?.Invoke();
                    EventUpdated?.Invoke();
                }
            }

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            _hideContentControlAction?.Invoke();
        }
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
        private string GetUserEmail(int? userId)
        {
            if (userId.HasValue)
            {
                return dogadjajService.GetUserEmailById(userId.Value);
            }
            return string.Empty;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if(selectedEvent != null) { 
                txtNazivNovogEventa.Text = selectedEvent.naziv;
                txtOpisNovogEventa.Text=selectedEvent.opis;
                txtPosebanDogadaj.Text=selectedEvent?.poseban_dogadjaj;
                datePocetakNovogEventa.SelectedDate = selectedEvent.datum_pocetka;
                dateZavrsetakNovogEventa.SelectedDate = selectedEvent.datum_zavrsetka;

                bool zavrsen = Convert.ToBoolean(selectedEvent.zavrsen);
                SelectedZavrseno(zavrsen);
            }
        }
        private void SelectedZavrseno(bool zavrsen)
        {
            List<bool> zavrsenList = new List<bool> { false, true };
            for(int i=0;i<zavrsenList.Count;i++)
            {
                if (zavrsenList[i] == zavrsen)
                {
                    pickZavrseno.SelectedIndex = i;
                    break;
                }
            }
        }

       
    }
}
