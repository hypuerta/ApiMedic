namespace ApiMedic.Entities.Interfaces
{
    using System.Collections.Generic;
    using Models;

    public interface IPatientAdapter
    {
        Patient GetPatient(int idPatient);

        IEnumerable<Patient> GetPatients();
    }
}