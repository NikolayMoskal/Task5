using System;
using System.Web.Http;
using BusinessLayer.Models;
using BusinessLayer.Services;
using BusinessLayer.UnitsOfWork;

namespace Task5.Controllers
{
    public class AuthController : ApiController
    {
        private readonly AccountService _accountService;
        private readonly RoleService _roleService;
        private readonly UserService _userService;

        public AuthController()
        {
            var unitOfWork = new UnitOfWork();
            _userService = new UserService(unitOfWork);
            _accountService = new AccountService(unitOfWork);
            _roleService = new RoleService(unitOfWork);
        }

        [HttpPost]
        public bool SignUp(CompositeModel model)
        {
            try
            {
                model.Account.PasswordHash = BCryptPasswordEncoder.Hash(model.Account.PasswordHash);
                var user = _userService.Save(model.User);
                model.Account.User.Id = user.Id;
                var role = _roleService.Save(model.Role);
                model.Account.Role.Id = role.Id;
                _accountService.Save(model.Account);
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }

        [HttpPost]
        public bool SignIn(AccountModel account)
        {
            return _accountService.Exists(account);
        }
    }
}