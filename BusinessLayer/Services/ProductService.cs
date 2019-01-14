using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BusinessLayer.Filters;
using BusinessLayer.Models;
using BusinessLayer.UnitsOfWork;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;

namespace BusinessLayer.Services
{
    public class ProductService : Service<ProductModel>
    {
        private readonly ProductRepository _repository;

        public ProductService(UnitOfWork unitOfWork) : base(unitOfWork)
        {
            _repository = UnitOfWork.ProductRepository;
        }

        public override IEnumerable<ProductModel> GetAll(Filter filter = null)
        {
            if (filter == null) filter = new Filter();

            var mapper = new MapperConfiguration(c => c.CreateMap<Product, ProductModel>()).CreateMapper();
            return mapper.Map<IEnumerable<Product>, IEnumerable<ProductModel>>(_repository.GetAll())
                .Where(filter.IsValid).ToList();
        }

        public override ProductModel GetOne(int id)
        {
            var mapper = new MapperConfiguration(c => c.CreateMap<Product, ProductModel>()).CreateMapper();
            return mapper.Map<Product, ProductModel>(_repository.GetOne(id));
        }

        public override ProductModel Save(ProductModel entity)
        {
            var mapper = new MapperConfiguration(c => c.CreateMap<ProductModel, Product>()).CreateMapper();
            var item = mapper.Map<ProductModel, Product>(entity);

            mapper = new MapperConfiguration(c => c.CreateMap<Product, ProductModel>()).CreateMapper();
            return mapper.Map<Product, ProductModel>(_repository.Save(item));
        }

        public override bool Delete(ProductModel entity)
        {
            var mapper = new MapperConfiguration(c => c.CreateMap<ProductModel, Product>()).CreateMapper();
            return _repository.Delete(mapper.Map<ProductModel, Product>(entity));
        }

        public override bool DeleteAll()
        {
            return _repository.DeleteAll();
        }
    }
}