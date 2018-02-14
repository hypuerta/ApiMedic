namespace ApiMedic.Api.Controllers
{
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Http;
    using BusinessLogic.Classes;
    using Entities.Models;

    public class AppointmentController : ApiController
    {
        [Route("api/appointment/{id:int}")]
        [HttpGet]
        public async Task<IHttpActionResult> Appointment(int id)
        {
            return Content(HttpStatusCode.OK, await new AppointmentBusiness().GetAppointment(id));
        }

        [Route("api/appointment")]
        [HttpGet]
        public async Task<IHttpActionResult> Appointment()
        {
            return Content(HttpStatusCode.OK, await new AppointmentBusiness().GetAppointments());
        }

        [Route("api/appointment")]
        [HttpPost]
        public async Task<IHttpActionResult> Appointment(Appointment appointment)
        {
            return Content(HttpStatusCode.OK, await new AppointmentBusiness().AddAppointment(appointment));
        }

        [Route("api/appointment/cancel/{id:int}")]
        [HttpPost]
        public async Task<IHttpActionResult> CancelAppointment(int id)
        {
            return Content(HttpStatusCode.OK, await new AppointmentBusiness().CancelAppointment(id));
        }

        [Route("api/appointment/update")]
        [HttpPost]
        public async Task<IHttpActionResult> UpdateAppointment(Appointment appointment)
        {
            return Content(HttpStatusCode.OK, await new AppointmentBusiness().UpdateAppointment(appointment));
        }

        [Route("api/appointment/{id:int}")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteAppointment(int id)
        {
            return Content(HttpStatusCode.OK, await new AppointmentBusiness().DeleteAppointment(id));
        }
    }
}