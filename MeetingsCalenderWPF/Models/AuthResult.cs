using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MeetingsCalenderWPF.IAuthResult;

namespace MeetingsCalenderWPF.Models
{
    // // Class for deserialzation of response from POST HtpClient
    class AuthResult : IAuthResult
    {
        public List<AuthData> data { get; set; }
        public Meta meta { get; set; }
    }
}
