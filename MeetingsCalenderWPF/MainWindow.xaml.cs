using MeetingsCalenderWPF.Events;
using MeetingsCalenderWPF.Interface;
using MeetingsCalenderWPF.Models;
using MeetingsCalenderWPF.Views;
using System;
using System.Windows;
using EventDetails = MeetingsCalenderWPF.Views.EventDetails;

namespace MeetingsCalenderWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IMeetingService _meetingService;
        public event EventHandler<AccessTokenEvent>? _onAccessToken;
        private IAuthResult _authResult { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            _meetingService = new MeetingsService();
            _authResult = new AuthResult();
            loginView._userLoggedIn += LoginView_UserLoggedIn;
        }

        private async void LoginView_UserLoggedIn(object sender, LoginEventArgs e)
        {
            try
            {
                await _meetingService.GetJWTToken(e.AuthRequest);
                _authResult = _meetingService._AuthResult;

                // Navigate to the event details page
                EventFrame.Navigate(new EventDetails());

                // Invoke the access token event
                _onAccessToken?.Invoke(this, new AccessTokenEvent(_authResult));

                // Hide the login view after successful login
                loginView.Visibility = Visibility.Collapsed;

                // Show informational message
                MessageBox.Show("Set From and To Dates to View Calendar Events");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
    }
}
