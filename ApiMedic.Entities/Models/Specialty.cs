// -------------------------------------------------------------------------------
// <copyright file="Specialty.cs" company="ApiMedic.Api">
// ApiMedic.Api
// </copyright>
// <author>Herley Puerta</author>
// <email>hypuerta@hotmail.com</email>
// <date>13/02/2018</date>
// <summary>Model Specialty.</summary>
// -------------------------------------------------------------------------------
namespace ApiMedic.Entities.Models
{
    using Newtonsoft.Json;

    /// <summary>
    /// Model Specialty.
    /// </summary>
    public class Specialty
    {
        /// <summary>
        /// Gets or sets Url.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets Id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets SpecialtyType.
        /// </summary>
        [JsonProperty("specialty_type")]
        public string SpecialtyType { get; set; }
    }
}