using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MeetingsCalenderWPF.Events;
using MeetingsCalenderWPF.Interface;
using MeetingsCalenderWPF.Models;

namespace MeetingsCalenderWPF.Views
{
    public partial class EventDetails : Page
    {
        private readonly IMeetingService _meetingService;
        private IAuthResult _authResult;

        // Constructor
        public EventDetails()
        {
            InitializeComponent();
            // Initialize meeting service
            _meetingService = new MeetingsService();
            _authResult = new AuthResult();

            // Subscribe to access token event
            (Application.Current.MainWindow as MainWindow)._onAccessToken += Window__onAccessToken;
        }

        // Event handler for access token event
        private void Window__onAccessToken(object? sender, AccessTokenEvent e)
        {
            _authResult = e.AuthResult;
            _meetingService._AuthResult = _authResult;
        }

        // Event handler for 'From' date picker selection change
        private void fromDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateMeetingInfo();
        }

        // Event handler for 'To' date picker selection change
        private void toDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateMeetingInfo();
        }

        // Update meeting information based on selected date range
        private async void UpdateMeetingInfo()
        {
            // Check if both 'From' and 'To' dates are selected
            if (fromDatePicker.SelectedDate.HasValue && toDatePicker.SelectedDate.HasValue)
            {
                // Ensure 'To' date is not less than 'From' date
                if (toDatePicker.SelectedDate < fromDatePicker.SelectedDate)
                {
                    MessageBox.Show("To date cannot be less than From date.");
                    toDatePicker.SelectedDate = fromDatePicker.SelectedDate;
                    return;
                }

                // Convert selected dates to UTC format
                var fromDate = fromDatePicker.SelectedDate.Value.ToUniversalTime();
                var toDate = toDatePicker.SelectedDate.Value.ToUniversalTime();

                try
                {
                    // Retrieve and display meeting events
                    eventList.ItemsSource = await _meetingService.GetMeetingEvents(
                        fromDate.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
                        toDate.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"));
                }
                catch (Exception ex)
                {
                    // Display error message if meeting retrieval fails
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }
    }
}
