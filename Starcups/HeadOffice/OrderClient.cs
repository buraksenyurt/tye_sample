using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace HeadOffice
{
    public class OrderClient
    {
        private readonly JsonSerializerOptions options = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        private readonly HttpClient client;

        public OrderClient(HttpClient client)
        {
            this.client = client;
        }

        public async Task<OrderData[]> GetOrdersAsync()
        {
            var responseMessage = await this.client.GetAsync("/order");
            var stream = await responseMessage.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<OrderData[]>(stream, options);
        }
    }
}