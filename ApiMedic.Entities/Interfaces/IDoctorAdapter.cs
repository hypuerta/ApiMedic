namespace ApiMedic.Entities.Interfaces
{
    using System.Collections.Generic;
    using Models;

    public interface IDoctorAdapter
    {
        Doctor GetDoctor(int idDoctor);

        IEnumerable<Doctor> GetDoctors();
    }
}