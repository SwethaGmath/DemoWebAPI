using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI_ForGitHub.Models;

namespace WebAPI_ForGitHub.Services
{
    public interface IProductServices
    {
        List<Product> LoadProducts(string where);
        Product GetProductById(Guid id);
        bool UpdateProduct(Guid id, Product product);
        bool Save(Product product);
        bool Delete(Guid id);

        List<ProductOption> LoadProductOptions(Guid productId);
        ProductOption GetProductOption(Guid productId, Guid id);
        bool Save(Guid productId, ProductOption option);
        bool UpdateOption(Guid productId, ProductOption option);
        bool DeleteOpt(Guid id);
    }
}
