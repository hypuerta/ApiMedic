// -------------------------------------------------------------------------------
// <copyright file="IPatientAdapter.cs" company="ApiMedic.Api">
// ApiMedic.Api
// </copyright>
// <author>Herley Puerta</author>
// <email>hypuerta@hotmail.com</email>
// <date>14/02/2018</date>
// <summary>Defines Patient adapter.</summary>
// -------------------------------------------------------------------------------
namespace ApiMedic.Entities.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    /// <summary>
    /// Defines Patient adapter.
    /// </summary>
    public interface IPatientAdapter
    {
        /// <summary>
        /// Get a patient by id.
        /// </summary>
        /// <param name="idPatient">Id patient.</param>
        /// <returns>Instance of patient.</returns>
        Task<Patient> GetPatient(int idPatient);

        /// <summary>
        /// Get list of patients.
        /// </summary>
        /// <returns>List of patients.</returns>
        Task<IEnumerable<Patient>> GetPatients();
    }
}