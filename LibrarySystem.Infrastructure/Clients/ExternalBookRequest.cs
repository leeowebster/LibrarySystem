using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using LibrarySystem.Domain.Models;

namespace LibrarySystem.Infrastructure.Clients
{
    public class ExternalBookRequest : IExternalBookService
    {
        private readonly HttpClient _HttpClient;

        public ExternalBookRequest(HttpClient httpClient)
        {
            _HttpClient = httpClient;
        }

        //public async Task<ExternalApiResponseDTO> GetBookData(string isbn)
        //{
        //    var response = await _HttpClient.GetAsync($"books/{isbn}.json");

        //    response.EnsureSuccessStatusCode();

        //    response.Content.ReadAsStream();


        //    var internalDto = new ExternalApiResponseDTO()
        //    {
        //        Title = response.title,

        //    }

        //    return response.Content.ReadFromJsonAsync<ExternalApiResponse>().Result;

        //}

        public async Task<ExternalApiResponseDTO> GetBookDataAsync(string isbn)
        {
            // 1. Faz a requisição e garante o status de sucesso (2xx)
            var response = await _HttpClient.GetAsync($"books/{isbn}.json");
            response.EnsureSuccessStatusCode();

            // 2. A Chave: Deserializa o JSON para o DTO usando o método de extensão
            // O .NET faz o trabalho de mapear o JSON para a estrutura de classes C#
            return await response.Content.ReadFromJsonAsync<ExternalApiResponseDTO>();
        }

    }
}
