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
    /// Interaction logic for ucPolls.xaml
    /// </summary>
    public partial class ucPolls : UserControl
    {
        private korisnik _loggedUser;
        korisnikOdabirAnketaService korisnikOdabirAnketaService = new korisnikOdabirAnketaService();
        anketaService anketaService = new anketaService();
        private object originalContent;
        public ucPolls(korisnik loggedUser)
        {
            InitializeComponent();
            _loggedUser = loggedUser;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            loadPolls();
        }
        private async void loadPolls()
        {
            lvPoll.Items.Clear();
            var polls = await anketaService.GetAllAnketaAsync();
            await Task.Run(() =>
            {
                foreach (var poll in polls)
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        lvPoll.Items.Add(new
                        {
                            PollId = poll.id,
                            PollTitle = poll.naslov,
                            PollDesc = poll.opis,
                            IsPollClosed = poll.zatvoren.HasValue && poll.zatvoren.Value,
                            HasVoted = korisnikOdabirAnketaService.hasUserVote(_loggedUser.id, poll.id)
                        });
                    });
                }
            });
        }


        private void lvPoll_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var listViewItem = ((FrameworkElement)e.OriginalSource).DataContext as dynamic;
            if (listViewItem != null)
            {
                originalContent = contentControl.Content;
                ucPollDetails ucPollDetails = new ucPollDetails(_loggedUser, this, listViewItem.PollId);
                contentControl.Content = ucPollDetails;
            }
        }

        public void RestoreOriginalContent()
        {
            loadPolls();
            contentControl.Content = originalContent;
        }
    }
}
