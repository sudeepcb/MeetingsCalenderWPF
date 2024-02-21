using MeetingsCalenderWPF.Interface; // Import necessary interface
using System;

namespace MeetingsCalenderWPF.Events
{
    // Define a custom event class for login events that inherits from EventArgs
    public class LoginEventArgs : EventArgs
    {
        // Property to hold the authentication request
        public IAuthRequest AuthRequest { get; }

        // Constructor to initialize the LoginEventArgs with an authentication request
        public LoginEventArgs(IAuthRequest authRequest)
        {
            // Ensure authRequest is not null before assignment
            AuthRequest = authRequest ?? throw new ArgumentNullException(nameof(authRequest), "AuthRequest cannot be null.");
        }
    }
}
