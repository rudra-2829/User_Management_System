using AarohiWPFCore.Models;
using AarohiWPFCore.Services;
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
using System.Windows.Shapes;

namespace AarohiWPFCore.Screens
{
    /// <summary>
    /// Interaction logic for UserManageScreen.xaml
    /// </summary>
    public partial class UserManageScreen : Window
    {
        private User selectedUser;

        public UserManageScreen(User user)
        {
            InitializeComponent();

            selectedUser = user;

            LoadUser();
        }
        public UserManageScreen()
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

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddEditScreen addEditScreen = new AddEditScreen();
            addEditScreen.ShowDialog();
            LoadUser(); // Refresh the user list after adding a new user
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            User selectedUser = userData.SelectedItem as User;
            if (selectedUser == null)
            {
                MessageBox.Show("Please select a user to edit.", "No User Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            AddEditScreen addEditScreen = new AddEditScreen(selectedUser);
            addEditScreen.ShowDialog();
            LoadUser(); // Refresh the user list after editing a user
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            User selectedUser = userData.SelectedItem as User;
            if (selectedUser == null)
            {
                MessageBox.Show("Please select a user to delete.", "No User Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            MessageBoxResult result = MessageBox.Show($"Are you sure you want to delete {selectedUser.FirstName} {selectedUser.LastName}?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                UserServices userServices = new UserServices();
                userServices.DeleteUser(selectedUser.Id);
                LoadUser();
            }
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
