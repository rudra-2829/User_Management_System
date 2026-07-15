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
    /// Interaction logic for AddEditScreen.xaml
    /// </summary>
    public partial class AddEditScreen : Window
    {
        private User? selectedUser;
        public AddEditScreen()
        {
            InitializeComponent();
        }

        public AddEditScreen(User user)
        {
            InitializeComponent();

            selectedUser = user;

            txtUserId.Text = user.Id.ToString();
            txtFirstName.Text = user.FirstName;
            txtLastName.Text = user.LastName;
            txtEmailAddress.Text = user.EmailAddress;
            txtUserName.Text = user.UserName;
            txtPassword.Password = user.Password;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            UserServices userServices = new UserServices();
            if(selectedUser == null)
            {
                if(txtFirstName.Text == "" || txtLastName.Text == "" || txtEmailAddress.Text == "" || txtUserName.Text == "" || txtPassword.Password == "")
                {
                    MessageBox.Show("Please fill in all fields.");
                    return;
                }
                User newUser = new User
                {
                    FirstName = txtFirstName.Text,
                    LastName = txtLastName.Text,
                    EmailAddress = txtEmailAddress.Text,
                    UserName = txtUserName.Text,
                    Password = txtPassword.Password
                };
                userServices.AddUser(newUser);
            }
            else
            {
                if (txtFirstName.Text == selectedUser.FirstName && txtLastName.Text == selectedUser.LastName && txtEmailAddress.Text == selectedUser.EmailAddress && txtUserName.Text == selectedUser.UserName && txtPassword.Password == selectedUser.Password)
                {
                    MessageBox.Show("Can't show any Update in User Details, Try Again");
                    return;
                }
                selectedUser.FirstName = txtFirstName.Text;
                selectedUser.LastName = txtLastName.Text;
                selectedUser.EmailAddress = txtEmailAddress.Text;
                selectedUser.UserName = txtUserName.Text;
                selectedUser.Password = txtPassword.Password;
                userServices.UpdateEmployee(selectedUser);
            }
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
