using System.Collections.Generic;
using AutoMapper;
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

        public override IEnumerable<UserModel> GetAll()
        {
            Mapper.Reset();
            Mapper.Initialize(c => c.CreateMap<User, UserModel>());
            return Mapper.Map<IEnumerable<User>, IEnumerable<UserModel>>(_userRepository.GetAll());
        }

        public override UserModel GetOne(int id)
        {
            Mapper.Reset();
            Mapper.Initialize(c => c.CreateMap<User, UserModel>());
            return Mapper.Map<User, UserModel>(_userRepository.GetOne(id));
        }

        public override UserModel Save(UserModel entity)
        {
            Mapper.Reset();
            Mapper.Initialize(c => c.CreateMap<UserModel, User>());
            var item = Mapper.Map<UserModel, User>(entity);

            Mapper.Reset();
            Mapper.Initialize(c => c.CreateMap<User, UserModel>());
            return Mapper.Map<User, UserModel>(_userRepository.Save(item));
        }

        public override void Delete(UserModel entity)
        {
            Mapper.Reset();
            Mapper.Initialize(c => c.CreateMap<UserModel, User>());
            _userRepository.Delete(Mapper.Map<UserModel, User>(entity));
        }

        public override void DeleteAll()
        {
            _userRepository.DeleteAll();
        }
    }
}