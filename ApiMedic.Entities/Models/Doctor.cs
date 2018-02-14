// -------------------------------------------------------------------------------
// <copyright file="Doctor.cs" company="ApiMedic.Api">
// ApiMedic.Api
// </copyright>
// <author>Herley Puerta</author>
// <email>hypuerta@hotmail.com</email>
// <date>13/02/2018</date>
// <summary>Model Doctor.</summary>
// -------------------------------------------------------------------------------
namespace ApiMedic.Entities.Models
{
    using Newtonsoft.Json;

    /// <summary>
    /// Model Doctor.
    /// </summary>
    public class Doctor
    {
        /// <summary>
        /// Gets or sets Id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets Url.
        /// </summary>
        public string Url { get; set; }

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
        /// Gets or sets BloodType.
        /// </summary>
        [JsonProperty("blood_type")]
        public string BloodType { get; set; }

        /// <summary>
        /// Gets or sets Specialty.
        /// </summary>
        [JsonProperty("specialty_field")]
        public Specialty Specialty { get; set; }
    }
}