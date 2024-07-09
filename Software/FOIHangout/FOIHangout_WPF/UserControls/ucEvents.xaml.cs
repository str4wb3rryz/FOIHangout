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
    public partial class ucEvents : UserControl
    {
        private dogadjajService dogadjajService = new dogadjajService();
        private ContentControl contentControl;

        public ucEvents(ContentControl contentControl)
        {
            InitializeComponent();
            this.contentControl = contentControl;
        }
        private dogadjaj GetDogadjajFromEvent(int id)
        {
            return dogadjajService.GetSpecificDogadjajById(id);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshGUI();
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
