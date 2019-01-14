using System.Collections.Generic;
using Newtonsoft.Json;

namespace BusinessLayer.Models
{
    public class ProductModel : Model
    {
        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("price")] public double Price { get; set; }

        [JsonIgnore] public IList<BookingModel> Bookings { get; set; }
    }
}