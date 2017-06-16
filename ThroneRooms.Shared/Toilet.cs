using Microsoft.Azure.Documents.Spatial;
using Newtonsoft.Json;

namespace ThroneRooms
{
    public class Toilet
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        public int ToiletId { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public string Address1 { get; set; }
        public string Town { get; set; }
        public string Postcode { get; set; }
        public string AddressNote { get; set; }
        public bool Male { get; set; }
        public bool Female { get; set; }
        public bool Unisex { get; set; }
        public bool AccessibleMale { get; set; }
        public bool AccessibleFemale { get; set; }
        public bool AccessibleUnisex { get; set; }
        public string AccessibleNote { get; set; }
        public bool Ambulant { get; set; }

        public bool Parking { get; set; }
        public string ParkingNote { get; set; }
        public bool ParkingAccessible { get; set; }
        public string AccessibleParkingNote { get; set; }
        public string IsOpen { get; set; }
        public string OpeningHoursSchedule { get; set; }
        public string OpeningHoursNote { get; set; }

        public bool BabyChange { get; set; }
        public bool Showers { get; set; }
        public bool DrinkingWater { get; set; }
        public bool SharpsDisposal { get; set; }
        public bool SanitaryDisposal { get; set; }

        public string Notes { get; set; }

        public Point Location { get; set; }
    }
}
