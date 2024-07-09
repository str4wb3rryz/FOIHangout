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
    /// Interaction logic for ucPollDetails.xaml
    /// </summary>
    public partial class ucPollDetails : UserControl
    {
        private korisnik _loggedUser;
        private ucPolls parentUcVoting;
        private int _pollID;
        bool isClosed;
        bool isYesNoChoice;
        bool hasUserVoted;
        anketaService anketaService = new anketaService();
        korisnikOdabirAnketaService korisnikOdabirAnketaService = new korisnikOdabirAnketaService();
        public ucPollDetails(korisnik loggedUser, ucPolls parent, int pollID)
        {
            InitializeComponent();
            _loggedUser = loggedUser;
            parentUcVoting = parent;
            _pollID = pollID;
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            loadPollDetails();
            checkRadioButtons();
        }
        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            parentUcVoting.RestoreOriginalContent();
        }

        private void loadPollDetails()
        {
            var poll = anketaService.GetAnketaById(_pollID).FirstOrDefault();
            if (poll == null) return;

            tbPollTitle.Text = poll.naslov;
            tbPollDesc.Text = poll.opis;

            isClosed = (bool)poll.zatvoren;
            isYesNoChoice = (bool)poll.izbor_da_ne;
            hasUserVoted = korisnikOdabirAnketaService.hasUserVote(_loggedUser.id, _pollID);

            rbYesNo.Visibility = isYesNoChoice ? Visibility.Visible : Visibility.Collapsed;
            spChoices.Visibility = !isYesNoChoice ? Visibility.Visible : Visibility.Collapsed;
            tbVotingEnded.Visibility = isClosed ? Visibility.Visible : Visibility.Collapsed;
            tbVotingDone.Visibility = hasUserVoted ? Visibility.Visible : Visibility.Collapsed;
            tbVotingResults.Visibility = isClosed ? Visibility.Visible : Visibility.Collapsed;
            btnAddVote.Visibility = isClosed ? Visibility.Collapsed : Visibility.Visible;

            if (!isYesNoChoice)
            {
                var choices = anketaService.GetPollChoicesById(_pollID).Select(x => x.odabir).ToList();
                spChoices.ItemsSource = choices;
            }

            if (isClosed || hasUserVoted)
            {
                rbYes.IsEnabled = false;
                rbNo.IsEnabled = false;
                spChoices.IsEnabled = false;
                showVotingResults();
            }
        }

        private void showVotingResults()
        {
            var choices = anketaService.GetPollChoicesById(_pollID);
            Dictionary<int, int> voteCounts = korisnikOdabirAnketaService.CountVotesForEachChoice(_pollID);
            Dictionary<string, int> percentageVotes = korisnikOdabirAnketaService.CalculatePercentageOfVotesForEachChoice(_pollID);

            StringBuilder sb = new StringBuilder();
            foreach (var choice in choices)
            {
                var voteCount = voteCounts.ContainsKey(choice.id) ? voteCounts[choice.id] : 0;
                var percentage = percentageVotes.ContainsKey(choice.odabir) ? percentageVotes[choice.odabir] : 0;
                sb.AppendLine($"{choice.odabir}: {voteCount} glasova ({percentage}%)");
            }

            tbVotingResults.Text = sb.ToString();
        }

        private void checkRadioButtons()
        {
            var userVote = korisnikOdabirAnketaService.getUsersVoteFromPoll(_loggedUser.id, _pollID).FirstOrDefault();
            if (userVote != null)
            {
                if (isYesNoChoice)
                {
                    if (userVote.id_odabira == 14)
                        rbYes.IsChecked = true;
                    else if (userVote.id_odabira == 15)
                        rbNo.IsChecked = true;
                }
                else
                {
                    var voteId = userVote.id_odabira;
                    var voteName = anketaService.GetPollChoicesById(_pollID).FirstOrDefault(choice => choice.id == voteId)?.odabir;
                    foreach (var item in spChoices.Items)
                    {
                        var choiceName = item.ToString();
                        RadioButton rb = getRadioButtonByName(choiceName);
                        if (rb != null && Equals(voteName, choiceName))
                        {
                            rb.IsChecked = true;
                            break;
                        }
                    }
                }
            }
        }

        private RadioButton getRadioButtonByName(string choiceName)
        {
            for (int i = 0; i < spChoices.Items.Count; i++)
            {
                var container = spChoices.ItemContainerGenerator.ContainerFromIndex(i);
                if (container == null) { 
                    spChoices.UpdateLayout(); 
                    container = spChoices.ItemContainerGenerator.ContainerFromIndex(i); 
                }
                if (container is ContentPresenter presenter)
                {
                    var radioButton = FindVisualChild<RadioButton>(presenter);
                    if (radioButton != null && radioButton.Content.ToString() == choiceName)
                    {
                        return radioButton;
                    }
                }
            }
            return null;
        }

        private void btnAddVote_Click(object sender, RoutedEventArgs e)
        {
            string message = "Niste odabrali nijednu opciju!";

            if (isYesNoChoice)
            {
                if (rbYes.IsChecked == true)
                    message = SaveVoteAndClosePoll("Da"); //14
                else if (rbNo.IsChecked == true)
                    message = SaveVoteAndClosePoll("Ne"); //15
            }
            else
            {
                var selectedRadioButton = GetSelectedRadioButtonFromItemsControl(spChoices);
                if (selectedRadioButton != null)
                {
                    var selectedChoice = selectedRadioButton.Content.ToString();
                    message = SaveVoteAndClosePoll(selectedChoice);
                }
            }

            if(message != "")
                MessageBox.Show(message);
        }

        private string SaveVoteAndClosePoll(string selectedChoice)
        {
            string message = "";
            var choiceId = isYesNoChoice ? (selectedChoice == "Da" ? 14 : 15) : anketaService.GetPollChoicesById(_pollID).FirstOrDefault(choice => choice.odabir == selectedChoice)?.id ?? 0;
            if (MessageBox.Show($"Jeste li sigurni da želite glasati za opciju '{selectedChoice}'?", "Potvrda glasanja", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                var vote = new korisnik_odabir_anketa
                {
                    id_korisnik = _loggedUser.id,
                    id_anketa = _pollID,
                    id_odabira = choiceId
                };
                var voteAdded = korisnikOdabirAnketaService.AddVote(vote);
                if (voteAdded)
                {
                    parentUcVoting.RestoreOriginalContent();
                    message = $"Vaš glas za opciju '{selectedChoice}' je uspješno zabilježen.";
                }
                else
                {
                    message = "Dogodila se greška prilikom zabilježavanja vašeg glasa.";
                }            
            }
            return message;
        }

        private RadioButton GetSelectedRadioButtonFromItemsControl(ItemsControl itemsControl)
        {
            for (int i = 0; i < itemsControl.Items.Count; i++)
            {
                var container = itemsControl.ItemContainerGenerator.ContainerFromIndex(i);
                if (container is ContentPresenter presenter)
                {
                    var radioButton = FindVisualChild<RadioButton>(presenter);
                    if (radioButton != null && radioButton.IsChecked == true)
                    {
                        return radioButton;
                    }
                }
            }
            return null;
        }

        private T FindVisualChild<T>(DependencyObject obj) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child is T)
                {
                    return (T)child;
                }
                else
                {
                    T childOfChild = FindVisualChild<T>(child);
                    if (childOfChild != null)
                    {
                        return childOfChild;
                    }
                }
            }
            return null;
        }
    }
}
