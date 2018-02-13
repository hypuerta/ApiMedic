namespace ApiMedic.Entities.Models
{
    using Newtonsoft.Json;

    public class Patient
    {
        public int Id { get; set; }

        public string History { get; set; }

        public string Identification { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        public string Genre { get; set; }

        [JsonProperty("civil_status")]
        public string CivilStatus { get; set; }

        [JsonProperty("blood_type")]
        public string BloodType { get; set; }

        [JsonProperty("date_birth")]
        public string DateBirth { get; set; }

        [JsonProperty("city_birth")]
        public string CityBirth { get; set; }

        public string Url { get; set; }
    }
}