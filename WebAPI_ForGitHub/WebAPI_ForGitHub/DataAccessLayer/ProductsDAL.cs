using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAPI_ForGitHub.Models;
using NLog;
    
namespace WebAPI_ForGitHub.DataAccessLayer
{
    public class ProductsDAL:IProductsDAL, IDisposable
    {
        private DatabaseEntities _db;
        //private DatabaseEntities db = new DatabaseEntities();
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public List<Product> LoadProducts(string where)
        {
            List<Product> products = null;
            try
            {

                using (_db = new DatabaseEntities())
                {
                    if (where != null)
                    {
                        products = _db.Products.Where(p => p.Name.Contains(where)).ToList<Product>();
                    }
                    else
                        products = _db.Products.ToList<Product>();

                }
            }
            catch (Exception e)
            {
                Logger.Error(e.Message, e.StackTrace);
            }
            return products;
        }

        public Product GetProductById(Guid id)
        {
            Product product = null;
            try
            {
                using (_db = new DatabaseEntities())
                {
                    product = (from p in _db.Products
                               where p.Id == id
                               select p).FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                Logger.Error(e.Message, e.StackTrace);
            }
            return product;
        }
        public bool UpdateProduct(Guid id, Product product)
        {
            Product pd = new Product();
            try
            {
                using (_db = new DatabaseEntities())
                {
                    pd = _db.Products.FirstOrDefault(p => p.Id == id);
                    if (pd == null)
                        return false;
                    else
                    {
                        pd.Id = id;
                        pd.Name = product.Name;
                        pd.Price = product.Price;
                        pd.Description = product.Description;
                        pd.DeliveryPrice = product.DeliveryPrice;
                    }

                    if (_db.SaveChanges() == 1)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception e)
            {
                Logger.Error("Exception occured", e.StackTrace);
                return false;
            }
        }
        public bool Save(Product product)
        {
            using (_db = new DatabaseEntities())
            {
                _db.Products.Add(product);
                if (_db.SaveChanges() == 1)
                    return true;
                else
                    return false;
            }
        }

        public bool Delete(Guid id)
        {
            using (_db = new DatabaseEntities())
            {
                var productToRemove = _db.Products.First(p => p.Id == id);
                if (productToRemove != null)
                {
                    _db.Products.Remove(productToRemove);
                    if (_db.SaveChanges() == 1)
                        return true;
                    else
                        return false;
                }
                else
                    return false;
            }
        }

        public List<ProductOption> LoadProductOptions(Guid productId)
        {
            List<ProductOption> li = null;
            try
            {
                using (_db = new DatabaseEntities())
                {
                    li = _db.ProductOptions.Where(p => p.ProductId == productId).ToList<ProductOption>();
                }
            }
            catch (Exception e)
            {
                Logger.Error("Exception occured", e.StackTrace);
            }
            return li;
        }
        public ProductOption GetProductOption(Guid productId, Guid id)
        {
            ProductOption prodOpt = null;
            try
            {
                using (_db = new DatabaseEntities())
                {
                    prodOpt = _db.ProductOptions.FirstOrDefault(p => p.ProductId == productId && p.Id == id);
                }
            }
            catch (Exception e)
            {
                Logger.Error("Exception occured", e.StackTrace);
            }
            return prodOpt;
        }
        public bool Save(Guid productId, ProductOption option)
        {
            try
            {
                using (_db = new DatabaseEntities())
                {
                    var product = _db.Products.FirstOrDefault(p => p.Id == productId);
                    if (product == null)
                        return false;
                    else
                    {
                        _db.ProductOptions.Add(option);
                        if (_db.SaveChanges() == 1)
                            return true;
                        else
                            return false;
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Error("Exception occured", e.StackTrace);
                return false;
            }

        }
        public bool UpdateOption(Guid id, ProductOption option)
        {
            try
            {
                using (_db = new DatabaseEntities())
                {
                    var prodOpt = _db.ProductOptions.FirstOrDefault(p => p.Id == id);
                    if (prodOpt != null)
                    {
                        prodOpt.Name = option.Name;
                        prodOpt.Description = option.Description;
                        if (_db.SaveChanges() == 1)
                            return true;
                        else
                            return false;
                    }
                    return false;
                }
            }
            catch (Exception e)
            {
                Logger.Error("Exception occured ", e.StackTrace);
                return false;
            }
        }

        public bool DeleteOpt(Guid id)
        {
            try
            {
                using (_db = new DatabaseEntities())
                {
                    var prodOptToRemove = _db.ProductOptions.First(p => p.Id == id);
                    if (prodOptToRemove != null)
                    {
                        _db.ProductOptions.Remove(prodOptToRemove);
                        if (_db.SaveChanges() == 1)
                            return true;
                        else
                            return false;
                    }
                    else
                        return false;
                }
            }
            catch (Exception e)
            {
                Logger.Error("Exception occured ", e.StackTrace);
                return false;
            }
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_db != null)
                {
                    _db.Dispose();
                    _db = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}