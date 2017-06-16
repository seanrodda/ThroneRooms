using Microsoft.Azure.Documents.Spatial;
using Newtonsoft.Json;

namespace ThroneRooms
{
    public class SimpleToilet
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        public string Name { get; set; }
        public Point Location { get; set; }
    }
}
