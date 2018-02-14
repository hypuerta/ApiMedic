// -------------------------------------------------------------------------------
// <copyright file="PatientAdapter.cs" company="ApiMedic.Api">
// ApiMedic.Api
// </copyright>
// <author>Herley Puerta</author>
// <email>hypuerta@hotmail.com</email>
// <date>14/02/2018</date>
// <summary>Implements Patient adapter.</summary>
// -------------------------------------------------------------------------------
namespace ApiMedic.Data.ExternalAgents
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Entities.Interfaces;
    using Entities.Models;
    using Newtonsoft.Json;

    /// <summary>
    /// Implements Patient adapter.
    /// </summary>
    public class PatientAdapter : AdapterBase, IPatientAdapter
    {
        /// <summary>
        /// Get a patient by id.
        /// </summary>
        /// <param name="idPatient">Id patient.</param>
        /// <returns>Instance of patient.</returns>
        public async Task<Patient> GetPatient(int idPatient)
        {
            Patient patient = new Patient();
            this.Url = new Uri(string.Format("{0}/{1}", ConfigurationManager.AppSettings["UrlPatient"], idPatient));
            HttpResponseMessage httpResponse = await this.ExecuteGet();
            if (httpResponse.IsSuccessStatusCode)
            {
                patient = JsonConvert.DeserializeObject<Patient>(httpResponse.Content.ReadAsStringAsync().Result);
            }

            return patient;
        }

        /// <summary>
        /// Get list of patients.
        /// </summary>
        /// <returns>List of patients.</returns>
        public async Task<IEnumerable<Patient>> GetPatients()
        {
            IEnumerable<Patient> patients = new List<Patient>();
            string requestUrl = string.Format("{0}", ConfigurationManager.AppSettings["UrlPatient"]);
            this.Url = new Uri(requestUrl);
            HttpResponseMessage httpResponse = await this.ExecuteGet();
            if (httpResponse.IsSuccessStatusCode)
            {
                patients = JsonConvert.DeserializeObject<IEnumerable<Patient>>(httpResponse.Content.ReadAsStringAsync().Result);
            }

            return patients;
        }
    }
}
