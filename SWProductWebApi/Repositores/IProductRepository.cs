using SWProductWebApi.Models;

namespace SWProductWebApi.Repositores
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetProducts();
    }
}
