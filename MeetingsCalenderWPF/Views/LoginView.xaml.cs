using MeetingsCalenderWPF.Events;
using MeetingsCalenderWPF.Interface;
using MeetingsCalenderWPF.Models;
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

namespace MeetingsCalenderWPF.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {
        public event EventHandler<LoginEventArgs>? _userLoggedIn;
        public LoginView()
        {
            InitializeComponent();
        }

        // Handler for the Login button click event
        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            // Check if username and password are provided
            if (string.IsNullOrWhiteSpace(username.Text) || string.IsNullOrWhiteSpace(password.Text))
            {
                MessageBox.Show("Please enter both username and password.");
                return;
            }

            // Validate username and password
            if (username.Text != "takehome@aircover.ai" || password.Text != "vsdpysMVByK&ir%@iq7*")
            {
                MessageBox.Show("Invalid username or password. Please try again.");
                return;
            }

            // Display success message
            MessageBox.Show("Login successful.");

            // Create authentication request object
            IAuthRequest request = new AuthRequest
            {
                username = username.Text,
                password = password.Text,
                magic_token = magicToken.IsChecked ?? false // Handle nullability of checkbox
            };

            // Invoke the user login event with the authentication request
            _userLoggedIn?.Invoke(this, new LoginEventArgs(request));
        }
    }

}

