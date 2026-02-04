using EmployeeDepartmentAndProjectManagement.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace EmployeeDepartmentAndProjectManagement.Services
{
    public class RandomStringGenerator : IRandomStringGenerator
    {
        private readonly HttpClient _httpClient;
        private readonly string _url;

        public RandomStringGenerator(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _url = configuration["ExternalServices:RandomStringGeneratorUrl"];
        }

        public async Task<string> GenerateAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync(_url);

                response.EnsureSuccessStatusCode();

                var randomString = await response.Content.ReadAsStringAsync();
                return randomString.Trim();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to generate project code from external service: {ex.Message}");
            }
        }
    }
}