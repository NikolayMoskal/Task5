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
    public class ClientService : Service<ClientModel>
    {
        private readonly ClientRepository _repository;

        public ClientService(UnitOfWork unitOfWork) : base(unitOfWork)
        {
            _repository = UnitOfWork.ClientRepository;
        }

        public override IEnumerable<ClientModel> GetAll(Filter filter = null)
        {
            if (filter == null) filter = new Filter();

            var mapper = new MapperConfiguration(c => c.CreateMap<Client, ClientModel>()).CreateMapper();
            return mapper.Map<IEnumerable<Client>, IEnumerable<ClientModel>>(_repository.GetAll())
                .Where(filter.IsValid).ToList();
        }

        public override ClientModel GetOne(int id)
        {
            var mapper = new MapperConfiguration(c => c.CreateMap<Client, ClientModel>()).CreateMapper();
            return mapper.Map<Client, ClientModel>(_repository.GetOne(id));
        }

        public override ClientModel Save(ClientModel entity)
        {
            var mapper = new MapperConfiguration(c => c.CreateMap<ClientModel, Client>()).CreateMapper();
            var item = mapper.Map<ClientModel, Client>(entity);

            mapper = new MapperConfiguration(c => c.CreateMap<Client, ClientModel>()).CreateMapper();
            return mapper.Map<Client, ClientModel>(_repository.Save(item));
        }

        public override bool Delete(ClientModel entity)
        {
            var mapper = new MapperConfiguration(c => c.CreateMap<ClientModel, Client>()).CreateMapper();
            return _repository.Delete(mapper.Map<ClientModel, Client>(entity));
        }

        public override bool DeleteAll()
        {
            return _repository.DeleteAll();
        }
    }
}