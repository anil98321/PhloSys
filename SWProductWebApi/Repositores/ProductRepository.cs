using SWProductWebApi.Controllers;
using SWProductWebApi.Models;
using System.Text.Json;

namespace SWProductWebApi.Repositores
{
    /// <summary>
    /// ProductRepository
    /// </summary>
    public class ProductRepository : IProductRepository
    {
        private readonly List<Product> _products;
        private readonly ILogger<ProductRepository> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger"></param>
        public ProductRepository(ILogger<ProductRepository> logger)
        {
            _logger = logger;
            string jsonData = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"\data\products.json");         
            _products = JsonSerializer.Deserialize<List<Product>>(jsonData, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true});
        }

        /// <summary>
        /// GetProducts
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Product> GetProducts()
        {
            _logger.LogInformation("Start Executing ProductRepository.GetProducts Method");
            return _products;
        }
    }
}
