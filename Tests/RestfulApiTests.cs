using Xunit;
using Xunit.Abstractions;
using Newtonsoft.Json.Linq;
using System.Net;
using TestCheck.Helpers;

namespace TestCheck.Tests
{
    [TestCaseOrderer("TestCheck.Helpers.PriorityOrderer", "TestCheck")]
    public class RestfulApiTests : IClassFixture<ApiFixture>
    {
        private readonly ObjectHelper objectHelper;
        private readonly ITestOutputHelper output;

        // shared object data across tests
        private static string objectId;
        private static string expectedName;
        private static int expectedYear;
        private static double expectedPrice;
        private static string expectedCpu;
        private static string expectedDisk;

        public RestfulApiTests(ApiFixture fixture, ITestOutputHelper output)
        {
            objectHelper = new ObjectHelper(fixture.Client);
            this.output = output;
        }

        // 1. Create object FIRST
        [Fact(DisplayName = "Create object and validate response matches request")]
        [Priority(1)]
        public void CreateObject()
        {
            var result = objectHelper.CreateObjectWithData();

            objectId = result.id;
            expectedName = result.name;
            expectedYear = result.year;
            expectedPrice = result.price;
            expectedCpu = result.cpu;
            expectedDisk = result.disk;

            var response = objectHelper.GetObjectById(objectId);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Content);

            var json = JObject.Parse(response.Content);

            output.WriteLine("Created Object Response:");
            output.WriteLine(json.ToString());

            // Full validation
            Assert.Equal(objectId, json["id"]?.ToString());
            Assert.Equal(expectedName, json["name"]?.ToString());
            Assert.Equal(expectedYear, (int)json["data"]["year"]);
            Assert.Equal(expectedPrice, (double)json["data"]["price"]);
            Assert.Equal(expectedCpu, json["data"]["CPU model"]?.ToString());
            Assert.Equal(expectedDisk, json["data"]["Hard disk size"]?.ToString());
        }


        // 2. Get object by ID
        [Fact(DisplayName = "Get object by ID and validate full response")]
        [Priority(2)]
        public void GetObjectById()
        {
            EnsureObjectCreated();

            var response = objectHelper.GetObjectById(objectId);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Content);

            var json = JObject.Parse(response.Content);

            output.WriteLine("Get Object Response:");
            output.WriteLine(json.ToString());

            // Validate everything
            Assert.Equal(objectId, json["id"]?.ToString());
            Assert.Equal(expectedName, json["name"]?.ToString());
            Assert.Equal(expectedYear, (int)json["data"]["year"]);
            Assert.Equal(expectedPrice, (double)json["data"]["price"]);
            Assert.Equal(expectedCpu, json["data"]["CPU model"]?.ToString());
            Assert.Equal(expectedDisk, json["data"]["Hard disk size"]?.ToString());
        }


        // 3. Update object
        [Fact(DisplayName = "Update object and validate updated response")]
        [Priority(3)]
        public void UpdateObject()
        {
            EnsureObjectCreated();

            var response = objectHelper.UpdateObject(objectId);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Content);

            var json = JObject.Parse(response.Content);

            output.WriteLine("Update Response:");
            output.WriteLine(json.ToString());

            // Validate ID remains same
            Assert.Equal(objectId, json["id"]?.ToString());

            // Validate updated fields exist
            Assert.NotNull(json["name"]);
            Assert.NotNull(json["data"]["year"]);
            Assert.NotNull(json["data"]["price"]);
            Assert.NotNull(json["data"]["CPU model"]);
            Assert.NotNull(json["data"]["Hard disk size"]);
            Assert.NotNull(json["data"]["color"]);
        }


        // 4. Get all objects
        [Fact(DisplayName = "Get all objects and validate response")]
        [Priority(4)]
        public void GetAllObjects()
        {
            var response = objectHelper.GetAllObjects();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            Assert.NotNull(response.Content);

            var jsonArray = JArray.Parse(response.Content);

            output.WriteLine($"Total objects returned: {jsonArray.Count}");

            // Validate list is not empty
            Assert.True(jsonArray.Count > 0);

            // Validate object structure
            Assert.NotNull(jsonArray[0]["id"]);
            Assert.NotNull(jsonArray[0]["name"]);
            Assert.NotNull(jsonArray[0]["data"]);
        }


        // 5. Delete object LAST
        [Fact(DisplayName = "Delete object and verify deletion")]
        [Priority(5)]
        public void DeleteObject()
        {
            EnsureObjectCreated();

            var deleteResponse = objectHelper.DeleteObject(objectId);

            Assert.Equal(HttpStatusCode.OK, deleteResponse.StatusCode);

            output.WriteLine("Delete Response:");
            output.WriteLine(deleteResponse.Content);

            // Verify object no longer exists
            var getResponse = objectHelper.GetObjectById(objectId);

            Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);
        }


        private void EnsureObjectCreated()
        {
            if (string.IsNullOrEmpty(objectId))
            {
                var result = objectHelper.CreateObjectWithData();

                objectId = result.id;
                expectedName = result.name;
                expectedYear = result.year;
                expectedPrice = result.price;
                expectedCpu = result.cpu;
                expectedDisk = result.disk;
            }
        }
    }
}