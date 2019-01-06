using System.Collections.Generic;
using AutoMapper;
using BusinessLayer.Models;
using BusinessLayer.UnitsOfWork;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;

namespace BusinessLayer.Services
{
    public class EmployeeService : Service<EmployeeModel>
    {
        private readonly EmployeeRepository _repository;

        public EmployeeService(UnitOfWork unitOfWork) : base(unitOfWork)
        {
            _repository = UnitOfWork.EmployeeRepository;
        }

        public override IEnumerable<EmployeeModel> GetAll()
        {
            Mapper.Initialize(c => c.CreateMap<Employee, EmployeeModel>());
            return Mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeModel>>(_repository.GetAll());
        }

        public override EmployeeModel GetOne(int id)
        {
            Mapper.Initialize(c => c.CreateMap<Employee, EmployeeModel>());
            return Mapper.Map<Employee, EmployeeModel>(_repository.GetOne(id));
        }

        public override void Save(EmployeeModel entity)
        {
            Mapper.Initialize(c => c.CreateMap<EmployeeModel, Employee>());
            _repository.Save(Mapper.Map<EmployeeModel, Employee>(entity));
        }

        public override void Delete(EmployeeModel entity)
        {
            Mapper.Initialize(c => c.CreateMap<EmployeeModel, Employee>());
            _repository.Delete(Mapper.Map<EmployeeModel, Employee>(entity));
        }

        public override void DeleteAll()
        {
            _repository.DeleteAll();
        }
    }
}