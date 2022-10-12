using BLL.Exceptions;
using BLL.Interfaces;
using Domain.QueryParameters.Barion;
using Microsoft.Extensions.Configuration;
using RestSharp;
using RestSharp.Serializers.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
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
            var request = new RestRequest("/v2/Payment/Start", Method.Post)
                .AddJsonBody(startPaymentRequest);

            var response = await _client.ExecuteAsync<StartPaymentResponse>(request);

            if (!response.IsSuccessful)
            {
                throw new WebPaymentException("Web payment attempt failed.", new[]
                {
                    response.ErrorMessage ?? "Barion API error"
                });
            }

            return response.Data!;
        }

        public async Task<GetPaymentStateResponse> GetPaymentState(GetPaymentStateRequest getPaymentStateRequest)
        {
            var request = new RestRequest("/v2/Payment/GetPaymentState", Method.Get)
                .AddJsonBody(getPaymentStateRequest);
            var response = await _client.ExecuteAsync<GetPaymentStateResponse>(request);

            if (!response.IsSuccessful)
            {
                throw new WebPaymentException("Web payment attempt failed.", new[]
                {
                    response.ErrorMessage ?? "Barion API error"
                });
            }

            return response.Data!;
        }

        public void Dispose()
        {
            _client?.Dispose();
            GC.SuppressFinalize(this);
        }

    }
}
