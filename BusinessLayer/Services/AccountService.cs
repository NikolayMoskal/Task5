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
    public class AccountService : Service<AccountModel>
    {
        private readonly AccountRepository _accountRepository;

        public AccountService(UnitOfWork unitOfWork) : base(unitOfWork)
        {
            _accountRepository = UnitOfWork.AccountRepository;
        }

        public override IEnumerable<AccountModel> GetAll(Filter filter = null)
        {
            if (filter == null) filter = new Filter();

            var mapper = new MapperConfiguration(c => c.CreateMap<Account, AccountModel>()).CreateMapper();
            return mapper.Map<IEnumerable<Account>, IEnumerable<AccountModel>>(_accountRepository.GetAll())
                .Where(filter.IsValid).ToList();
        }

        public override AccountModel GetOne(int id)
        {
            var mapper = new MapperConfiguration(c => c.CreateMap<Account, AccountModel>()).CreateMapper();
            return mapper.Map<Account, AccountModel>(_accountRepository.GetOne(id));
        }

        public override AccountModel Save(AccountModel entity)
        {
            var mapper = new MapperConfiguration(c => c.CreateMap<AccountModel, Account>()).CreateMapper();
            var model = mapper.Map<AccountModel, Account>(entity);

            mapper = new MapperConfiguration(c => c.CreateMap<Account, AccountModel>()).CreateMapper();
            return mapper.Map<Account, AccountModel>(_accountRepository.Save(model));
        }

        public override bool Delete(AccountModel entity)
        {
            var mapper = new MapperConfiguration(c => c.CreateMap<AccountModel, Account>()).CreateMapper();
            return _accountRepository.Delete(mapper.Map<AccountModel, Account>(entity));
        }

        public override bool DeleteAll()
        {
            return _accountRepository.DeleteAll();
        }

        public AccountModel Exists(AccountModel entity)
        {
            var mapper = new MapperConfiguration(c => c.CreateMap<AccountModel, Account>()).CreateMapper();
            var model = mapper.Map<AccountModel, Account>(entity);

            mapper = new MapperConfiguration(c => c.CreateMap<Account, AccountModel>()).CreateMapper();
            return _accountRepository.Exists(model, out var foundModel)
                ? mapper.Map<Account, AccountModel>(foundModel)
                : null;
        }
    }
}