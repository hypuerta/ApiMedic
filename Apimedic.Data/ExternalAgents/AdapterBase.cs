namespace ApiMedic.Data.ExternalAgents
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class AdapterBase
    {
        protected Uri Url { get; set; }

        protected async Task<HttpResponseMessage> ExecuteGet()
        {
            HttpResponseMessage responseHttp = null;
            using (var httpClient = new HttpClient())
            {
                responseHttp = await httpClient.GetAsync(this.Url);
            }

            return responseHttp;
        }
    }
}