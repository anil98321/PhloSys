using SWProductWebApi.Helper;
using SWProductWebApi.Models;

namespace SWProductWebApi.Services
{
    /// <summary>
    /// IProductService Interface
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// GetFilteredProducts
        /// </summary>
        /// <param name="productQuery"></param>
        /// <returns></returns>
        IEnumerable<Product> GetFilteredProducts(ProductQuery productQuery);
    }
}
