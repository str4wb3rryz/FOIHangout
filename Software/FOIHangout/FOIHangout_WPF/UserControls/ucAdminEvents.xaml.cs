using BusinessLogicLayer;
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
    /// Interaction logic for ucAdminEvents.xaml
    /// </summary>
    /// 
    
    public partial class ucAdminEvents : UserControl
    {
        private dogadjajService dogadjajService = new dogadjajService();
        private korisnik _loggedUser;
        private NotificationTemplate notificationTemplate = new NotificationTemplate();

        public ucAdminEvents(korisnik loggedUser)
        {
            InitializeComponent();
            _loggedUser = loggedUser;
        }

        private void btnCreateNewEvent_Click(object sender, RoutedEventArgs e)
        {
            ccShowAddUpdate.Visibility = Visibility.Visible;
            ucAdminEventAddNew ucAdminEventAddNew = new ucAdminEventAddNew(dgShowEvents, dogadjajService, HideContentControl, _loggedUser);
            ucAdminEventAddNew.EventAdded += RefreshGUI;
            ccShowAddUpdate.Content = ucAdminEventAddNew;
            svDataGrid.Height = 180;
        }

        private void btnEditEvent_Click(object sender, RoutedEventArgs e)
        {
            if (dgShowEvents.SelectedItem is EventDisplayModel selectedEvent)
            {
                var dogadjaj = GetDogadjajFromEvent(selectedEvent.EventId);
                ccShowAddUpdate.Visibility = Visibility.Visible;
                ucAdminEventEdit ucAdminEventEditEvent = new ucAdminEventEdit(dgShowEvents, dogadjajService, dogadjaj, HideContentControl, _loggedUser);
                ucAdminEventEditEvent.EventUpdated += RefreshGUI;
                ccShowAddUpdate.Content = ucAdminEventEditEvent;
                svDataGrid.Height = 180;
            }
            else
            {
                MessageBox.Show("Niste odabrali događaj za anžuriranje.\nOdaberite događaj kojeg želite anžurirati.");
            }
            RefreshGUI();
        }

        private void btnDeleteEvent_Click(object sender, RoutedEventArgs e)
        {
            if (dgShowEvents.SelectedItem is EventDisplayModel selectedEvent)
            {
                var dogadjaj =  GetDogadjajFromEvent(selectedEvent.EventId);
                if (dogadjaj != null)
                {
                    var isDeleted = dogadjajService.RemoveDogadjaj(dogadjaj);
                    if (isDeleted)
                    {
                        if(MessageBox.Show("Jeste li sigurno da želite obrisati odabrani događaj", "Brisanje događaja", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        {
                            MessageBox.Show("Događaj je uspješno obrisan!");
                            //notificationTemplate.CreateNewNotification(_loggedUser.id, "Brisanje događaja", "Naziv događaja: " + selectedEvent.EventName+ ".\nOpis događaja: " + selectedEvent.EventDescription);
                            RefreshGUI();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Događaj je u tijeku i ne može se obrisati!");
                    }
                }
            }
            else
            {
                MessageBox.Show("Niste odabrali događaj za brisanje.\nOdaberite događaj kojeg želite obrisati.");
            }
        }
        private dogadjaj GetDogadjajFromEvent(int id) 
        {
            return dogadjajService.GetSpecificDogadjajById(id);
        }

        /*private dogadjaj GetSelectedDogadjaj()
        {
            return dgShowEvents.SelectedItem as dogadjaj;
        }*/

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshGUI();
            ucAdminEventAddNew ucAdminEventAddNew = new ucAdminEventAddNew(dgShowEvents, dogadjajService, HideContentControl, _loggedUser);
            ucAdminEventAddNew.EventAdded += RefreshGUI;
        }

        public void HideContentControl()
        {
            ccShowAddUpdate.Visibility = Visibility.Collapsed;
            svDataGrid.Height = 600;
        }

        private async void RefreshGUI()
        {
            //var prikazDogadaja = await DohvatiDogadjaje();
            // dgShowEvents.ItemsSource = prikazDogadaja;

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
