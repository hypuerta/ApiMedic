namespace ApiMedic.Entities.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Models;

    public interface IAppointmentRepository
    {
        int AddAppointment(Appointment appointment);

        int UpdateAppointment(Appointment appointment);

        Appointment GetAppointment(int id);

        IQueryable<Appointment> GetAppointments();

        IList<Appointment> GetAppointmentsByDoctor(int idDoctor, DateTime date);
    }
}