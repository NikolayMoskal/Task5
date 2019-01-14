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
    public class EmployeeService : Service<EmployeeModel>
    {
        private readonly EmployeeRepository _repository;

        public EmployeeService(UnitOfWork unitOfWork) : base(unitOfWork)
        {
            _repository = UnitOfWork.EmployeeRepository;
        }

        public override IEnumerable<EmployeeModel> GetAll(Filter filter = null)
        {
            if (filter == null) filter = new Filter();

            var mapper = new MapperConfiguration(c => c.CreateMap<Employee, EmployeeModel>()).CreateMapper();
            return mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeModel>>(_repository.GetAll())
                .Where(filter.IsValid).ToList();
        }

        public override EmployeeModel GetOne(int id)
        {
            var mapper = new MapperConfiguration(c => c.CreateMap<Employee, EmployeeModel>()).CreateMapper();
            return mapper.Map<Employee, EmployeeModel>(_repository.GetOne(id));
        }

        public override EmployeeModel Save(EmployeeModel entity)
        {
            var mapper = new MapperConfiguration(c => c.CreateMap<EmployeeModel, Employee>()).CreateMapper();
            var item = mapper.Map<EmployeeModel, Employee>(entity);

            mapper = new MapperConfiguration(c => c.CreateMap<Employee, EmployeeModel>()).CreateMapper();
            return mapper.Map<Employee, EmployeeModel>(_repository.Save(item));
        }

        public override bool Delete(EmployeeModel entity)
        {
            var mapper = new MapperConfiguration(c => c.CreateMap<EmployeeModel, Employee>()).CreateMapper();
            return _repository.Delete(mapper.Map<EmployeeModel, Employee>(entity));
        }

        public override bool DeleteAll()
        {
            return _repository.DeleteAll();
        }
    }
}