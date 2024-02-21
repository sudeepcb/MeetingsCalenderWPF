// Importing necessary namespaces
using MeetingsCalenderWPF.Interface; // Importing the IMeetingService interface
using MeetingsCalenderWPF.Models; // Importing the necessary models
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

// Declaring a namespace for the class
namespace MeetingsCalenderWPF
{
    // Declaring the class MeetingsService which implements the IMeetingService interface
    internal class MeetingsService : IMeetingService
    {
        // Properties of the class
        // These properties define the attributes of the meeting service
        public IAuthResult _AuthResult { get; set; } // Authentication result object
        public List<EventDetails> _meetingEvents { get; set; } // List of meeting events

        // Private member variable
        private readonly HttpClient _httpClient;

        // Constructor for the MeetingsService class
        public MeetingsService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://api.aircover.ai"), // Setting the base address for the HTTP client
            };
            _AuthResult = new AuthResult(); // Initializing the authentication result object
            _meetingEvents = new List<EventDetails>(); // Initializing the list of meeting events
        }

        // Method to get a JWT token for authentication
        public async Task GetJWTToken(IAuthRequest authRequest)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync("/auth/login", authRequest); // Sending a POST request to obtain a JWT token

                if (response.IsSuccessStatusCode) // Checking if the request was successful
                {
                    string responseBody = await response.Content.ReadAsStringAsync(); // Reading the response body

                    if (responseBody != null) // Checking if the response body is not null
                    {
                        _AuthResult = JsonSerializer.Deserialize<AuthResult>(responseBody); // Deserializing the response body into an AuthResult object
                    }
                }
            }
            catch (Exception e)
            {
                throw; // Throwing any exceptions that occur
            }
        }

        // Method to get meeting events within a specified date range
        public async Task<List<EventDetails>> GetMeetingEvents(string start_date, string end_date)
        {
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", _AuthResult.data[0].access_token); // Setting the authorization header using the JWT token

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"/meetings/?start={start_date}&end={end_date}"); // Sending a GET request to retrieve meeting events

                if (response.IsSuccessStatusCode) // Checking if the request was successful
                {
                    string responseBody = await response.Content.ReadAsStringAsync(); // Reading the response body

                    if (responseBody != null) // Checking if the response body is not null
                    {
                        var meetingsResult = JsonSerializer.Deserialize<EventGetResult>(responseBody); // Deserializing the response body into an EventGetResult object
                        foreach (var data in meetingsResult.data) // Iterating through the retrieved meeting data
                        {
                            foreach (var item in data) // Iterating through each item in the data
                            {
                                _meetingEvents.Add(item); // Adding the meeting event to the list of meeting events
                            }
                        }
                    }
                }

                return _meetingEvents; // Returning the list of meeting events
            }
            catch (Exception e)
            {
                throw; // Throwing any exceptions that occur
            }
        }
    }
}
