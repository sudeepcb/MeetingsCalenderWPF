using Moq;
using AutoFixture;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using MeetingsCalenderWPF;
using System.Windows.Documents;
using Moq.Protected;
using MeetingsCalenderWPF.Models;
using FluentAssertions;


namespace MeetingsCalenderWPF.Tests
{
    [TestClass]
    public class MeetingsService_Tests
    {
        private readonly Fixture _fixture;
        private readonly TestHttpMessageHandler _handlerMock;
        private readonly HttpClient _httpClient;

        public MeetingsService_Tests()
        {
            _fixture = new Fixture();
            _fixture.Customize(new Customizations());
            _handlerMock = new TestHttpMessageHandler();
            _httpClient = new HttpClient(_handlerMock)
            {
                BaseAddress = new Uri("https://api.aircover.ai")
            };
        }

        [TestMethod]
        public async Task GetJWTToken_Success()
        {
            // Arrange
            var service = new MeetingsService(_httpClient);
            var authRequest = _fixture.Create<AuthRequest>();
            var expectedToken = _fixture.Create<AuthResult>();
            expectedToken.meta = new Meta();

            _handlerMock.Response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonSerializer.Serialize(expectedToken))
            };

            // Act
            await service.GetJWTToken(authRequest);

            // Assert using FluentAssertions
            service._AuthResult.Should().BeEquivalentTo(expectedToken);
        }

        [TestMethod]
        public async Task GetMeetingEvents_Success()
        {
            // Arrange
            var service = new MeetingsService(_httpClient);
            var start_date = "2020-10-11T00:00:00.000Z";
            var end_date = "2024-02-17T00:00:00.000Z";
            var expectedEvents = _fixture.Create<List<List<EventDetails>>>();

            _handlerMock.Response = new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonSerializer.Serialize(new EventGetResult { data = expectedEvents }))
                };

            // Act
            service._AuthResult = _fixture.Create<AuthResult>();
            var actualEvents = await service.GetMeetingEvents(start_date, end_date);

            // Assert using FluentAssertions
            actualEvents.Should().BeOfType<List<EventDetails>>();
        }

        [TestMethod]
        public async Task GetJWTToken_UnsuccessfulResponse()
        {
            // Arrange
            var service = new MeetingsService(_httpClient);
            var authRequest = _fixture.Create<AuthRequest>();

            _handlerMock
                .Response = new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.Unauthorized // Simulate an unauthorized response
                };

            // Act and Assert
            Func<Task> action = async () => await service.GetJWTToken(authRequest);
            await action.Should().ThrowAsync<HttpRequestException>();
        }

        [TestMethod]
        public async Task GetMeetingEvents_UnsuccessfulResponse()
        {
            // Arrange
            var service = new MeetingsService(_httpClient);
            var start_date = "2020-10-11T00:00:00.000Z";
            var end_date = "2024-02-17T00:00:00.000Z";

            _handlerMock
                .Response  = new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.InternalServerError // Simulate an internal server error
                };

            service._AuthResult = _fixture.Create<AuthResult>();

            // Act
            Func<Task> action = async () => await service.GetMeetingEvents(start_date, end_date);
            await action.Should().ThrowAsync<HttpRequestException>();
        }

        [TestMethod]
        public async Task GetMeetingEvents_NullResponseContent()
        {
            // Arrange
            var service = new MeetingsService(_httpClient);
            var start_date = "2020-10-11T00:00:00.000Z";
            var end_date = "2024-02-17T00:00:00.000Z";

            _handlerMock
                .Response = new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = null // Simulate null response content
                };

            service._AuthResult = _fixture.Create<AuthResult>();
            // Act
            Func<Task> action = async () => await service.GetMeetingEvents(start_date, end_date);
            await action.Should().ThrowAsync<JsonException>();
        }

        [TestMethod]
        public async Task GetMeetingEvents_DeserializationError()
        {
            // Arrange
            var service = new MeetingsService(_httpClient);
            var start_date = "2020-10-11T00:00:00.000Z";
            var end_date = "2024-02-17T00:00:00.000Z";
            

            _handlerMock.Response = new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("Invalid JSON response") // Simulate invalid JSON response content
                };

            service._AuthResult = _fixture.Create<AuthResult>();

            // Act and Assert
            Func<Task> action = async () => await service.GetMeetingEvents(start_date, end_date);
            await action.Should().ThrowAsync<JsonException>();
        }

        public class TestHttpMessageHandler : HttpMessageHandler
        {
            public HttpResponseMessage Response { get; set; } = new HttpResponseMessage(HttpStatusCode.OK);


            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
            {
                return await Task.FromResult(Response);
            }
        }

    }
}