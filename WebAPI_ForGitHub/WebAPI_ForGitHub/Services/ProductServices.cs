using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAPI_ForGitHub.DataAccessLayer;
using WebAPI_ForGitHub.Models;

namespace WebAPI_ForGitHub.Services
{
    public class ProductServices : IProductServices
    {
        private IProductsDAL _dalObj;
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public ProductServices(IProductsDAL objProductDAL)
        {
            _dalObj = objProductDAL;
        }

        public List<Product> LoadProducts(string where)
        {
            List<Product> products = null;
            _dalObj = new ProductsDAL();
            products = _dalObj.LoadProducts(where);

            return products;
        }
        public Product GetProductById(Guid id)
        {
            Product product = null;
            _dalObj = new ProductsDAL();
            product = _dalObj.GetProductById(id);

            return product;

        }
        public bool UpdateProduct(Guid id, Product product)
        {
            _dalObj = new ProductsDAL();
            if (!_dalObj.UpdateProduct(id, product))
                return false;
            return true;
        }

        public bool Save(Product product)
        {
            Product pd = new Product();
            pd.Id = Guid.NewGuid();
            pd.Name = product.Name;
            pd.Description = product.Description;
            pd.Price = product.Price;
            pd.DeliveryPrice = product.DeliveryPrice;
            _dalObj = new ProductsDAL();
            if (!_dalObj.Save(pd))
                return false;
            return true;

        }

        public bool Delete(Guid id)
        {
            // Enhance this method to remove product options first before deleting the product itself.
            _dalObj = new ProductsDAL();
            if (!_dalObj.Delete(id))
                return false;
            return true;
        }

        public List<ProductOption> LoadProductOptions(Guid productId)
        {
            List<ProductOption> li = new List<ProductOption>();
            _dalObj = new ProductsDAL();
            li = _dalObj.LoadProductOptions(productId);
            return li;

        }

        public ProductOption GetProductOption(Guid productId, Guid id)
        {
            ProductOption prodOpt = null;
            _dalObj = new ProductsDAL();
            prodOpt = _dalObj.GetProductOption(productId, id);

            return prodOpt;
        }

        public bool Save(Guid productId, ProductOption option)
        {
            ProductOption prodOpt = new ProductOption
            {
                Id = Guid.NewGuid(),
                ProductId = productId,
                Name = option.Name,
                Description = option.Description
            };
            _dalObj = new ProductsDAL();
            if (!_dalObj.Save(productId, prodOpt))
                return false;
            return true;
        }

        public bool UpdateOption(Guid id, ProductOption option)
        {
            _dalObj = new ProductsDAL();
            if (!_dalObj.UpdateOption(id, option))
                return false;
            return true;
        }

        public bool DeleteOpt(Guid id)
        {
            _dalObj = new ProductsDAL();
            if (!_dalObj.DeleteOpt(id))
                return false;
            return true;
        }
    }
}