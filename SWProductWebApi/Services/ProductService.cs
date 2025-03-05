using SWProductWebApi.Controllers;
using SWProductWebApi.Helper;
using SWProductWebApi.Models;
using SWProductWebApi.Repositores;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace SWProductWebApi.Services
{
    /// <summary>
    /// ProductService
    /// </summary>
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductService> _logger;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="productRepository"></param>
        public ProductService(ILogger<ProductService> logger, IProductRepository productRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
        }
        /// <summary>
        /// GetFilteredProducts
        /// </summary>
        /// <param name="productQuery"></param>
        /// <returns></returns>
        public IEnumerable<Product> GetFilteredProducts(ProductQuery productQuery)
        {
            _logger.LogInformation("Start Executing ProductService.GetFilteredProducts Method");

            var products = _productRepository.GetProducts();

            if (productQuery != null && productQuery?.MinPrice != null)
            {
                products = products.Where(p => p.Price >= productQuery.MinPrice);
            }

            if (productQuery != null && productQuery?.MaxPrice != null)
            {
                products = products.Where(p => p.Price <= productQuery.MaxPrice);
            }

            if (productQuery != null && productQuery?.Size != null && productQuery?.Size.Count > 0)
            {
                var productSize = new List<Product>() {};

                productQuery.Size.ForEach(i =>
                {
                    if (productSize != null && productSize.Count > 0) 
                    {
                        productSize = productSize.Union(products.Where(p => p.Sizes.Contains(i))).ToList();
                    }
                    else 
                    {
                        productSize.AddRange(products.Where(p => p.Sizes.Contains(i)).ToList());
                    }   
                });

                if (productSize != null && productSize.Count > 0)
                    products = productSize;
            }

            if (productQuery != null && productQuery?.Highlight != null && productQuery?.Highlight.Count > 0)
            {
                var productHighlight = new List<Product>() { };

                productQuery.Highlight.ForEach(i =>
                {
                    if (productHighlight != null && productHighlight.Count > 0)
                    {
                        productHighlight = productHighlight.Union(products.Where(p => p.Description.Contains(i))
                            .Select(s => new Product()
                            {
                                Price = s.Price,
                                Title = s.Title,
                                Sizes = s.Sizes,
                                Description = s.Description.Replace(i, $"<em>{i}</em>")
                            })).ToList();
                    }
                    else
                    {
                        productHighlight.AddRange(products.Where(p => p.Description.Contains(i))
                            .Select(s => new Product()
                            {
                                Price = s.Price,
                                Title = s.Title,
                                Sizes = s.Sizes,
                                Description = s.Description.Replace(i, $"<em>{i}</em>")
                            }).ToList());
                    }
                });

                if (productHighlight != null && productHighlight.Count > 0)
                    products = productHighlight;
            }
            _logger.LogInformation("End Executing ProductService.GetFilteredProducts Method");
            return products;
        }
    }
}
