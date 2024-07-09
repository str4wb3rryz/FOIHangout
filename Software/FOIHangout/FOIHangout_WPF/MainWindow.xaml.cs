using BusinessLogicLayer.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.IO;
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

namespace FOIHangout_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        korisnikService services = new korisnikService();
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ucLogin ucLogin = new ucLogin();
            contentControl.Content = ucLogin;
            await LoadUserControlAsync();
        }

        private async Task LoadUserControlAsync()
        {
            await services.GetUsersAsync();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F1)
            {
                if (contentControl.Content is UserControl userControl)
                {
                    string userControlName = userControl.GetType().Name;

                    string topic = string.Empty;
                    switch (userControlName)
                    {
                        case "ucLogin":
                            topic = "login.htm";
                            break;
                        case "ucRegister":
                        case "ucRegisterSecond":
                        case "ucRegisterThird":
                            topic = "registration.htm";
                            break;
                        default:
                            topic = "main.htm";
                            break;
                    }

                    string currentDirectory = Directory.GetCurrentDirectory();
                    string filePath = System.IO.Path.Combine(currentDirectory, "UserGuide.chm");
                    System.Windows.Forms.Help.ShowHelp(null, filePath, System.Windows.Forms.HelpNavigator.Topic, topic);
                }
            }
        }
    }
}
