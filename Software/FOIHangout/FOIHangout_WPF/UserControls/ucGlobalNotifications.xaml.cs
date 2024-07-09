using BusinessLogicLayer;
using BusinessLogicLayer.Services;
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
    /// Interaction logic for ucGlobalNotifications.xaml
    /// </summary>
    public partial class ucGlobalNotifications : UserControl
    {
        private obavijestiService notificationService = new obavijestiService();

        private ContentControl contentControl;
        public ucGlobalNotifications(ContentControl contentControl)
        {
            InitializeComponent();
            this.contentControl = contentControl;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshGUI();
        }

        private async void RefreshGUI()
        {
            var notifications = await notificationService.GetObavijesti();
            var displayNotifications = notifications;
            dgShowGlobalNotifications.ItemsSource = displayNotifications;
        }
    }
}
