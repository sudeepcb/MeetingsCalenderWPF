using MeetingsCalenderWPF.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingsCalenderWPF.Models
{
    // Attendee class defining a specific group of attendees
    public class Attendee : IAttendee
    {
        public string company { get; set; }
        public string email { get; set; }
        public string external_email { get; set; }
        public string name { get; set; }
        public object join_time { get; set; }
        public object leave_time { get; set; }
        public string salesloft_id { get; set; }
        public string zoom_user_id { get; set; }
        public bool in_meeting { get; set; }
    }
}
