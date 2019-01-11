using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace BusinessLayer.Models
{
    public class UserModel : Model
    {
        [JsonProperty("firstName")] public string FirstName { get; set; }

        [JsonProperty("lastName")] public string LastName { get; set; }

        [JsonProperty("birthDate")] public DateTime BirthDate { get; set; }

        [JsonProperty("accounts")] public IList<AccountModel> Accounts { get; set; }
    }
}