// -------------------------------------------------------------------------------
// <copyright file="IDoctorBusiness.cs" company="ApiMedic.Api">
// ApiMedic.Api
// </copyright>
// <author>Herley Puerta</author>
// <email>hypuerta@hotmail.com</email>
// <date>14/02/2018</date>
// <summary>Defines Doctor Business.</summary>
// -------------------------------------------------------------------------------
namespace ApiMedic.BusinessLogic.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Entities.Responses;

    /// <summary>
    /// Defines Doctor Business.
    /// </summary>
    public interface IDoctorBusiness
    {
        /// <summary>
        /// Get available time slots to doctor in date.
        /// </summary>
        /// <param name="idDoctor">Id doctor.</param>
        /// <param name="date">Date to search.</param>
        /// <returns>String with time slots.</returns>
        Task<IList<string>> GetAvailableTimesDoctor(int idDoctor, string date);

        /// <summary>
        /// Get assigned times by doctor in date.
        /// </summary>
        /// <param name="idDoctor">Id doctor.</param>
        /// <param name="date">Date to search.</param>
        /// <returns>List of assigned times by doctor.</returns>
        Task<IList<ResponseAppointmentsByDoctor>> GetAssignedTimeByDoctor(int idDoctor, string date);
    }
}