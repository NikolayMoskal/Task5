using System.Collections.Generic;
using Newtonsoft.Json;

namespace BusinessLayer.Models
{
    public class ClientModel : Model
    {
        [JsonProperty("name")] public string Name { get; set; }

        [JsonIgnore] public IList<BookingModel> Bookings { get; set; }
    }
}