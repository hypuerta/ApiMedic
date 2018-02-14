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

    public class AppointmentRepository : IAppointmentRepository, IDisposable
    {
        private bool disposed = false;
        private readonly ApiMedicDB dataBase = new ApiMedicDB();

        public async Task<int> AddAppointment(Appointment appointment)
        {
            this.dataBase.Appointments.Add(appointment);
            return await this.dataBase.SaveChangesAsync();
        }

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

        public async Task<IList<Appointment>> GetAppointments()
        {
            return await this.dataBase.Appointments.ToListAsync();
        }

        public async Task<IList<Appointment>> GetAppointmentsByDoctor(int idDoctor, DateTime date)
        {
            return await this.dataBase.Appointments.Where(a => a.IdDoctor == idDoctor &&
                DbFunctions.TruncateTime(a.StartDate) == date &&
                a.Active == true).OrderBy(a => a.StartDate).ToListAsync();
        }

        public async Task<Appointment> GetAppointment(int id)
        {
            return await this.dataBase.Appointments.FindAsync(id);
        }

        public async Task<IList<Appointment>> GetAppointmentsInDateByDoctor(Appointment appointment)
        {
            return await this.dataBase.Appointments.Where(a => a.IdDoctor == appointment.IdDoctor &&
                (a.StartDate <= appointment.StartDate && a.EndDate >= appointment.StartDate) || 
                (a.StartDate <= appointment.EndDate && a.EndDate >= appointment.EndDate)).ToListAsync();
        }

        public async Task<int> DeleteAppointment(Appointment appointment)
        {
            Appointment appointmentNew = this.dataBase.Appointments.Find(appointment.IdAppointment);
            this.dataBase.Appointments.Remove(appointmentNew);
            return await this.dataBase.SaveChangesAsync();
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

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