using System.Collections.Generic;
using AutoMapper;
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

        public override IEnumerable<ProductModel> GetAll()
        {
            Mapper.Initialize(c => c.CreateMap<Product, ProductModel>());
            return Mapper.Map<IEnumerable<Product>, IEnumerable<ProductModel>>(_repository.GetAll());
        }

        public override ProductModel GetOne(int id)
        {
            Mapper.Initialize(c => c.CreateMap<Product, ProductModel>());
            return Mapper.Map<Product, ProductModel>(_repository.GetOne(id));
        }

        public override void Save(ProductModel entity)
        {
            Mapper.Initialize(c => c.CreateMap<ProductModel, Product>());
            _repository.Save(Mapper.Map<ProductModel, Product>(entity));
        }

        public override void Delete(ProductModel entity)
        {
            Mapper.Initialize(c => c.CreateMap<ProductModel, Product>());
            _repository.Delete(Mapper.Map<ProductModel, Product>(entity));
        }

        public override void DeleteAll()
        {
            _repository.DeleteAll();
        }
    }
}