using System.Collections.Generic;
using System.Web.Http;
using BusinessLayer.Filters;
using BusinessLayer.Models;
using BusinessLayer.Services;
using BusinessLayer.UnitsOfWork;

namespace Task5.Controllers
{
    public class ProductController : ApiController
    {
        private readonly ProductService _productService = new ProductService(new UnitOfWork());

        [HttpPost]
        public IEnumerable<ProductModel> All(ProductModel model)
        {
            return _productService.GetAll(new ProductFilter(model));
        }

        [HttpPost]
        public ProductModel Save(ProductModel model)
        {
            return _productService.Save(model);
        }

        [HttpDelete]
        public bool Delete(ProductModel model)
        {
            return _productService.Delete(model);
        }

        [HttpDelete]
        public bool DeleteAll()
        {
            return _productService.DeleteAll();
        }
    }
}