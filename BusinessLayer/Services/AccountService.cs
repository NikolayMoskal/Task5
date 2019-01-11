using System.Collections.Generic;
using AutoMapper;
using BusinessLayer.Models;
using BusinessLayer.UnitsOfWork;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;

namespace BusinessLayer.Services
{
    public class AccountService : Service<AccountModel>
    {
        private readonly AccountRepository _accountRepository;

        public AccountService(UnitOfWork unitOfWork) : base(unitOfWork)
        {
            _accountRepository = UnitOfWork.AccountRepository;
        }

        public override IEnumerable<AccountModel> GetAll()
        {
            Mapper.Reset();
            Mapper.Initialize(c => c.CreateMap<Account, AccountModel>());
            return Mapper.Map<IEnumerable<Account>, IEnumerable<AccountModel>>(_accountRepository.GetAll());
        }

        public override AccountModel GetOne(int id)
        {
            Mapper.Reset();
            Mapper.Initialize(c => c.CreateMap<Account, AccountModel>());
            return Mapper.Map<Account, AccountModel>(_accountRepository.GetOne(id));
        }

        public override AccountModel Save(AccountModel entity)
        {
            Mapper.Reset();
            Mapper.Initialize(c => c.CreateMap<AccountModel, Account>());
            var model = Mapper.Map<AccountModel, Account>(entity);

            Mapper.Reset();
            Mapper.Initialize(c => c.CreateMap<Account, AccountModel>());
            return Mapper.Map<Account, AccountModel>(_accountRepository.Save(model));
        }

        public override void Delete(AccountModel entity)
        {
            Mapper.Reset();
            Mapper.Initialize(c => c.CreateMap<AccountModel, Account>());
            _accountRepository.Delete(Mapper.Map<AccountModel, Account>(entity));
        }

        public override void DeleteAll()
        {
            _accountRepository.DeleteAll();
        }

        public bool Exists(AccountModel entity)
        {
            Mapper.Reset();
            Mapper.Initialize(c => c.CreateMap<AccountModel, Account>());
            var model = Mapper.Map<AccountModel, Account>(entity);
            return _accountRepository.Exists(model, out _);
        }
    }
}