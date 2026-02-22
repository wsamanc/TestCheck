using RestSharp;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;

namespace TestCheck.Helpers
{
    public class ObjectHelper
    {
        private readonly RestClient client;

        public ObjectHelper(RestClient client)
        {
            this.client = client;
        }

        public (string id, string name, int year, double price, string cpu, string disk)
        CreateObjectWithData()
        {
            var name = RandomDataHelper.GenerateName();
            var year = RandomDataHelper.GenerateYear();
            var price = RandomDataHelper.GeneratePrice();
            var cpu = RandomDataHelper.GenerateCpuModel();
            var disk = RandomDataHelper.GenerateDiskSize();

            var request = new RestRequest("objects", Method.Post);

            var body = new
            {
                name = name,
                data = new Dictionary<string, object>
        {
            { "year", year },
            { "price", price },
            { "CPU model", cpu },
            { "Hard disk size", disk }
        }
            };

            request.AddJsonBody(body);

            var response = client.Execute(request);

            if (!response.IsSuccessful)
                throw new Exception("Create failed");

            var json = JObject.Parse(response.Content);

            var id = json["id"].ToString();

            return (id, name, year, price, cpu, disk);
        }

        public RestResponse GetAllObjects()
        {
            var request = new RestRequest("objects", Method.Get);
            return client.Execute(request);
        }

        public RestResponse GetObjectById(string id)
        {
            var request = new RestRequest($"objects/{id}", Method.Get);
            return client.Execute(request);
        }

        public RestResponse UpdateObject(string id)
        {
            var request = new RestRequest($"objects/{id}", Method.Put);

            var requestBody = new
            {
                name = RandomDataHelper.GenerateName(),
                data = new Dictionary<string, object>
        {
            { "year", RandomDataHelper.GenerateYear() },
            { "price", RandomDataHelper.GeneratePrice() },
            { "CPU model", RandomDataHelper.GenerateCpuModel() },
            { "Hard disk size", RandomDataHelper.GenerateDiskSize() },
            { "color", RandomDataHelper.GenerateColor() }
        }
            };

            request.AddJsonBody(requestBody);

            return client.Execute(request);
        }
        public RestResponse DeleteObject(string id)
        {
            var request = new RestRequest($"objects/{id}", Method.Delete);
            var response = client.Execute(request);

            if (response == null)
                throw new Exception("Delete response is null");

            if (!response.IsSuccessful)
                throw new Exception($"Delete failed. Status: {response.StatusCode}, Body: {response.Content}");

            return response;
        }
    }
}