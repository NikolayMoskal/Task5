using System.Collections.Generic;
using AutoMapper;
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

        public override IEnumerable<RoleModel> GetAll()
        {
            Mapper.Reset();
            Mapper.Initialize(c => c.CreateMap<Role, RoleModel>());
            return Mapper.Map<IEnumerable<Role>, IEnumerable<RoleModel>>(_roleRepository.GetAll());
        }

        public override RoleModel GetOne(int id)
        {
            Mapper.Reset();
            Mapper.Initialize(c => c.CreateMap<Role, RoleModel>());
            return Mapper.Map<Role, RoleModel>(_roleRepository.GetOne(id));
        }

        public override RoleModel Save(RoleModel entity)
        {
            Mapper.Reset();
            Mapper.Initialize(c => c.CreateMap<RoleModel, Role>());
            var item = Mapper.Map<RoleModel, Role>(entity);

            Mapper.Reset();
            Mapper.Initialize(c => c.CreateMap<Role, RoleModel>());
            return Mapper.Map<Role, RoleModel>(_roleRepository.Save(item));
        }

        public override void Delete(RoleModel entity)
        {
            Mapper.Reset();
            Mapper.Initialize(c => c.CreateMap<RoleModel, Role>());
            _roleRepository.Delete(Mapper.Map<RoleModel, Role>(entity));
        }

        public override void DeleteAll()
        {
            _roleRepository.DeleteAll();
        }
    }
}