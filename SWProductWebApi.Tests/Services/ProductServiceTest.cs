using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using SWProductWebApi.Controllers;
using SWProductWebApi.Helper;
using SWProductWebApi.Models;
using SWProductWebApi.Repositores;
using SWProductWebApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWProductWebApi.Tests.Services
{
    
    public class ProductServiceTest
    {
        private readonly ProductService _service;
        private readonly Mock<ILogger<ProductService>> _mockLogger;
        private readonly Mock<IProductRepository> _mockProductRepository;
        public ProductServiceTest()
        {
            _mockLogger =  new Mock<ILogger<ProductService>>();
            _mockProductRepository =  new Mock<IProductRepository>();
            _service = new ProductService(_mockLogger.Object, _mockProductRepository.Object);
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

            _mockProductRepository.Setup(e => e.GetProducts())
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
            var resultProducts = _service.GetFilteredProducts(pQuery);

            // Assert
            Assert.NotNull(resultProducts);
            Assert.Equal(2, resultProducts.Count());
            Assert.True(resultProducts.Any());
        }
    }
}
