using System;
using Newtonsoft.Json;

namespace BusinessLayer.Models
{
    public class BookingModel : Model
    {
        [JsonProperty("date")] public DateTime Date { get; set; }

        [JsonIgnore] public ClientModel Client { get; set; }

        [JsonIgnore] public EmployeeModel Employee { get; set; }

        [JsonIgnore] public ProductModel Product { get; set; }
    }
}