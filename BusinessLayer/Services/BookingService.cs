using System.Collections.Generic;
using AutoMapper;
using BusinessLayer.Models;
using BusinessLayer.UnitsOfWork;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;

namespace BusinessLayer.Services
{
    public class BookingService : Service<BookingModel>
    {
        private readonly BookingRepository _repository;

        public BookingService(UnitOfWork unitOfWork) : base(unitOfWork)
        {
            _repository = UnitOfWork.BookingRepository;
        }

        public override IEnumerable<BookingModel> GetAll()
        {
            Mapper.Initialize(c => c.CreateMap<Booking, BookingModel>());
            return Mapper.Map<IEnumerable<Booking>, IEnumerable<BookingModel>>(_repository.GetAll());
        }

        public override BookingModel GetOne(int id)
        {
            Mapper.Initialize(c => c.CreateMap<Booking, BookingModel>());
            return Mapper.Map<Booking, BookingModel>(_repository.GetOne(id));
        }

        public override void Save(BookingModel entity)
        {
            Mapper.Initialize(c => c.CreateMap<BookingModel, Booking>());
            _repository.Save(Mapper.Map<BookingModel, Booking>(entity));
        }

        public override void Delete(BookingModel entity)
        {
            Mapper.Initialize(c => c.CreateMap<BookingModel, Booking>());
            _repository.Delete(Mapper.Map<BookingModel, Booking>(entity));
        }

        public override void DeleteAll()
        {
            _repository.DeleteAll();
        }
    }
}