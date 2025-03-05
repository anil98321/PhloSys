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

namespace SWProductWebApi.Tests.Repositories
{
    
    public class ProductRepositoryTest
    {
        private readonly ProductRepository _repository;
        private readonly Mock<ILogger<ProductRepository>> _mockLogger;
        private readonly Mock<IProductService> _mockProductService;
        public ProductRepositoryTest()
        {
            _mockLogger =  new Mock<ILogger<ProductRepository>>();
            _mockProductService =  new Mock<IProductService>();
            _repository = new ProductRepository(_mockLogger.Object);
        }

        [Fact]
        public void GetProducts_Test() {
            // Act
            var resultProducts = _repository.GetProducts();

            // Assert
            Assert.NotNull(resultProducts);
            Assert.Equal(48, resultProducts.Count());
            Assert.True(resultProducts.Any());
        }
    }
}
