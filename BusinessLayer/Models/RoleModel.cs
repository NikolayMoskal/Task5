using System.Collections.Generic;
using Newtonsoft.Json;

namespace BusinessLayer.Models
{
    public class RoleModel : Model
    {
        [JsonProperty("roleName")] public string RoleName { get; set; }

        [JsonIgnore] public IList<AccountModel> Accounts { get; set; }

        [JsonProperty("accounts")]
        private IList<AccountModel> AccountModels
        {
            set => Accounts = value;
        }
    }
}