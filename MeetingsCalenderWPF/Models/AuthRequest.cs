using MeetingsCalenderWPF.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingsCalenderWPF.Models
{
    // Class for serialzation of authrequest for POST HtpClient
    class AuthRequest : IAuthRequest
    {
        public string username { get; set; } = null!;
        public string password { get; set; } = null!;
        public bool magic_token { get; set; }
    }
}
