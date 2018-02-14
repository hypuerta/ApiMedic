namespace ApiMedic.Entities.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public interface IDoctorAdapter
    {
        Task<Doctor> GetDoctor(int idDoctor);

        Task<IEnumerable<Doctor>> GetDoctors();
    }
}