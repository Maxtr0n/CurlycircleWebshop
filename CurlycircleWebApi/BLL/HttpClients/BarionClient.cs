using BLL.Interfaces;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.HttpClients
{
    public class BarionClient : IBarionClient, IDisposable
    {
        private readonly RestClient _client;
        private readonly string _url;
        private readonly string _secretKey;


        public BarionClient(IConfiguration configuration)
        {
            _url = configuration["Barion:BaseUrl"];
            _secretKey = configuration["Barion:SecretKey"];
            var options = new RestClientOptions(_url);

            _client = new RestClient(options);
        }

        public async Task<string> StartPayment(string paymentStartJson)
        {

            var response = await _client.PostJsonAsync<string, string>("/v2/Payment/Start", "asd");
            return response!;
        }


        public void Dispose()
        {
            _client?.Dispose();
            GC.SuppressFinalize(this);
        }

    }
}
