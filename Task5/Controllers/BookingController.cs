using System.Collections.Generic;
using System.Web.Http;
using BusinessLayer.Filters;
using BusinessLayer.Models;
using BusinessLayer.Services;
using BusinessLayer.UnitsOfWork;

namespace Task5.Controllers
{
    public class BookingController : ApiController
    {
        private readonly BookingService _bookingService = new BookingService(new UnitOfWork());

        [HttpPost]
        public IEnumerable<BookingModel> All(BookingModel model)
        {
            return _bookingService.GetAll(new BookingFilter(model));
        }

        [HttpPost]
        public BookingModel Save(BookingModel model)
        {
            return _bookingService.Save(model);
        }

        [HttpDelete]
        public bool Delete(BookingModel model)
        {
            return _bookingService.Delete(model);
        }

        [HttpDelete]
        public bool DeleteAll()
        {
            return _bookingService.DeleteAll();
        }
    }
}