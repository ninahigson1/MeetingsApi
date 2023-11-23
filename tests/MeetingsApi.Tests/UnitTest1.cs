using System.Net;
using System.Text.Json;
using FluentAssertions;
using meetingsApi;
using Microsoft.AspNetCore.Mvc.Testing;

namespace MeetingsApi.Tests
{
    public class MeetingsController_Tests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _applicationFactory;

        public MeetingsController_Tests(WebApplicationFactory<Program> applicationFactory)
        {
            _applicationFactory = applicationFactory;
        }

        [Fact]
        public async Task QuerysAllMeetingsByDate()
        {
            var client = _applicationFactory.CreateClient();
            var response = await client.GetAsync($"Meetings?meetingDate={DateTime.Now :yyyy-MM-dd}");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var content = await response.Content.ReadAsStringAsync();
            var meetings = JsonSerializer.Deserialize<List<Meeting>>(content);
            meetings.Should().NotBeNull();
            meetings!.Count.Should().Be(4);
        }
    }
}