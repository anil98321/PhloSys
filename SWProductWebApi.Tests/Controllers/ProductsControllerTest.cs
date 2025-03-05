using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using SWProductWebApi.Controllers;
using SWProductWebApi.Helper;
using SWProductWebApi.Models;
using SWProductWebApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWProductWebApi.Tests.Controllers
{
    
    public class ProductServiceTest
    {
        private readonly ProductsController _controller;
        private readonly Mock<ILogger<ProductsController>> _mockLogger;
        private readonly Mock<IProductService> _mockProductService;
        public ProductServiceTest()
        {
            _mockLogger =  new Mock<ILogger<ProductsController>>();
            _mockProductService =  new Mock<IProductService>();
            _controller = new ProductsController(_mockLogger.Object, _mockProductService.Object);
        }

        [Fact]
        public void GetProducts_Test() {
            // Arragne
            var pQuery = new ProductQuery() {
                MinPrice = 10,
                MaxPrice = 20,
                Size = new List<string>() { "small", "medium", "large" },
                Highlight = new List<string>() { "red", "blue", "green" }
            };

            _mockProductService.Setup(e => e.GetFilteredProducts(pQuery))
                .Returns(new List<Product>()
                {
                    new Product()
                    {
                        Title = "A Red Trouser",
                        Description = "This trouser perfectly pairs with a green shirt",
                        Id = 1,
                        Price = 15,
                        Sizes = [ "small", "medium", "large" ]
                    },
                    new Product()
                    {
                        Title = "A Green Trouser",
                        Description = "This trouser perfectly pairs with a blue shirt.",
                        Id = 1,
                        Price = 15,
                        Sizes = [ "small" ]
                    }
                });

            // Act
            var result = _controller.GetProducts(pQuery);
            var resultType = result as OkObjectResult;
            var resultProducts = resultType.Value as List<Product>;
            // Assert
            Assert.NotNull(resultProducts);
            Assert.Equal(2, resultProducts.Count);
            Assert.True(resultProducts.Any());
        }
    }
}
