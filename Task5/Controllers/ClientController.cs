using System.Collections.Generic;
using System.Web.Http;
using BusinessLayer.Models;
using BusinessLayer.Services;
using BusinessLayer.UnitsOfWork;

namespace Task5.Controllers
{
    public class ClientController : ApiController
    {
        private readonly ClientService _clientService = new ClientService(new UnitOfWork());

        [HttpGet]
        public IEnumerable<ClientModel> All()
        {
            return _clientService.GetAll();
        }
    }
}