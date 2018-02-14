namespace ApiMedic.BusinessLogic.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Entities.Responses;

    public interface IDoctorBusiness
    {
        Task<IList<string>> GetAvailableTimesDoctor(int idDoctor, string date);

        Task<IList<ResponseAppointmentsByDoctor>> GetAssignedTimeByDoctor(int idDoctor, string date);
    }
}