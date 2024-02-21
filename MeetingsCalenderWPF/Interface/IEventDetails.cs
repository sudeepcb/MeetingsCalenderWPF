using MeetingsCalenderWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingsCalenderWPF.Interface
{
    // Declaring the interface IEventDetails
    public interface IEventDetails
    {
        // Properties of the interface
        // These properties define the details of an event
        public string id { get; set; }
        public List<Attendee> attendees { get; set; }
        public string description { get; set; }
        public DateTime end_time { get; set; }
        public string room { get; set; }
        public string location { get; set; }
        public string owner { get; set; }
        public DateTime start_time { get; set; }
        public string summary { get; set; }
        public int conference_service { get; set; }
        public int conference_service_redirect { get; set; }
        public string backup_url { get; set; }
        public List<Conference> conference_bridges { get; set; }
        public string actual_start_time { get; set; }
        public string actual_end_time { get; set; }
        public int deal_id { get; set; }
        public string customer { get; set; }
        public List<string> allowed_org_list { get; set; }
        public string prospect { get; set; }
        public string room_password { get; set; }
        public string passcode { get; set; }
        public string gcal_event_id { get; set; }
        public string external_owner { get; set; }
        public bool should_transcribe { get; set; }
        public object notes { get; set; }
        public object detailed_notes { get; set; }
        public string customer_summary { get; set; }
        public string draft_followup_email { get; set; }
        public object notetaker_removed_event_ids { get; set; }
        public string language { get; set; }
        public DateTime last_updated { get; set; }
    }
}
