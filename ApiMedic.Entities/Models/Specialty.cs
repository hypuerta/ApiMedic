namespace ApiMedic.Entities.Models
{
    using Newtonsoft.Json;

    public class Specialty
    {
        public string Url { get; set; }
        public string Id { get; set; }

        [JsonProperty("specialty_type")]
        public string SpecialtyType { get; set; }
    }
}