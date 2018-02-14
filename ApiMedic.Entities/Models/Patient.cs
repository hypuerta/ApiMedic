// -------------------------------------------------------------------------------
// <copyright file="Patient.cs" company="ApiMedic.Api">
// ApiMedic.Api
// </copyright>
// <author>Herley Puerta</author>
// <email>hypuerta@hotmail.com</email>
// <date>13/02/2018</date>
// <summary>Model Patient.</summary>
// -------------------------------------------------------------------------------
namespace ApiMedic.Entities.Models
{
    using Newtonsoft.Json;

    /// <summary>
    /// Model Patient.
    /// </summary>
    public class Patient
    {
        /// <summary>
        /// Gets or sets Id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets History.
        /// </summary>
        public string History { get; set; }

        /// <summary>
        /// Gets or sets Identification.
        /// </summary>
        public string Identification { get; set; }

        /// <summary>
        /// Gets or sets FirstName.
        /// </summary>
        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets LastName.
        /// </summary>
        [JsonProperty("last_name")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets Genre.
        /// </summary>
        public string Genre { get; set; }

        /// <summary>
        /// Gets or sets CivilStatus.
        /// </summary>
        [JsonProperty("civil_status")]
        public string CivilStatus { get; set; }

        /// <summary>
        /// Gets or sets BloodType.
        /// </summary>
        [JsonProperty("blood_type")]
        public string BloodType { get; set; }

        /// <summary>
        /// Gets or sets DateBirth.
        /// </summary>
        [JsonProperty("date_birth")]
        public string DateBirth { get; set; }

        /// <summary>
        /// Gets or sets CityBirth.
        /// </summary>
        [JsonProperty("city_birth")]
        public string CityBirth { get; set; }

        /// <summary>
        /// Gets or sets Url.
        /// </summary>
        public string Url { get; set; }
    }
}