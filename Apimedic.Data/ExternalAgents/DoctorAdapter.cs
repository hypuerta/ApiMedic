namespace ApiMedic.Data.ExternalAgents
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Threading.Tasks;
    using Entities.Interfaces;
    using Entities.Models;
    using Newtonsoft.Json;

    public class DoctorAdapter : AdapterBase, IDoctorAdapter
    {
        public Doctor GetDoctor(int idDoctor)
        {
            Doctor doctor = new Doctor();
            this.Url = new Uri(string.Format("{0}/{1}", ConfigurationManager.AppSettings["UrlDoctor"], idDoctor));
            var httpResponse = Task.Run(() => this.ExecuteGet());
            httpResponse.Wait();
            if (httpResponse.Result.IsSuccessStatusCode)
            {
                doctor = JsonConvert.DeserializeObject<Doctor>(httpResponse.Result.Content.ReadAsStringAsync().Result);
            }

            return doctor;
        }

        public IEnumerable<Doctor> GetDoctors()
        {
            IEnumerable<Doctor> doctors = new List<Doctor>();
            this.Url = new Uri(string.Format("{0}", ConfigurationManager.AppSettings["UrlDoctor"]));
            var httpResponse = Task.Run(() => this.ExecuteGet());
            httpResponse.Wait();
            if (httpResponse.Result.IsSuccessStatusCode)
            {
                doctors = JsonConvert.DeserializeObject<IEnumerable<Doctor>>(httpResponse.Result.Content.ReadAsStringAsync().Result);
            }

            return doctors;
        }
    }
}