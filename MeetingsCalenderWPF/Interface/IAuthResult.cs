using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingsCalenderWPF
{
    // Define an interface to represent an authentication response
    public interface IAuthResult
    {
        List<AuthData> data { get; set; }
        Meta meta { get; set; }
    }

    // Define an interface to represent an authentication response data
    public class AuthData
    {
        public string access_token { get; set; }
        public string refresh_token { get; set; }
        public string magic_token { get; set; }
        public long access_token_expires { get; set; }
        public long refresh_token_expires { get; set; }
        public long magic_token_expires { get; set; }
        public bool google_linked { get; set; }
        public bool google_scopes_need_update { get; set; }
        public bool zoom_linked { get; set; }
        public bool sfdc_linked { get; set; }
        public bool highspot_linked { get; set; }
        public bool salesloft_linked { get; set; }
        public bool ms_linked { get; set; }
        public bool account_verified { get; set; }
        public List<string> customer_org_list { get; set; }
    }

    public class Meta
    {
    }
}
