// -------------------------------------------------------------------------------
// <copyright file="DoctorController.cs" company="ApiMedic.Api">
// ApiMedic.Api
// </copyright>
// <author>Herley Puerta</author>
// <email>hypuerta@hotmail.com</email>
// <date>14/02/2018</date>
// <summary>Implements Doctor Controller.</summary>
// -------------------------------------------------------------------------------
namespace ApiMedic.Api.Controllers
{
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Http;
    using BusinessLogic.Classes;

    /// <summary>
    /// Implements Doctor Controller.
    /// </summary>
    public class DoctorController : ApiController
    {
        /// <summary>
        /// Get available time slots to doctor in date.
        /// </summary>
        /// <param name="idDoctor">Id doctor.</param>
        /// <param name="date">Date to search.</param>
        /// <returns>String with time slots.</returns>
        [Route("api/doctor/availableTimeSlots/{idDoctor}/{date}")]
        [HttpGet]
        public async Task<IHttpActionResult> AvailableTimeSlots(int idDoctor, string date)
        {
            return this.Content(HttpStatusCode.OK, await new DoctorBusiness().GetAvailableTimesDoctor(idDoctor, date));
        }

        /// <summary>
        /// Get assigned times by doctor in date.
        /// </summary>
        /// <param name="idDoctor">Id doctor.</param>
        /// <param name="date">Date to search.</param>
        /// <returns>List of assigned times by doctor.</returns>
        [Route("api/doctor/getAppointment/{idDoctor}/{date}")]
        [HttpGet]
        public async Task<IHttpActionResult> AppointmentsByDoctor(int idDoctor, string date)
        {
            return this.Content(HttpStatusCode.OK, await new DoctorBusiness().GetAssignedTimeByDoctor(idDoctor, date));
        }
    }
}