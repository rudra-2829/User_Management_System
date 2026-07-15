using AarohiWPFCore;
using AarohiWPFCore.Models;
using AarohiWPFCore.Screens;
using System.Configuration;
using System.Data;
using System.Windows;

namespace UserManagementApplication
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            NavigationEvent.OpenMainWindowRequested += OpenMainWindow;

            UserLoginScreen window = new UserLoginScreen();
            MainWindow = window;
            window.Show();
        }

        private void OpenMainWindow(User user)
        {
            MainWindow mainWindow = new MainWindow(user);
            mainWindow.Show();
        }
    }
}