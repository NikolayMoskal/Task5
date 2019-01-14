using System.Collections.Generic;
using System.Web.Http;
using BusinessLayer.Filters;
using BusinessLayer.Models;
using BusinessLayer.Services;
using BusinessLayer.UnitsOfWork;

namespace Task5.Controllers
{
    public class ClientController : ApiController
    {
        private readonly ClientService _clientService = new ClientService(new UnitOfWork());

        [HttpPost]
        public IEnumerable<ClientModel> All(ClientModel model)
        {
            return _clientService.GetAll(new ClientFilter(model));
        }

        [HttpPost]
        public ClientModel Save(ClientModel model)
        {
            return _clientService.Save(model);
        }

        [HttpDelete]
        public bool Delete(ClientModel model)
        {
            return _clientService.Delete(model);
        }

        [HttpDelete]
        public bool DeleteAll()
        {
            return _clientService.DeleteAll();
        }
    }
}