using System.Collections.Generic;
using System.Web.Http;
using BusinessLayer.Filters;
using BusinessLayer.Models;
using BusinessLayer.Services;
using BusinessLayer.UnitsOfWork;

namespace Task5.Controllers
{
    public class EmployeeController : ApiController
    {
        private readonly EmployeeService _employeeService = new EmployeeService(new UnitOfWork());

        [HttpPost]
        public IEnumerable<EmployeeModel> All(EmployeeModel model)
        {
            return _employeeService.GetAll(new EmployeeFilter(model));
        }

        [HttpPost]
        public EmployeeModel Save(EmployeeModel model)
        {
            return _employeeService.Save(model);
        }

        [HttpDelete]
        public bool Delete(EmployeeModel model)
        {
            return _employeeService.Delete(model);
        }

        [HttpDelete]
        public bool DeleteAll()
        {
            return _employeeService.DeleteAll();
        }
    }
}