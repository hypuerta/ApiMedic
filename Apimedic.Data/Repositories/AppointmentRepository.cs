namespace ApiMedic.Data.Repositories
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using Api.Models;
    using Entities.Interfaces;
    using Entities.Models;
    using System.Collections.Generic;

    public class AppointmentRepository : IAppointmentRepository, IDisposable
    {
        private bool disposed = false;
        private readonly ApiMedicDB dataBase = new ApiMedicDB();

        public int AddAppointment(Appointment appointment)
        {
            this.dataBase.Appointments.Add(appointment);
            return this.dataBase.SaveChanges();
        }

        public int UpdateAppointment(Appointment appointment)
        {
            this.dataBase.Entry(appointment).State = EntityState.Modified;
            return this.dataBase.SaveChanges();
        }

        public IQueryable<Appointment> GetAppointments()
        {
            return this.dataBase.Appointments;
        }

        public IList<Appointment> GetAppointmentsByDoctor(int idDoctor, DateTime date)
        {
            return this.dataBase.Appointments.Where(a => a.IdDoctor == idDoctor &&
                DbFunctions.TruncateTime(a.StartDate) == date &&
                a.Active == true).OrderBy(a => a.StartDate).ToList();
        }

        public Appointment GetAppointment(int id)
        {
            return this.dataBase.Appointments.Find(id);
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