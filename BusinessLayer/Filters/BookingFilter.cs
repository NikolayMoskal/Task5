using BusinessLayer.Models;

namespace BusinessLayer.Filters
{
    public class BookingFilter : Filter
    {
        private readonly BookingModel _templateModel;

        public BookingFilter(BookingModel templateModel)
        {
            _templateModel = templateModel;
        }

        public override bool IsValid(object obj)
        {
            if (!(obj is BookingModel model)) return false;

            if (_templateModel == null)
                return true;

            if (_templateModel.Client == null && _templateModel.Employee == null && _templateModel.Product == null)
                return true;

            if (_templateModel.Client != null && new ClientFilter(_templateModel.Client).IsValid(model.Client))
                return true;

            if (_templateModel.Employee != null && new EmployeeFilter(_templateModel.Employee).IsValid(model.Employee))
                return true;

            if (_templateModel.Product != null && new ProductFilter(_templateModel.Product).IsValid(model.Product))
                return true;

            return _templateModel.Date == model.Date;
        }
    }
}