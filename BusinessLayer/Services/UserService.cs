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
    public class UserService : Service<UserModel>
    {
        private readonly UserRepository _userRepository;

        public UserService(UnitOfWork unitOfWork) : base(unitOfWork)
        {
            _userRepository = UnitOfWork.UserRepository;
        }

        public override IEnumerable<UserModel> GetAll(Filter filter = null)
        {
            if (filter == null) filter = new Filter();

            var mapper = new MapperConfiguration(c => c.CreateMap<User, UserModel>()).CreateMapper();
            return mapper.Map<IEnumerable<User>, IEnumerable<UserModel>>(_userRepository.GetAll())
                .Where(filter.IsValid).ToList();
        }

        public override UserModel GetOne(int id)
        {
            var mapper = new MapperConfiguration(c => c.CreateMap<User, UserModel>()).CreateMapper();
            return mapper.Map<User, UserModel>(_userRepository.GetOne(id));
        }

        public override UserModel Save(UserModel entity)
        {
            var mapper = new MapperConfiguration(c => c.CreateMap<UserModel, User>()).CreateMapper();
            var item = mapper.Map<UserModel, User>(entity);

            mapper = new MapperConfiguration(c => c.CreateMap<User, UserModel>()).CreateMapper();
            return mapper.Map<User, UserModel>(_userRepository.Save(item));
        }

        public override bool Delete(UserModel entity)
        {
            var mapper = new MapperConfiguration(c => c.CreateMap<UserModel, User>()).CreateMapper();
            return _userRepository.Delete(mapper.Map<UserModel, User>(entity));
        }

        public override bool DeleteAll()
        {
            return _userRepository.DeleteAll();
        }
    }
}