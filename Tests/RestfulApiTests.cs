using Xunit;
using Xunit.Abstractions;
using RestSharp;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Collections.Generic;

namespace TestCheck.Tests
{
    public class RestfulApiTests : IClassFixture<ApiFixture>
    {
        private readonly RestClient client;
        private readonly ITestOutputHelper output;
        private string objectId;

        public RestfulApiTests(ApiFixture fixture, ITestOutputHelper output)
        {
            this.client = fixture.Client;
            this.output = output;
        }

        private string CreateTestObject(
            string name,
            int year,
            int price,
            string cpuModel,
            string hardDiskSize)
        {
            var request = new RestRequest("objects", Method.Post);

            var body = new
            {
                name = name,
                data = new Dictionary<string, object>
                {
                    { "year", year },
                    { "price", price },
                    { "CPU model", cpuModel },
                    { "Hard disk size", hardDiskSize }
                }
            };

            request.AddJsonBody(body);

            var response = client.Execute(request);

            output.WriteLine("Create Response:");
            output.WriteLine(response.Content);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var json = JObject.Parse(response.Content);

            return json["id"].ToString();
        }

        [Fact]
        public void GetAllObjects_ShouldReturn200()
        {
            var request = new RestRequest("posts", Method.Get);

            var response = client.Execute(request);

            output.WriteLine(response.Content);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact(Skip = "Skipped due to API rate limit")]
        public void CreateObject_ShouldReturn200()
        {
            objectId = CreateTestObject(
                "Apple MacBook",
                2026,
                180000,
                "Intel Core i7",
                "256 GB");

            Assert.False(string.IsNullOrEmpty(objectId));
        }

        [Fact]
        public void GetObjectById_ShouldReturn200()
        {
            var existingId = "1"; // use existing public ID

            var request = new RestRequest($"posts/{existingId}", Method.Get);

            var response = client.Execute(request);

            output.WriteLine(response.Content);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var json = JObject.Parse(response.Content);

            Assert.Equal(existingId, json["id"].ToString());
        }
    }
}