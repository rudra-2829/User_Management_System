using AarohiWPFCore.Models;
using AarohiWPFCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AarohiWPFCore.Screens
{
    /// <summary>
    /// Interaction logic for UserDetailScreen.xaml
    /// </summary>
    public partial class UserDetailScreen : Window
    {
        public UserDetailScreen()
        {
            InitializeComponent();
            LoadUser();
        }

        private void LoadUser()
        {
            UserServices userServices = new UserServices();
            List<User> users = userServices.GetUsers();

            userData.ItemsSource = users;
        }
        private void btnShow_Click(object sender, RoutedEventArgs e)
        {
            User selectedUser = userData.SelectedItem as User;
            if(selectedUser == null)
            {
                MessageBox.Show("Please select a user to view details.", "No User Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            NavigationEvent.RaiseOpenMainWindow(selectedUser);
            this.Close();
            this.Hide();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
