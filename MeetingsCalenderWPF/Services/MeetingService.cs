using MeetingsCalenderWPF.Interface;
using MeetingsCalenderWPF.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace MeetingsCalenderWPF
{
    public class MeetingsService : IMeetingService
    {
        private readonly HttpClient _httpClient;
        public IAuthResult _authResult;
        public List<EventDetails> _meetingEvents;

        public MeetingsService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://api.aircover.ai")
            };
            _authResult = new AuthResult();
            _meetingEvents = new List<EventDetails>();
        }

        public IAuthResult _AuthResult { get; set; }
        List<EventDetails> IMeetingService._meetingEvents { get; set; }

        public async Task GetJWTToken(IAuthRequest authRequest)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync("/auth/login", authRequest);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                _authResult = JsonSerializer.Deserialize<AuthResult>(responseBody);
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Failed to retrieve JWT token.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while processing JWT token response.", ex);
            }
        }

        public async Task<List<EventDetails>> GetMeetingEvents(string start_date, string end_date)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _authResult?.data?[0]?.access_token);

                HttpResponseMessage response = await _httpClient.GetAsync($"/meetings/?start={start_date}&end={end_date}");
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                var meetingsResult = JsonSerializer.Deserialize<EventGetResult>(responseBody);

                _meetingEvents.Clear();
                foreach (var data in meetingsResult?.data)
                {
                    foreach (var item in data)
                    {
                        _meetingEvents.Add(item);
                    }
                }

                return _meetingEvents;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Failed to retrieve meeting events.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while processing meeting events response.", ex);
            }
        }
    }
}
