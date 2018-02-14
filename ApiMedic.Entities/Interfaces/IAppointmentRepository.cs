namespace ApiMedic.Entities.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public interface IAppointmentRepository
    {
        Task<int> AddAppointment(Appointment appointment);

        Task<int> UpdateAppointment(Appointment appointment);

        Task<Appointment> GetAppointment(int id);

        Task<IList<Appointment>> GetAppointments();

        Task<IList<Appointment>> GetAppointmentsByDoctor(int idDoctor, DateTime date);

        Task<IList<Appointment>> GetAppointmentsInDateByDoctor(Appointment appointment);

        Task<int> DeleteAppointment(Appointment appointment);
    }
}