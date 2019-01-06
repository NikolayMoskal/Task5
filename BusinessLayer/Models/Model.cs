using Newtonsoft.Json;

namespace BusinessLayer.Models
{
    public class Model
    {
        [JsonProperty("id")] public int Id { get; set; }
    }
}