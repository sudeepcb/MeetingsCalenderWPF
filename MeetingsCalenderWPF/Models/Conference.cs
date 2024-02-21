using MeetingsCalenderWPF.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingsCalenderWPF.Models
{
    // Conference class defining a specific conference
    public class Conference : IConference
    {
        public string country { get; set; }
        public string country_name { get; set; }
        public string number { get; set; }
        public string type { get; set; }
        public string passcode { get; set; }
        public string pin { get; set; }
    }
}
