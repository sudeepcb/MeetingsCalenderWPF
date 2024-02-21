using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingsCalenderWPF.Interface
{
    // Declaring the interface IConference
    public interface IConference
    {
        // Properties of the interface
        // These properties define the attributes of a conference
        public string country { get; set; }
        public string country_name { get; set; }
        public string number { get; set; }
        public string type { get; set; }
        public string passcode { get; set; }
        public string pin { get; set; }
    }
}
