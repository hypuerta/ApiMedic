// -------------------------------------------------------------------------------
// <copyright file="AppointmentController.cs" company="ApiMedic.Api">
// ApiMedic.Api
// </copyright>
// <author>Herley Puerta</author>
// <email>hypuerta@hotmail.com</email>
// <date>14/02/2018</date>
// <summary>Implements Appointment Controller.</summary>
// -------------------------------------------------------------------------------
namespace ApiMedic.Api.Controllers
{
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Http;
    using BusinessLogic.Classes;
    using Entities.Models;

    /// <summary>
    /// Implements Appointment Controller.
    /// </summary>
    public class AppointmentController : ApiController
    {
        /// <summary>
        /// Get Appointment by id.
        /// </summary>
        /// <param name="id">Id appointment.</param>
        /// <returns>Instance of appointment</returns>
        [Route("api/appointment/{id:int}")]
        [HttpGet]
        public async Task<IHttpActionResult> Appointment(int id)
        {
            return this.Content(HttpStatusCode.OK, await new AppointmentBusiness().GetAppointment(id));
        }

        /// <summary>
        /// Get list of all appointments.
        /// </summary>
        /// <returns>List of appointments.</returns>
        [Route("api/appointment")]
        [HttpGet]
        public async Task<IHttpActionResult> Appointment()
        {
            return this.Content(HttpStatusCode.OK, await new AppointmentBusiness().GetAppointments());
        }

        /// <summary>
        /// Add appointment to database.
        /// </summary>
        /// <param name="appointment">Instance of appointment.</param>
        /// <returns>1 is correct.</returns>
        [Route("api/appointment")]
        [HttpPost]
        public async Task<IHttpActionResult> Appointment(Appointment appointment)
        {
            return this.Content(HttpStatusCode.OK, await new AppointmentBusiness().AddAppointment(appointment));
        }

        /// <summary>
        /// Set active to false to an appointment.
        /// </summary>
        /// <param name="id">Id appointment.</param>
        /// <returns>1 is correct.</returns>
        [Route("api/appointment/cancel/{id:int}")]
        [HttpPost]
        public async Task<IHttpActionResult> CancelAppointment(int id)
        {
            return this.Content(HttpStatusCode.OK, await new AppointmentBusiness().CancelAppointment(id));
        }

        /// <summary>
        /// Update an appointment.
        /// </summary>
        /// <param name="appointment">Instance of appointment.</param>
        /// <returns>1 is correct.</returns>
        [Route("api/appointment/update")]
        [HttpPost]
        public async Task<IHttpActionResult> UpdateAppointment(Appointment appointment)
        {
            return this.Content(HttpStatusCode.OK, await new AppointmentBusiness().UpdateAppointment(appointment));
        }

        /// <summary>
        /// Delete an appointment from database.
        /// </summary>
        /// <param name="id">Id appointment to delete.</param>
        /// <returns>1 is correct.</returns>
        [Route("api/appointment/{id:int}")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteAppointment(int id)
        {
            return this.Content(HttpStatusCode.OK, await new AppointmentBusiness().DeleteAppointment(id));
        }
    }
}