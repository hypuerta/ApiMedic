// -------------------------------------------------------------------------------
// <copyright file="ResponseAppointmentsByDoctor.cs" company="ApiMedic.Api">
// ApiMedic.Api
// </copyright>
// <author>Herley Puerta</author>
// <email>hypuerta@hotmail.com</email>
// <date>13/02/2018</date>
// <summary>Model ResponseAppointmentsByDoctor.</summary>
// -------------------------------------------------------------------------------
namespace ApiMedic.Entities.Responses
{
    /// <summary>
    /// Model ResponseAppointmentsByDoctor.
    /// </summary>
    public class ResponseAppointmentsByDoctor
    {
        /// <summary>
        /// Gets or sets DoctorIdentification.
        /// </summary>
        public string DoctorIdentification { get; set; }

        /// <summary>
        /// Gets or sets DoctorName.
        /// </summary>
        public string DoctorName { get; set; }

        /// <summary>
        /// Gets or sets PatientIdentification.
        /// </summary>
        public string PatientIdentification { get; set; }

        /// <summary>
        /// Gets or sets PatientName.
        /// </summary>
        public string PatientName { get; set; }

        /// <summary>
        /// Gets or sets DateAppointment.
        /// </summary>
        public string DateAppointment { get; set; }
    }
}