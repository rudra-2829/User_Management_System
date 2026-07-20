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
    /// Interaction logic for UserLoginScreen.xaml
    /// </summary>
    public partial class UserLoginScreen : Window
    {
        public UserLoginScreen()
        {
            InitializeComponent();
        }

        public void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUserName.Text;
            string password = txtPassword.Password;

            if (string.IsNullOrWhiteSpace(username))
            {
                MessageBox.Show("Please enter Username");
                return;
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter Password");
                return;
            }

            UserServices userService = new UserServices();

            User? user = userService.UserLogin(
                txtUserName.Text,
                txtPassword.Password);

            if (user != null)
            {
                MessageBox.Show($"Welcome {user.FirstName}!");

                NavigationEvent.RaiseOpenMainWindow(user);
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid username or password.");
            }
        }
    }
}
