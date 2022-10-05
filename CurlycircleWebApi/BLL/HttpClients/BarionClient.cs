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

        public BarionClient(IConfiguration configuration)
        {
            _url = configuration["Barion:BaseUrl"];
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
            var request = new RestRequest("/v2/Payment/GetPaymentState")
                .AddJsonBody(getPaymentStateRequest);
            var response = await _client.ExecuteGetAsync<GetPaymentStateResponse>(request);
            return response.Data!;
        }

        public void Dispose()
        {
            _client?.Dispose();
            GC.SuppressFinalize(this);
        }

    }
}
