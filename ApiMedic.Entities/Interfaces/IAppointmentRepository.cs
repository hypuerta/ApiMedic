// -------------------------------------------------------------------------------
// <copyright file="IAppointmentRepository.cs" company="ApiMedic.Api">
// ApiMedic.Api
// </copyright>
// <author>Herley Puerta</author>
// <email>hypuerta@hotmail.com</email>
// <date>13/02/2018</date>
// <summary>Defines Appointment Repository.</summary>
// -------------------------------------------------------------------------------
namespace ApiMedic.Entities.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    /// <summary>
    /// Defines Appointment Repository.
    /// </summary>
    public interface IAppointmentRepository
    {
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
        /// Get appointment by id.
        /// </summary>
        /// <param name="id">Id of appointment to get.</param>
        /// <returns>Instance of appointment.</returns>
        Task<Appointment> GetAppointment(int id);

        /// <summary>
        /// Get list of Appointments.
        /// </summary>
        /// <returns>List of appointments.</returns>
        Task<IList<Appointment>> GetAppointments();

        /// <summary>
        /// Get Appointments by doctor.
        /// </summary>
        /// <param name="idDoctor">Id doctor.</param>
        /// <param name="date">Date to search appointments.</param>
        /// <returns>List of appointments by doctor in date.</returns>
        Task<IList<Appointment>> GetAppointmentsByDoctor(int idDoctor, DateTime date);

        /// <summary>
        /// Get appointments by boctor between start date and end date.
        /// </summary>
        /// <param name="appointment">Intance of appointment.</param>
        /// <returns>List of appointments.</returns>
        Task<IList<Appointment>> GetAppointmentsInDateByDoctor(Appointment appointment);

        /// <summary>
        /// Delete an appointment from database.
        /// </summary>
        /// <param name="appointment">Instance of appointment.</param>
        /// <returns>1 is correct.</returns>
        Task<int> DeleteAppointment(Appointment appointment);
    }
}