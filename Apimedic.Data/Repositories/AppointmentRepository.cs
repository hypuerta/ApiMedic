// -------------------------------------------------------------------------------
// <copyright file="AppointmentRepository.cs" company="ApiMedic.Api">
// ApiMedic.Api
// </copyright>
// <author>Herley Puerta</author>
// <email>hypuerta@hotmail.com</email>
// <date>13/02/2018</date>
// <summary>Implements Appointment Repository.</summary>
// -------------------------------------------------------------------------------
namespace ApiMedic.Data.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using Data.Context;
    using Entities.Interfaces;
    using Entities.Models;

    /// <summary>
    /// Implements Appointment Repository.
    /// </summary>
    public class AppointmentRepository : IAppointmentRepository, IDisposable
    {
        /// <summary>
        /// Database instance.
        /// </summary>
        private readonly ApiMedicDB dataBase = new ApiMedicDB();

        /// <summary>
        /// Disposed resource.
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// Add appointment to database.
        /// </summary>
        /// <param name="appointment">Instance of appointment.</param>
        /// <returns>1 is correct.</returns>
        public async Task<int> AddAppointment(Appointment appointment)
        {
            this.dataBase.Appointments.Add(appointment);
            return await this.dataBase.SaveChangesAsync();
        }

        /// <summary>
        /// Update an appointment.
        /// </summary>
        /// <param name="appointment">Instance of appointment.</param>
        /// <returns>1 is correct.</returns>
        public async Task<int> UpdateAppointment(Appointment appointment)
        {
            Appointment appointmentNew = this.dataBase.Appointments.Find(appointment.IdAppointment);
            appointmentNew.IdDoctor = appointment.IdDoctor;
            appointmentNew.IdPatient = appointment.IdPatient;
            appointmentNew.StartDate = appointment.StartDate;
            appointmentNew.EndDate = appointment.EndDate;
            appointmentNew.Active = appointment.Active;
            this.dataBase.Entry(appointmentNew).State = EntityState.Modified;
            return await this.dataBase.SaveChangesAsync();
        }

        /// <summary>
        /// Get list of Appointments.
        /// </summary>
        /// <returns>List of appointments.</returns>
        public async Task<IList<Appointment>> GetAppointments()
        {
            return await this.dataBase.Appointments.ToListAsync();
        }

        /// <summary>
        /// Get Appointments by doctor.
        /// </summary>
        /// <param name="idDoctor">Id doctor.</param>
        /// <param name="date">Date to search appointments.</param>
        /// <returns>List of appointments by doctor in date.</returns>
        public async Task<IList<Appointment>> GetAppointmentsByDoctor(int idDoctor, DateTime date)
        {
            return await this.dataBase.Appointments.Where(a => a.IdDoctor == idDoctor &&
                DbFunctions.TruncateTime(a.StartDate) == date &&
                a.Active == true).OrderBy(a => a.StartDate).ToListAsync();
        }

        /// <summary>
        /// Get appointment by id.
        /// </summary>
        /// <param name="id">Id of appointment to get.</param>
        /// <returns>Instance of appointment.</returns>
        public async Task<Appointment> GetAppointment(int id)
        {
            return await this.dataBase.Appointments.FindAsync(id);
        }

        /// <summary>
        /// Get appointments by boctor between start date and end date.
        /// </summary>
        /// <param name="appointment">Intance of appointment.</param>
        /// <returns>List of appointments.</returns>
        public async Task<IList<Appointment>> GetAppointmentsInDateByDoctor(Appointment appointment)
        {
            return await this.dataBase.Appointments.Where(a => (a.IdDoctor == appointment.IdDoctor) &&
                ((a.StartDate <= appointment.StartDate && a.EndDate >= appointment.StartDate) || 
                (a.StartDate <= appointment.EndDate && a.EndDate >= appointment.EndDate))).ToListAsync();
        }

        /// <summary>
        /// Delete an appointment from database.
        /// </summary>
        /// <param name="appointment">Instance of appointment.</param>
        /// <returns>1 is correct.</returns>
        public async Task<int> DeleteAppointment(Appointment appointment)
        {
            Appointment appointmentNew = this.dataBase.Appointments.Find(appointment.IdAppointment);
            this.dataBase.Appointments.Remove(appointmentNew);
            return await this.dataBase.SaveChangesAsync();
        }

        /// <summary>
        /// Dispose resource.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose resources.
        /// </summary>
        /// <param name="disposing">True if disposing.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed && disposing)
            {
                this.dataBase.Dispose();
            }

            this.disposed = true;
        }
    }
}