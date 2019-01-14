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
    public class RoleService : Service<RoleModel>
    {
        private readonly RoleRepository _roleRepository;

        public RoleService(UnitOfWork unitOfWork) : base(unitOfWork)
        {
            _roleRepository = UnitOfWork.RoleRepository;
        }

        public override IEnumerable<RoleModel> GetAll(Filter filter = null)
        {
            if (filter == null) filter = new Filter();

            var mapper = new MapperConfiguration(c => c.CreateMap<Role, RoleModel>()).CreateMapper();
            return mapper.Map<IEnumerable<Role>, IEnumerable<RoleModel>>(_roleRepository.GetAll())
                .Where(filter.IsValid).ToList();
        }

        public override RoleModel GetOne(int id)
        {
            var mapper = new MapperConfiguration(c => c.CreateMap<Role, RoleModel>()).CreateMapper();
            return mapper.Map<Role, RoleModel>(_roleRepository.GetOne(id));
        }

        public override RoleModel Save(RoleModel entity)
        {
            var mapper = new MapperConfiguration(c => c.CreateMap<RoleModel, Role>()).CreateMapper();
            var item = mapper.Map<RoleModel, Role>(entity);

            mapper = new MapperConfiguration(c => c.CreateMap<Role, RoleModel>()).CreateMapper();
            return mapper.Map<Role, RoleModel>(_roleRepository.Save(item));
        }

        public override bool Delete(RoleModel entity)
        {
            var mapper = new MapperConfiguration(c => c.CreateMap<RoleModel, Role>()).CreateMapper();
            return _roleRepository.Delete(mapper.Map<RoleModel, Role>(entity));
        }

        public override bool DeleteAll()
        {
            return _roleRepository.DeleteAll();
        }
    }
}