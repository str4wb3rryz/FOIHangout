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
    /// Interaction logic for ucFriends.xaml
    /// </summary>
    public partial class ucFriends : UserControl
    {
        korisnikService korisnikService = new korisnikService();
        prijateljiZahtjevZaPrijateljstvoService prijateljiZahtjevZaPrijateljstvoService = new prijateljiZahtjevZaPrijateljstvoService();
        prijateljiService prijateljiService = new prijateljiService();
        private korisnik _loggedUser;
        private List<interesi> _loggedUserInterests;
        private List<korisnik> _users;
        private List<int> declinedUserIds = new List<int>();


        public ucFriends(korisnik loggedUser)
        {
            InitializeComponent();
            _loggedUser = loggedUser;
            _loggedUserInterests = korisnikService.GetUserInterests(_loggedUser.id);
            _users = korisnikService.GetAllUsers().ToList();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            DisplaySimilarInterests(_loggedUser, _users, _loggedUserInterests);
            DisplaySimilarInterests2(_loggedUser, _users, _loggedUserInterests);
            DisplayRecievedFriendRequests();
            DisplayFriends();
        }

        private void DisplaySimilarInterests(korisnik loggedUser, List<korisnik> users, List<interesi> loggedUserInterests)
        {
            lvRecommendedUsers.Items.Clear();
            foreach (var user in users)
            {
                if (user.id != loggedUser.id && !declinedUserIds.Contains(user.id))
                {
                    var userInterests = korisnikService.GetUserInterests(user.id);
                    var similarInterestsCount = userInterests
                        .Count(ui => loggedUserInterests.Any(lui => lui.id == ui.id));

                    if (similarInterestsCount >= 3)
                    {
                        var sentFriendRequests = prijateljiZahtjevZaPrijateljstvoService.GetSentFriendRequestsById2(loggedUser.id);
                        bool alreadySentRequest = sentFriendRequests.Any(r => r.primalac_id == user.id);
                        if (alreadySentRequest) { continue; }

                        var recievedFriendRequests = prijateljiZahtjevZaPrijateljstvoService.GetRecievedFriendRequestsById(loggedUser.id);
                        bool recievedRequest = recievedFriendRequests.Any(r => r.posiljatelj_id == user.id);
                        if (recievedRequest) { continue; }
                        lvRecommendedUsers.Items.Add(new { UserFullName = user.ime + " " + user.prezime });
                    }
                }
            }
        }
        private void DisplaySimilarInterests2(korisnik loggedUser, List<korisnik> users, List<interesi> loggedUserInterests)
        {
            lvRecommendedUsers2.Items.Clear();
            foreach (var user in users)
            {
                if (user.id != loggedUser.id && !declinedUserIds.Contains(user.id))
                {
                    var userInterests = korisnikService.GetUserInterests(user.id);
                    var similarInterestsCount = userInterests
                        .Count(ui => loggedUserInterests.Any(lui => lui.id == ui.id));

                    if (similarInterestsCount == 2)
                    {
                        var sentFriendRequests = prijateljiZahtjevZaPrijateljstvoService.GetSentFriendRequestsById2(loggedUser.id);
                        bool alreadySentRequest = sentFriendRequests.Any(r => r.primalac_id == user.id);
                        if (alreadySentRequest) { continue; }

                        var recievedFriendRequests = prijateljiZahtjevZaPrijateljstvoService.GetRecievedFriendRequestsById(loggedUser.id);
                        bool recievedRequest = recievedFriendRequests.Any(r => r.posiljatelj_id == user.id);
                        if (recievedRequest) { continue; }
                        lvRecommendedUsers2.Items.Add(new { UserFullName = user.ime + " " + user.prezime });
                    }
                }
            }
        }

        private void lvRecommendedUsers_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lvRecommendedUsers.SelectedItem != null)
            {
                var selectedUser = (dynamic)lvRecommendedUsers.SelectedItem;
                var user = _users.FirstOrDefault(u => u.ime + " " + u.prezime == selectedUser.UserFullName);
                if (user != null)
                {
                    MessageBoxResult result = MessageBox.Show($"Želite li poslati zahtjev za prijateljstvo {user.ime} {user.prezime}?", "Pošalji zahtjev za prijateljstvo ", MessageBoxButton.YesNo);

                    if (result == MessageBoxResult.Yes)
                    {
                        prijateljiZahtjevZaPrijateljstvoService.SendFriendRequest(_loggedUser.id, user.id);
                        MessageBox.Show("Zahjtev poslan " + user.ime + " " + user.prezime);
                    }
                    else
                    {
                        lvRecommendedUsers.Items.Remove(selectedUser);
                        declinedUserIds.Add(user.id);
                        MessageBox.Show("Osoba " + user.ime + " " + user.prezime + " više neće biti preporučena");
                    }
                }
            }
            DisplaySimilarInterests(_loggedUser, _users, _loggedUserInterests);
        }

        private void lvRecommendedUsers2_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lvRecommendedUsers2.SelectedItem != null)
            {
                var selectedUser = (dynamic)lvRecommendedUsers2.SelectedItem;
                var user = _users.FirstOrDefault(u => u.ime + " " + u.prezime == selectedUser.UserFullName);
                if (user != null)
                {
                    MessageBoxResult result = MessageBox.Show($"Želite li poslati zahtjev za prijateljstvo {user.ime} {user.prezime}?", "Pošalji zahtjev za prijateljstvo ", MessageBoxButton.YesNo);

                    if (result == MessageBoxResult.Yes)
                    {
                        prijateljiZahtjevZaPrijateljstvoService.SendFriendRequest(_loggedUser.id, user.id);
                        MessageBox.Show("Zahjtev poslan " + user.ime + " " + user.prezime);
                    }
                    else
                    {
                        lvRecommendedUsers.Items.Remove(selectedUser);
                        declinedUserIds.Add(user.id);
                        MessageBox.Show("Osoba " + user.ime + " " + user.prezime + " više neće biti preporučena");
                    }
                }
            }
            DisplaySimilarInterests2(_loggedUser, _users, _loggedUserInterests);

        }

        private void DisplayRecievedFriendRequests()
        {
            lvRecievedFriendRequests.Items.Clear();
            var recievedFriendRequests = prijateljiZahtjevZaPrijateljstvoService.GetRecievedFriendRequestsById(_loggedUser.id);
            foreach (var request in recievedFriendRequests)
            {
                if (request.status == "Poslano")
                {
                    int senderId = Convert.ToInt32(request.posiljatelj_id);
                    var sender = korisnikService.GetUserById(senderId).FirstOrDefault();
                    if (sender != null)
                    {
                        lvRecievedFriendRequests.Items.Add(new { SenderFullName = sender.ime + " " + sender.prezime });
                    }
                }
            }
        }


        private void DisplayFriends()
        {
            lvFriends.Items.Clear();
            var friends = prijateljiService.GetFriends(_loggedUser.id);
            foreach (var friend in friends)
            {
                int friendUserId;
                if (friend.id_korisnika1 == _loggedUser.id)
                {
                    friendUserId = Convert.ToInt32(friend.id_korisnika2);
                }
                else
                {
                    friendUserId = Convert.ToInt32(friend.id_korisnika1);
                }

                var friendUser = korisnikService.GetUserById(friendUserId).FirstOrDefault();
                if (friendUser != null)
                {
                    lvFriends.Items.Add(new { FriendFullName = friendUser.ime + " " + friendUser.prezime });
                }
            }
        }


        private void lvRecievedFriendRequests_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lvRecievedFriendRequests.SelectedItem != null)
            {
                var selectedRequest = (dynamic)lvRecievedFriendRequests.SelectedItem;
                var request = selectedRequest.SenderFullName;
                var requestUser = _users.FirstOrDefault(u => u.ime + " " + u.prezime == request);
                if (requestUser != null)
                {
                    MessageBoxResult result = MessageBox.Show($"Želite li prihvatiti zahtjev za prijateljstvo od {requestUser.ime} {requestUser.prezime}?", "Prihvati zahtjev za prijateljstvo ", MessageBoxButton.YesNo);

                    if (result == MessageBoxResult.Yes)
                    {
                        var recievedFriendRequests = prijateljiZahtjevZaPrijateljstvoService.GetRecievedFriendRequestsById(_loggedUser.id);
                        var requestToUpdate = recievedFriendRequests.FirstOrDefault(r => r.posiljatelj_id == requestUser.id);
                        requestToUpdate.status = "Prihvaćeno";
                        requestToUpdate.prihvacen = true;
                        requestToUpdate.datum_odgovora = DateTime.Now;
                        prijateljiZahtjevZaPrijateljstvoService.Update(requestToUpdate);

                        var prijatelj = new prijatelji
                        {
                            id_korisnika1 = _loggedUser.id,
                            id_korisnika2 = requestUser.id
                        };
                        prijateljiService.Add(prijatelj);

                        MessageBox.Show("Zahjtev prihvaćen " + requestUser.ime + " " + requestUser.prezime);
                        DisplayRecievedFriendRequests();
                        DisplayFriends();
                    }
                    else
                    {
                        var recievedFriendRequests = prijateljiZahtjevZaPrijateljstvoService.GetRecievedFriendRequestsById(_loggedUser.id);
                        var requestToUpdate = recievedFriendRequests.FirstOrDefault(r => r.posiljatelj_id == requestUser.id);
                        requestToUpdate.status = "Odbijeno";
                        requestToUpdate.prihvacen = false;
                        requestToUpdate.datum_odgovora = DateTime.Now;
                        prijateljiZahtjevZaPrijateljstvoService.Update(requestToUpdate);

                        MessageBox.Show("Zahjtev odbijen " + requestUser.ime + " " + requestUser.prezime);
                        DisplayRecievedFriendRequests();
                        DisplayFriends();
                    }
                }
            }
        }

    }
}