using System.Collections.Generic;
using Newtonsoft.Json;

namespace BusinessLayer.Models
{
    public class EmployeeModel : Model
    {
        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("bookings")] public IList<BookingModel> Bookings { get; set; }
    }
}