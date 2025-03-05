using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SWProductWebApi.Helper;
using SWProductWebApi.Services;

namespace SWProductWebApi.Controllers
{
    /// <summary>
    /// ProductsController
    /// </summary>
    [Route("api/[controller]/filter")]
    [ApiController]    
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductsController> _logger;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="productService"></param>
        public ProductsController(ILogger<ProductsController> logger,IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        /// <summary>
        /// GetProducts Action
        /// </summary>
        /// <param name="productQuery"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetProducts([FromQuery] ProductQuery productQuery)
        {
            _logger.LogInformation("Start Executing ProductsController.GetProducts Method");
            var products = _productService.GetFilteredProducts(productQuery);
            _logger.LogInformation("End Executing ProductsController.GetProducts Method");
            return Ok(products);            
        }
    }
}
