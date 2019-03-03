using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI_ForGitHub.Models;
using WebAPI_ForGitHub.Services;

namespace WebAPI_ForGitHub.Controllers
{
    [RoutePrefix("products")]
    public class ProductsController : ApiController
    {
        private readonly IProductServices _productServices;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        
        public ProductsController(IProductServices productServices)
        {
            _productServices = productServices;
        }
        [Route]
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            _logger.Info("ProductController: GetAll" + Environment.NewLine + DateTime.Now);
            var products = _productServices.LoadProducts(null);
            if (products == null)
                return NotFound();
            return Ok(products);

        }
        [Route]
        [HttpGet]
        public IHttpActionResult GetByName(string name)
        {
            _logger.Info("ProductController: Search by name:{0}", name);
            var products = _productServices.LoadProducts(name);
            if (products.Count == 0)
                return NotFound();
            return Ok(products);
        }
        [Route("{id}")]
        [HttpGet]
        public IHttpActionResult GetProduct(Guid id)
        {
            _logger.Info("ProductController: Search by Id:{0}", id + Environment.NewLine + DateTime.Now);
            var product = _productServices.GetProductById(id);
            if (product == null)
                return BadRequest("Product not found");
            return Ok(product);

        }
        [Route]
        [HttpPost]
        public IHttpActionResult Create(Product product)
        {
            _logger.Info("ProductController: Create" + Environment.NewLine + DateTime.Now);
            if (!_productServices.Save(product))
                return BadRequest("Bad Request");
            return Ok("Product created");
        }

        [Route("{id}")]
        [HttpPut]
        public IHttpActionResult Update(Guid id, Product product)
        {
            _logger.Info("ProductController: Update" + Environment.NewLine + DateTime.Now);
            if (!_productServices.UpdateProduct(id, product))
                return BadRequest();
            return Ok("Product updated");

        }
        [Route("{id}")]
        [HttpDelete]
        public IHttpActionResult Delete(Guid id)
        {
            _logger.Info("ProductController: Delete" + Environment.NewLine + DateTime.Now);
            if (!_productServices.Delete(id))
                return Content(HttpStatusCode.NotFound, "Product not found");
            return Content(HttpStatusCode.OK, "Product deleted");
        }

        [Route("{productId}/options")]
        [HttpGet]
        public IHttpActionResult GetOptions(Guid productId)
        {
            _logger.Info("ProductController: GetOptions " + Environment.NewLine + DateTime.Now);
            var productOptions = _productServices.LoadProductOptions(productId);
            if (productOptions.Count == 0)
                return Content(HttpStatusCode.NotFound, "Product options not found");
            return Ok(productOptions);
        }

        [Route("{productId}/options/{id}")]
        [HttpGet]
        public IHttpActionResult GetOption(Guid productId, Guid id)
        {
            _logger.Info("ProductController: GetOption" + Environment.NewLine + DateTime.Now);
            var option = _productServices.GetProductOption(productId, id);
            if (option == null)
                return Content(HttpStatusCode.NotFound, "Product option not found");
            return Ok(option);
        }

        [Route("{productId}/options")]
        [HttpPost]
        public IHttpActionResult CreateOption(Guid productId, ProductOption option)
        {
            _logger.Info("ProductController: CreateOption" + Environment.NewLine + DateTime.Now);
            option.ProductId = productId;
            if (!_productServices.Save(productId, option))
                return BadRequest("Bad Request");
            return Created("Option created", option);
        }

        [Route("{productId}/options/{id}")]
        [HttpPut]
        public IHttpActionResult UpdateOption(Guid id, ProductOption option)
        {
            _logger.Info("ProductController: UpdateOption " + Environment.NewLine + DateTime.Now);
            if (!_productServices.UpdateOption(id, option))
                return Content(HttpStatusCode.NotFound, "Product not found");
            return Content(HttpStatusCode.Accepted, option);
        }

        [Route("{productId}/options/{id}")]
        [HttpDelete]
        public IHttpActionResult DeleteOption(Guid id)
        {
            _logger.Info("ProductController: DeleteOption" + Environment.NewLine + DateTime.Now);
            if (!_productServices.DeleteOpt(id))
                return Content(HttpStatusCode.NotFound, "Product option not found");
            return Content(HttpStatusCode.OK, "Product option deleted successfully");
        }
    }
}
