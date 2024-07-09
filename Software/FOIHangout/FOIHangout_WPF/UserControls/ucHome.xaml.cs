using BusinessLogicLayer.Services;
using EntitiesLayer.Entities;
using FOIHangout_WPF.HelperClasses;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Interaction logic for ucHome.xaml
    /// </summary>
    public partial class ucHome : UserControl
    {
        private korisnik _loggedUser;
        forumService forumService = new forumService();
        korisnikService korisnikService = new korisnikService();
        private NotificationTemplate notificationTemplate = new NotificationTemplate();
        public ucHome(korisnik loggedUser)
        {
            InitializeComponent();
            _loggedUser = loggedUser;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            loadForum();
        }

        private void lvPosts_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var listViewItem = ((FrameworkElement)e.OriginalSource).DataContext as dynamic;
            if (listViewItem != null)
            {
                Grid.SetColumnSpan(lvPosts, 1);
                ucPostDetailsAndReplies ucPostDetailsAndReplies = new ucPostDetailsAndReplies(listViewItem.PostId, _loggedUser);
                ucPostDetailsAndReplies.ReplyCommentCancelled += hideShowButtons;
                ucPostDetailsAndReplies.ReplyCommentAdded += loadForum;
                contentControl.Content = ucPostDetailsAndReplies;
                hideShowButtons();
            }
        }

        private void btnAddPost_Click(object sender, RoutedEventArgs e)
        {
            Grid.SetColumnSpan(lvPosts, 1);
            ucNewPost ucNewPost = new ucNewPost(_loggedUser);
            contentControl.Content = ucNewPost;
            hideShowButtons();
        }

        private void btnAddComment_Click(object sender, RoutedEventArgs e)
        {
            if (contentControl.Content is ucPostDetailsAndReplies ucPostDetailsAndReplies)
            {
                ucPostDetailsAndReplies.loadReplyForm();
                hideShowButtons();
            }
        }

        private void btnPostCancel_Click(object sender, RoutedEventArgs e)
        {
            Grid.SetColumnSpan(lvPosts, 3);
            contentControl.Content = null;
            hideShowButtons();
        }

        private void btnPost_Click(object sender, RoutedEventArgs e)
        {
            var ucNewPost = contentControl.Content as ucNewPost;
            if (string.IsNullOrEmpty(ucNewPost.txtTitle.Text) || string.IsNullOrEmpty(ucNewPost.txtContent.Text))
            {
                MessageBox.Show("Niste ispunili sva polja");
                return;
            } 
            var messageBoxResult = MessageBox.Show("Jeste li sigurni da želite objaviti novi post?", "Objava posta", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                addNewPost();
                Grid.SetColumnSpan(lvPosts, 3);
                contentControl.Content = null;
                hideShowButtons();
                loadForum();
            }
        }

        private async void loadForum()
        {
            lvPosts.Items.Clear();
            var postsAsync = await forumService.LoadForumAsync();
            var posts = postsAsync.Where(p => p.id_roditeljskog_posta == null).OrderByDescending(p => p.datum_objave);

            var postModels = new List<dynamic>();

            foreach (var post in posts)
            {
                var userTask = korisnikService.GetUserByIdAsync(post.id_korisnika.Value);
                var numberOfRepliesTask = forumService.GetAllRepliesByPostIdAsync(post.id);

                await Task.WhenAll(userTask, numberOfRepliesTask);

                var user = userTask.Result.FirstOrDefault();
                var numberOfReplies = numberOfRepliesTask.Result.Count;

                bool isBlocked = await blockUserService.IsBlockedAsync(_loggedUser.id, post.id_korisnika.Value);

                if (isBlocked)
                {
                    continue;
                }

                postModels.Add(new
                {
                    PostId = post.id,
                    PostTitle = post.naslov,
                    Poster = user != null ? $"{user.ime} {user.prezime}" : "Nepoznato",
                    DateOfPosting = post.datum_objave,
                    PostDesc = post.opis,
                    PostReplies = numberOfReplies
                });
            }
            lvPosts.Items.Clear();
            foreach (var postModel in postModels)
            {
                lvPosts.Items.Add(postModel);
            }
        }


        private void addNewPost()
        {
            var ucNewPost = contentControl.Content as ucNewPost;
            forum post = new forum
            {
                id_korisnika = _loggedUser.id,
                naslov = ucNewPost.txtTitle.Text,
                opis = ucNewPost.txtContent.Text,
                datum_objave = DateTime.Now,
                id_roditeljskog_posta = null
            };
            forumService.AddPost(post);
            notificationTemplate.CreateNewNotification(_loggedUser.id, "Kreiranje novog posta od strane "+_loggedUser.ime + " "+_loggedUser.prezime, "Naslov posta: "+ucNewPost.txtTitle.Text);
            MessageBox.Show("Nova objava uspješno dodana");
        }

        private void hideShowButtons()
        {
            ucPostDetailsAndReplies ucPostDetailsAndReplies = contentControl.Content as ucPostDetailsAndReplies;
            if (contentControl.Content is ucPostDetailsAndReplies)
            {
                btnAddComment.Visibility = Visibility.Visible;
                btnAddPost.Visibility = Visibility.Visible;
                btnPost.Visibility = Visibility.Hidden;
                btnPostCancel.Visibility = Visibility.Hidden;
                if (ucPostDetailsAndReplies.contentControl.Content is ucNewReply)
                {
                    btnAddComment.Visibility = Visibility.Hidden;
                    btnAddPost.Visibility = Visibility.Hidden;
                    btnPost.Visibility = Visibility.Hidden;
                    btnPostCancel.Visibility = Visibility.Hidden;
                }
            }
            else if (contentControl.Content is ucNewPost)
            {
                btnAddComment.Visibility = Visibility.Hidden;
                btnAddPost.Visibility = Visibility.Hidden;
                btnPost.Visibility = Visibility.Visible;
                btnPostCancel.Visibility = Visibility.Visible;
            }
            else
            {
                btnAddComment.Visibility = Visibility.Hidden;
                btnAddPost.Visibility = Visibility.Visible;
                btnPost.Visibility = Visibility.Hidden;
                btnPostCancel.Visibility = Visibility.Hidden;
            }
        }
    }
}
