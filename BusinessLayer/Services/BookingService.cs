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
    public class BookingService : Service<BookingModel>
    {
        private readonly BookingRepository _repository;

        public BookingService(UnitOfWork unitOfWork) : base(unitOfWork)
        {
            _repository = UnitOfWork.BookingRepository;
        }

        public override IEnumerable<BookingModel> GetAll(Filter filter = null)
        {
            if (filter == null) filter = new Filter();

            var mapper = new MapperConfiguration(c => c.CreateMap<Booking, BookingModel>()).CreateMapper();
            return mapper.Map<IEnumerable<Booking>, IEnumerable<BookingModel>>(_repository.GetAll())
                .Where(filter.IsValid).ToList();
        }

        public override BookingModel GetOne(int id)
        {
            var mapper = new MapperConfiguration(c => c.CreateMap<Booking, BookingModel>()).CreateMapper();
            return mapper.Map<Booking, BookingModel>(_repository.GetOne(id));
        }

        public override BookingModel Save(BookingModel entity)
        {
            var mapper = new MapperConfiguration(c => c.CreateMap<BookingModel, Booking>()).CreateMapper();
            var model = mapper.Map<BookingModel, Booking>(entity);

            mapper = new MapperConfiguration(c => c.CreateMap<Booking, BookingModel>()).CreateMapper();
            return mapper.Map<Booking, BookingModel>(_repository.Save(model));
        }

        public override bool Delete(BookingModel entity)
        {
            var mapper = new MapperConfiguration(c => c.CreateMap<BookingModel, Booking>()).CreateMapper();
            return _repository.Delete(mapper.Map<BookingModel, Booking>(entity));
        }

        public override bool DeleteAll()
        {
            return _repository.DeleteAll();
        }
    }
}