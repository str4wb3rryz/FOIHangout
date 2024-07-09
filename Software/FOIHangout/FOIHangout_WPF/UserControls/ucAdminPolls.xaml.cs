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
    /// Interaction logic for ucAdminPolls.xaml
    /// </summary>
    public partial class ucAdminPolls : UserControl
    {
        private korisnik _loggedUser;
        anketaService anketaService = new anketaService();
        odabirService odabirService = new odabirService();
        korisnikOdabirAnketaService korisnikOdabirAnketaService = new korisnikOdabirAnketaService();
        private NotificationTemplate notificationTemplate = new NotificationTemplate();
        public ucAdminPolls(korisnik loggedUser)
        {
            InitializeComponent();
            _loggedUser = loggedUser;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            loadPolls();
            ucNewPoll ucNewPoll = new ucNewPoll(_loggedUser, HideContentControl);
            ucNewPoll.PoolAdded += loadPolls;
        }
        private async void loadPolls()
        {
            lvPolls.Items.Clear();
            var polls = await anketaService.GetAllAnketaAsync();
            await Task.Run(() =>
            {
                foreach (var poll in polls)
                {
                    var choices = poll.izbor_visestruki?.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                                       .Select(choice => choice.Trim())
                                       .ToList() ?? new List<string> { " " };
                    if ((bool)poll.izbor_da_ne)
                    {
                        choices.Clear();
                        choices.Add("----------");
                    }
                    this.Dispatcher.Invoke(() =>
                    {
                        lvPolls.Items.Add(new
                        {
                            PollId = poll.id,
                            PollTitle = poll.naslov,
                            PollDesc = poll.opis,
                            PollMultipleChoices = choices,
                            IsPollClosed = poll.zatvoren.HasValue && poll.zatvoren.Value ? "Da" : "Ne"
                        });
                    });
                }
            });
        }


        private void btnAddPoll_Click(object sender, RoutedEventArgs e)
        {
            contentControl.Visibility = Visibility.Visible;
            ucNewPoll ucNewPoll = new ucNewPoll(_loggedUser, HideContentControl);
            ucNewPoll.PoolAdded += loadPolls;
            contentControl.Content = ucNewPoll;
            svListView.Height = 260;
        }

        public void HideContentControl()
        {
            contentControl.Visibility = Visibility.Collapsed;
            svListView.Height = 600;
        }

        private void btnCloseVoting_Click(object sender, RoutedEventArgs e)
        {
            dynamic selectedItem = lvPolls.SelectedItem;
            if (selectedItem != null)
            {
                var selectedPoll = (dynamic)lvPolls.SelectedItem;
                int pollId = selectedPoll.PollId;
                var pollToClose = anketaService.GetAnketaById(pollId).FirstOrDefault();

                if (pollToClose != null)
                {
                    if ((bool)pollToClose.zatvoren)
                    {
                        MessageBox.Show("Glasanje je već zatvoreno.");
                        return;
                    }
                    if (MessageBox.Show("Jeste li sigurni da želite zatvoriti glasanje za odabranu anketu?", "Zatvaranje ankete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        pollToClose.zatvoren = true;
                        anketaService.UpdateAnketa(pollToClose);
                        notificationTemplate.CreateNewNotification(_loggedUser.id, "Zatvaranje glasova ankete od strane " + _loggedUser.ime + " " + _loggedUser.prezime+".\nRezultate možete vidjeti u pregledu anteta.", "Naslov ankete: " + selectedPoll.PollTitle);
                        MessageBox.Show("Glasanje za anketu je uspješno zatvoreno. \n----------\n" + votingResults(pollId));
                        loadPolls();
                    }
                }
                else
                {
                    MessageBox.Show("Odabrana anketa nije pronađena.");
                }
            }
            else
            {
                MessageBox.Show("Molimo odaberite anketu koju želite zatvoriti.");
            }
        }

        private void lvPolls_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            dynamic selectedItem = lvPolls.SelectedItem;
            if (selectedItem != null)
            {
                var selectedPoll = (dynamic)lvPolls.SelectedItem;
                int pollId = selectedPoll.PollId;
                var poll = anketaService.GetAnketaById(pollId).FirstOrDefault();
                if (poll != null)
                {
                    if (!(bool)poll.zatvoren)
                    {
                        MessageBox.Show("Prvo zatvorite glasanje da bi se mogli vidjeti rezultati");
                        return;
                    } else
                    {
                        MessageBox.Show(votingResults(pollId));
                    }
                }
                else
                {
                    MessageBox.Show("Odabrana anketa nije pronađena.");
                }
            }
            else
            {
                MessageBox.Show("Greška.");
            }
        }

        private string votingResults(int pollId)
        {
            var choices = anketaService.GetPollChoicesById(pollId);
            Dictionary<int, int> voteCounts = korisnikOdabirAnketaService.CountVotesForEachChoice(pollId);
            Dictionary<string, int> percentageVotes = korisnikOdabirAnketaService.CalculatePercentageOfVotesForEachChoice(pollId);
            string message = "Rezultati glasanja:\n";
            foreach (var choice in choices)
            {
                var voteCount = voteCounts.ContainsKey(choice.id) ? voteCounts[choice.id] : 0;
                var percentage = percentageVotes.ContainsKey(choice.odabir) ? percentageVotes[choice.odabir] : 0;
                message += $"Izbor: {choice.odabir}, Broj glasova: {voteCount} ({percentage}%)\n";
            }
            return message;
        }
    }
}
