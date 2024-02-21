using MeetingsCalenderWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MeetingsCalenderWPF.Interface
{
    // Declaring the interface IMeetingService
    public interface IMeetingService
    {
        // Properties of the interface
        // These properties define the attributes and behavior of the meeting service
        public IAuthResult _AuthResult { get; set; }

        public List<EventDetails> _meetingEvents { get; set; }
        public Task GetJWTToken(IAuthRequest authRequest);

        public Task<List<EventDetails>> GetMeetingEvents(string start_date, string end_date);
    }
}
