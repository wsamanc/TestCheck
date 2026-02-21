using RestSharp;

namespace TestCheck.Tests
{
    public class ApiFixture
    {
        public RestClient Client { get; }

        public ApiFixture()
        {
            Client = new RestClient("https://jsonplaceholder.typicode.com");
        }
    }
}