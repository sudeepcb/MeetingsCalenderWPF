using MeetingsCalenderWPF.Interface;
using System;

namespace MeetingsCalenderWPF.Events
{
    // Custom event class for holding access token events
    public class AccessTokenEvent : EventArgs
    {
        // Property to hold the authentication result
        public IAuthResult AuthResult { get; }

        // Constructor to initialize the AccessTokenEvent with an authentication result
        public AccessTokenEvent(IAuthResult authResult)
        {
            // Ensure authResult is not null before assignment
            AuthResult = authResult ?? throw new ArgumentNullException(nameof(authResult), "AuthResult cannot be null.");
        }
    }
}
