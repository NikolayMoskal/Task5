using System;
using Newtonsoft.Json;

namespace BusinessLayer.Models
{
    public class BookingModel : Model
    {
        [JsonProperty("date")] public DateTime Date { get; set; }

        [JsonProperty("client")] public ClientModel Client { get; set; }

        [JsonProperty("employee")] public EmployeeModel Employee { get; set; }

        [JsonProperty("product")] public ProductModel Product { get; set; }
    }
}