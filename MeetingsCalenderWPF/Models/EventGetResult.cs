using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingsCalenderWPF.Models
{
    // Class for deserilzation of meetings data from HttpClient
    public class EventGetResult
    {
        public List<List<EventDetails>> data { get; set; }
        public Meta meta { get; set; }
    }
}
