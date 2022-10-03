using BLL.Interfaces;
using Domain.QueryParameters.Barion;
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

        public async Task<StartPaymentResponse> StartPayment(StartPaymentRequest startPaymentRequest)
        {

            var response = await _client.PostJsonAsync<StartPaymentRequest, StartPaymentResponse>("/v2/Payment/Start", startPaymentRequest);
            return response!;
        }

        public async Task<GetPaymentStateResponse> GetPaymentState(GetPaymentStateRequest getPaymentStateRequest)
        {
            var response = await _client.PostJsonAsync<GetPaymentStateRequest, GetPaymentStateResponse>("/v2/Payment/GetPaymentState", getPaymentStateRequest);
            return response!;
        }


        public void Dispose()
        {
            _client?.Dispose();
            GC.SuppressFinalize(this);
        }

    }
}
