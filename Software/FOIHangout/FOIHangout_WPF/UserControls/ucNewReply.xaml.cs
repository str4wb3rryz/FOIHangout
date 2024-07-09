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
    /// Interaction logic for ucNewReply.xaml
    /// </summary>
    public partial class ucNewReply : UserControl
    {
        private korisnik _loggedUser;
        private int _postId;
        public event Action CommentCancelled;
        public event Action CommentAdded;
        forumService forumService = new forumService();
        private NotificationTemplate notificationTemplate = new NotificationTemplate();

        public ucNewReply(int postId, korisnik loggedUser)
        {
            InitializeComponent();
            _postId = postId;
            _loggedUser = loggedUser;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            txtDate.Text = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
        }

        private void btnCommentCancel_Click(object sender, RoutedEventArgs e)
        {
            closeReplyForm();
        }

        private void btnComment_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtContent.Text))
            {
                MessageBox.Show("Molimo unesite sadržaj komentara");
                return;
            }
            var messageBoxResult = MessageBox.Show("Jeste li sigurni da želite komentirati?", "Novi komentar", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                var post = new forum
                {
                    id_korisnika = _loggedUser.id,
                    naslov = "Odgovor na " + forumService.GetPostById(_postId).FirstOrDefault().naslov,
                    opis = txtContent.Text,
                    datum_objave = DateTime.Now,
                    id_roditeljskog_posta = _postId
                };
                forumService.AddPost(post);
                notificationTemplate.CreateNewNotification(_loggedUser.id, "Kreiranje komentara na post od strane " + _loggedUser.ime + " " + _loggedUser.prezime, "Komentar: " + txtContent.Text);
                MessageBox.Show("Novi komentar uspješno dodan");
                closeReplyForm();
                CommentAdded?.Invoke();
            }
        }

        private void closeReplyForm()
        {
            var parent = this.Parent as ContentControl;
            parent.Content = null;
            CommentCancelled?.Invoke();
        }
    }
}
