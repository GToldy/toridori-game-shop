using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Codecool.CodecoolShop.Models;
using Codecool.CodecoolShop.Managers;


namespace Codecool.CodecoolShop.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class CategoryApiController : ControllerBase
    {
        private readonly ProductDbManager _productDbManager;

        public CategoryApiController(ProductDbManager productDbManager)
        {
            _productDbManager = productDbManager;
        }

        

        [HttpGet("{id}")]
        public List<Product> GetProducts(int id)
        {
            var response = _productDbManager.GetAllProductsForCategory(id);
            
            return new List<Product>(response) ;
        }

        [HttpGet]
        public List<Product> GetAllProducts()
        {
            var response = _productDbManager.GetAllProducts();

            return new List<Product>(response);
        }

        [HttpGet]
        public List<ProductCategory> GetProductCategories()
        {
            var response = _productDbManager.GetAllProductCategory();

            return new List<ProductCategory>(response);
        }
    }
}
