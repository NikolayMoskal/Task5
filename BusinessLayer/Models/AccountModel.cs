using Newtonsoft.Json;

namespace BusinessLayer.Models
{
    public class AccountModel : Model
    {
        [JsonProperty("userName")] public string UserName { get; set; }

        [JsonProperty("password")] public string PasswordHash { get; set; }

        [JsonIgnore] public UserModel User { get; set; }

        [JsonProperty("user")]
        private UserModel UserModel
        {
            set => User = value;
        }

        [JsonProperty("role")] public RoleModel Role { get; set; }
    }
}