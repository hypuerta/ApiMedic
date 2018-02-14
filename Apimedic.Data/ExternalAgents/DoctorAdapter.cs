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

    public class DoctorAdapter : AdapterBase, IDoctorAdapter
    {
        public async Task<Doctor> GetDoctor(int idDoctor)
        {
            Doctor doctor = new Doctor();
            this.Url = new Uri(string.Format("{0}/{1}", ConfigurationManager.AppSettings["UrlDoctor"], idDoctor));
            HttpResponseMessage httpResponse = await this.ExecuteGet();
            if (httpResponse.IsSuccessStatusCode)
            {
                doctor = JsonConvert.DeserializeObject<Doctor>(httpResponse.Content.ReadAsStringAsync().Result);
            }

            return doctor;
        }

        public async Task<IEnumerable<Doctor>> GetDoctors()
        {
            IEnumerable<Doctor> doctors = new List<Doctor>();
            this.Url = new Uri(string.Format("{0}", ConfigurationManager.AppSettings["UrlDoctor"]));
            HttpResponseMessage httpResponse = await this.ExecuteGet();
            if (httpResponse.IsSuccessStatusCode)
            {
                doctors = JsonConvert.DeserializeObject<IEnumerable<Doctor>>(httpResponse.Content.ReadAsStringAsync().Result);
            }

            return doctors;
        }
    }
}