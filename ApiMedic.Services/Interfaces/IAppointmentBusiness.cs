// -------------------------------------------------------------------------------
// <copyright file="IAppointmentBusiness.cs" company="ApiMedic.Api">
// ApiMedic.Api
// </copyright>
// <author>Herley Puerta</author>
// <email>hypuerta@hotmail.com</email>
// <date>13/02/2018</date>
// <summary>Defines Appointment Business Logic.</summary>
// -------------------------------------------------------------------------------
namespace ApiMedic.BusinessLogic.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Entities.Models;

    /// <summary>
    /// Defines Appointment Business Logic.
    /// </summary>
    public interface IAppointmentBusiness
    {
        /// <summary>
        /// Get an instance of Appointment.
        /// </summary>
        /// <param name="idAppointmet">Id of Appointment.</param>
        /// <returns>Instance of Appointment</returns>
        Task<Appointment> GetAppointment(int idAppointmet);

        /// <summary>
        /// Get list of Appointments.
        /// </summary>
        /// <returns>List of appointments.</returns>
        Task<IList<Appointment>> GetAppointments();

        /// <summary>
        /// Add appointment to database.
        /// </summary>
        /// <param name="appointment">Instance of appointment.</param>
        /// <returns>1 is correct.</returns>
        Task<int> AddAppointment(Appointment appointment);

        /// <summary>
        /// Update an appointment.
        /// </summary>
        /// <param name="appointment">Instance of appointment.</param>
        /// <returns>1 is correct.</returns>
        Task<int> UpdateAppointment(Appointment appointment);

        /// <summary>
        /// Set active to false to an appointment.
        /// </summary>
        /// <param name="idAppointment">Id appointment.</param>
        /// <returns>1 is correct.</returns>
        Task<int> CancelAppointment(int idAppointment);

        /// <summary>
        /// Delete an appointment from database.
        /// </summary>
        /// <param name="idAppointment">Id appointment to delete.</param>
        /// <returns>1 is correct.</returns>
        Task<int> DeleteAppointment(int idAppointment);
    }
}