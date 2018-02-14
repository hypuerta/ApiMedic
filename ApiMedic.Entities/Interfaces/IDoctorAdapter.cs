// -------------------------------------------------------------------------------
// <copyright file="IDoctorAdapter.cs" company="ApiMedic.Api">
// ApiMedic.Api
// </copyright>
// <author>Herley Puerta</author>
// <email>hypuerta@hotmail.com</email>
// <date>14/02/2018</date>
// <summary>Defines Doctor adapter.</summary>
// -------------------------------------------------------------------------------
namespace ApiMedic.Entities.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    /// <summary>
    /// Defines Doctor adapter.
    /// </summary>
    public interface IDoctorAdapter
    {
        /// <summary>
        /// Get a doctor by id.
        /// </summary>
        /// <param name="idDoctor">Id doctor.</param>
        /// <returns>Instance of doctor.</returns>
        Task<Doctor> GetDoctor(int idDoctor);

        /// <summary>
        /// Get list of doctors.
        /// </summary>
        /// <returns>List of doctors.</returns>
        Task<IEnumerable<Doctor>> GetDoctors();
    }
}