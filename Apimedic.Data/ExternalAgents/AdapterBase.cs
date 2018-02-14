// -------------------------------------------------------------------------------
// <copyright file="AdapterBase.cs" company="ApiMedic.Api">
// ApiMedic.Api
// </copyright>
// <author>Herley Puerta</author>
// <email>hypuerta@hotmail.com</email>
// <date>14/02/2018</date>
// <summary>Base adapter.</summary>
// -------------------------------------------------------------------------------
namespace ApiMedic.Data.ExternalAgents
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    /// <summary>
    /// Base adapter.
    /// </summary>
    public class AdapterBase
    {
        /// <summary>
        /// Gets or sets Url request.
        /// </summary>
        protected Uri Url { get; set; }

        /// <summary>
        /// Execute get requets.
        /// </summary>
        /// <returns>response Http.</returns>
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