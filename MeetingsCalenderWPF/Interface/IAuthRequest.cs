using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingsCalenderWPF.Interface
{
    // Define an interface to represent an authentication request
    public interface IAuthRequest
    {
        // Properties representing various aspects of an authentication request
        public string username { get; set; }
        public string password { get; set; }

        public bool magic_token { get; set; }
    }
}
