using System.Collections.Generic;
using AutoMapper;
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

        public override IEnumerable<ClientModel> GetAll()
        {
            Mapper.Reset();
            Mapper.Initialize(c => c.CreateMap<Client, ClientModel>());
            return Mapper.Map<IEnumerable<Client>, IEnumerable<ClientModel>>(_repository.GetAll());
        }

        public override ClientModel GetOne(int id)
        {
            Mapper.Reset();
            Mapper.Initialize(c => c.CreateMap<Client, ClientModel>());
            return Mapper.Map<Client, ClientModel>(_repository.GetOne(id));
        }

        public override void Save(ClientModel entity)
        {
            Mapper.Reset();
            Mapper.Initialize(c => c.CreateMap<ClientModel, Client>());
            _repository.Save(Mapper.Map<ClientModel, Client>(entity));
        }

        public override void Delete(ClientModel entity)
        {
            Mapper.Reset();
            Mapper.Initialize(c => c.CreateMap<ClientModel, Client>());
            _repository.Delete(Mapper.Map<ClientModel, Client>(entity));
        }

        public override void DeleteAll()
        {
            _repository.DeleteAll();
        }
    }
}