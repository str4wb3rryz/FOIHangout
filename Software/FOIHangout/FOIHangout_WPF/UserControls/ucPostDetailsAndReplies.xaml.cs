using BusinessLogicLayer.Services;
using EntitiesLayer.Entities;
using FOIHangout_WPF.Windows;
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
    /// Interaction logic for ucPostDetailsAndReplies.xaml
    /// </summary>
    public partial class ucPostDetailsAndReplies : UserControl
    {
        private korisnik _loggedUser;
        private int _postId;
        forumService forumService = new forumService();
        korisnikService korisnikService = new korisnikService();
        public event Action ReplyCommentCancelled;
        public event Action ReplyCommentAdded;

        public ucPostDetailsAndReplies(int postId, korisnik loggedUser)
        {
            InitializeComponent();
            _postId = postId;
            _loggedUser = loggedUser;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            loadPostDetails();
            loadReplies();
        }

        private void loadPostDetails()
        {
            var post = forumService.GetPostById(_postId).FirstOrDefault();
            tbPostTitle.Text = post.naslov;
            tbPostDesc.Text = post.opis;
            var user = korisnikService.GetUserById(post.id_korisnika.Value).FirstOrDefault();
            tbPoster.Text = user != null ? $"{user.ime} {user.prezime}" : "Nepoznato";
            tbDateOfPosting.Text = post.datum_objave.ToString();
        }
        async private void loadReplies()
        {
            lvReplies.Items.Clear();
            var replies = forumService.GetAllRepliesByPostId(_postId);
            if (replies == null || replies.Count() == 0)
            {
                lvReplies.Items.Add(new
                {
                    ReplyId = 0,
                    ReplyDesc = "Nema komentara",
                });
                return;
            }
            else
            {
                lvReplies.Items.Clear();
                foreach (var reply in replies)
                {
                    var user = korisnikService.GetUserById(reply.id_korisnika.Value).FirstOrDefault();

                    bool isBlocked = await blockUserService.IsBlockedAsync(_loggedUser.id, user.id);

                    if (isBlocked)
                    {
                        continue;
                    }

                    lvReplies.Items.Add(new
                    {
                        ReplyId = reply.id,
                        ReplyDesc = reply.opis,
                        ReplyDate = reply.datum_objave,
                        ReplyPoster = user != null ? $"{user.ime} {user.prezime}" : "Nepoznato"
                    });
                }
            }
        }
        public void loadReplyForm()
        {
            ucNewReply ucNewReply = new ucNewReply(_postId, _loggedUser);
            contentControl.Content = ucNewReply;
            ucNewReply.CommentCancelled += () => { 
                ReplyCommentCancelled?.Invoke(); 
            };
            ucNewReply.CommentAdded += () => {
                loadReplies();
                ReplyCommentAdded?.Invoke();
            };
        }

        private void imgUserPicture_MouseDown(object sender, MouseButtonEventArgs e)
        {
            openInfoAboutOtherUsers();
        }

        private void tbPoster_MouseDown(object sender, MouseButtonEventArgs e)
        {
            openInfoAboutOtherUsers();
        }

        private void openInfoAboutOtherUsers()
        {
            var post = forumService.GetPostById(_postId).FirstOrDefault();
            var user = korisnikService.GetUserById(post.id_korisnika.Value).FirstOrDefault();
            CheckOtherUserInfo checkOtherUserInfo = new CheckOtherUserInfo(user, _loggedUser);
            checkOtherUserInfo.ShowDialog();
        }
    }
}
