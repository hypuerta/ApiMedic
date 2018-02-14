namespace ApiMedic.BusinessLogic.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Entities.Models;

    public interface IAppointmentBusiness
    {
        Task<Appointment> GetAppointment(int idAppointmet);
        Task<IList<Appointment>> GetAppointments();
        Task<int> AddAppointment(Appointment appointment);
        Task<int> UpdateAppointment(Appointment appointment);
        Task<int> CancelAppointment(int idAppointment);
        Task<int> DeleteAppointment(int idAppointment);
    }
}