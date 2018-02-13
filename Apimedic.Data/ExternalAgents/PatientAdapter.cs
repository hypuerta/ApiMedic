namespace ApiMedic.Data.ExternalAgents
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Threading.Tasks;
    using Entities.Interfaces;
    using Entities.Models;
    using Newtonsoft.Json;

    public class PatientAdapter: AdapterBase, IPatientAdapter
    {
        public Patient GetPatient(int idPatient)
        {
            Patient patient = new Patient();
            this.Url = new Uri(string.Format("{0}/{1}", ConfigurationManager.AppSettings["UrlPatient"], idPatient));
            var httpResponse = Task.Run(() => this.ExecuteGet());
            httpResponse.Wait();
            if (httpResponse.Result.IsSuccessStatusCode)
            {
                patient = JsonConvert.DeserializeObject<Patient>(httpResponse.Result.Content.ReadAsStringAsync().Result);
            }

            return patient;
        }

        public IEnumerable<Patient> GetPatients()
        {
            IEnumerable<Patient> patients = new List<Patient>();
            string requestUrl = string.Format("{0}", ConfigurationManager.AppSettings["UrlPatient"]);
            this.Url = new Uri(requestUrl);
            var httpResponse = Task.Run(() => this.ExecuteGet());
            httpResponse.Wait();
            if (httpResponse.Result.IsSuccessStatusCode)
            {
                patients = JsonConvert.DeserializeObject<IEnumerable<Patient>>(httpResponse.Result.Content.ReadAsStringAsync().Result);
            }

            return patients;
        }
    }
}
