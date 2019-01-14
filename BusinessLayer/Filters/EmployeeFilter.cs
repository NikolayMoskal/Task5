using BusinessLayer.Models;

namespace BusinessLayer.Filters
{
    public class EmployeeFilter : Filter
    {
        private readonly EmployeeModel _templateModel;

        public EmployeeFilter(EmployeeModel templateModel)
        {
            _templateModel = templateModel;
        }

        public override bool IsValid(object obj)
        {
            if (!(obj is EmployeeModel model)) return false;

            return _templateModel?.Name == null ||
                   string.IsNullOrEmpty(_templateModel.Name) ||
                   model.Name == _templateModel.Name;
        }
    }
}