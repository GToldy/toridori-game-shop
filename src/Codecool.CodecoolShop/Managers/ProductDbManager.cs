using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Models;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Codecool.CodecoolShop.Managers
{
    public class ProductDbManager : BaseDbManager
    {
        private readonly IProductDbDao _productDbDao;
        private readonly IProductCategoryDbDao _productCategoryDbDao;
        private readonly ISupplierDbDao _supplierDbDao;

        public ProductDbManager(IProductDbDao productDbDao, IProductCategoryDbDao productCategoryDbDao, ISupplierDbDao supplierDbDao) : base()
        {
            _productDbDao = productDbDao;
            _productCategoryDbDao = productCategoryDbDao;
            _supplierDbDao = supplierDbDao;
        }

        public Product GetProductById(int productId)
        {
            return _productDbDao.Get(productId);
        }

        public Supplier GetSupplierById(int supplierId)
        {
            return _supplierDbDao.Get(supplierId);
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _productDbDao.GetAll();
        }

        public IEnumerable<Supplier> GetAllSupplier()
        {
            return _supplierDbDao.GetAll();
        }

        public IEnumerable<Product> GetProductsForSupplier(int id)
        {
            Supplier supplier = _supplierDbDao.Get(id);
            return _productDbDao.GetBySupplier(supplier);
        }

        public IEnumerable<Product> GetAllProductsForCategory(int id)
        {
            ProductCategory category = _productCategoryDbDao.Get(id);
            return _productDbDao.GetByProductCategory(category);
        }

        public IEnumerable<ProductCategory> GetAllProductCategory()
        {
            return _productCategoryDbDao.GetAll();
        }
    }
}
