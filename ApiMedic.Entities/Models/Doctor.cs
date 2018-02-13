namespace ApiMedic.Entities.Models
{
    using Newtonsoft.Json;

    public class Doctor
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Identification { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("blood_type")]
        public string BloodType { get; set; }

        [JsonProperty("specialty_field")]
        public Specialty Specialty { get; set; }
    }
}