using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Codecool.CodecoolShop.Models;
using Codecool.CodecoolShop.Managers;

namespace Codecool.CodecoolShop.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class SupplierApiController : ControllerBase
    {
        private readonly ProductDbManager _productDbManager;

        public SupplierApiController(ProductDbManager productDbManager)
        {

            _productDbManager = productDbManager;
        }



        [HttpGet("{id}")]
        public List<Product> GetProductBySupplier(int id)
        {
            var response = _productDbManager.GetProductsForSupplier(id);

            return new List<Product>(response);
        }

        [HttpGet]
        public List<Product> GetAllProducts()
        {
            var response = _productDbManager.GetAllProducts();

            return new List<Product>(response);
        }

        [HttpGet]
        public List<Supplier> GetProductSuppliers()
        {
            var response = _productDbManager.GetAllSupplier();

            return new List<Supplier>(response);
        }
    }
}
