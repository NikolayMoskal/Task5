using Newtonsoft.Json;

namespace BusinessLayer.Models
{
    public class CompositeModel
    {
        [JsonProperty("user")] public UserModel User { get; set; }

        [JsonProperty("account")] public AccountModel Account { get; set; }

        [JsonProperty("role")] public RoleModel Role { get; set; }
    }
}