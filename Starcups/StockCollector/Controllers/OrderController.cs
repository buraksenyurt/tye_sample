using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace StockCollector.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private static readonly string[] ShopNames = new[]
        {
            "Capitol", "Balat", "Taksim Meydan", "Pendik Marina", "Bebek", "Koşuyolu", "Bakırköy", "Moda", "Beşiktaş Arena", "Maslak 1881"
        };
        private static readonly string[] Items = new[]
        {
            "Peçete (100 * Adet)", "Karıştırma Kaşığı (100 * Adet)", "Şeker (Kilo)","Short Bardak (100 * Adet)"
        };

        private readonly ILogger<OrderController> _logger;

        public OrderController(ILogger<OrderController> logger)
        {
            _logger = logger;
        }

        //[HttpGet]
        //public IEnumerable<OrderData> Get()
        //{
        //    var rng = new Random();
        //    return Enumerable.Range(1, 10).Select(index => new OrderData
        //    {
        //        ItemName = Items[rng.Next(Items.Length)],
        //        Quantity = rng.Next(1, 10),
        //        ShopName = ShopNames[rng.Next(ShopNames.Length)],
        //        Time = DateTime.Now
        //    }).ToArray();
        //}

        [HttpGet]
        public async Task<string> Get([FromServices] IDistributedCache cache)
        {
            var keyOrder = await cache.GetStringAsync("keyOrder");
            if (keyOrder == null)
            {
                _logger.LogInformation("Redis Key boştu");
                var rng = new Random();
                var orders = Enumerable.Range(1, 10).Select(index => new OrderData
                {
                    ItemName = Items[rng.Next(Items.Length)],
                    Quantity = rng.Next(1, 10),
                    ShopName = ShopNames[rng.Next(ShopNames.Length)],
                    Time = DateTime.Now
                }).ToArray();

                keyOrder = JsonSerializer.Serialize(orders);
                _logger.LogInformation($"Veri serileştirildi {keyOrder}");

                await cache.SetStringAsync("keyOrder", keyOrder, new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(10)
                });
            }
            return keyOrder;
        }
    }
}
