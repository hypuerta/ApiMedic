namespace ApiMedic.Entities.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public interface IPatientAdapter
    {
        Task<Patient> GetPatient(int idPatient);

        Task<IEnumerable<Patient>> GetPatients();
    }
}