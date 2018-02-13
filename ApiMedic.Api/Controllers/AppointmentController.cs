namespace ApiMedic.Api.Controllers
{
    using System.Web.Http;
    using BusinessLogic;
    using Entities.Models;

    public class AppointmentController : ApiController
    {
        [Route("api/appointment/{id:int}")]
        [HttpGet]
        public IHttpActionResult Appointment(int id)
        {
            return Ok(new AppointmentBL().GetAppointment(id));
        }

        [Route("api/appointment")]
        [HttpGet]
        public IHttpActionResult Appointment()
        {
            return Ok(new AppointmentBL().GetAppointments());
        }

        [Route("api/appointment")]
        [HttpPost]
        public IHttpActionResult Appointment(Appointment appointment)
        {
            return Ok(new AppointmentBL().AddAppointment(appointment));
        }

        [Route("api/appointment/cancel")]
        [HttpPost]
        public IHttpActionResult CancelAppointment(int idAppointment)
        {
            return Ok(new AppointmentBL().CancelAppointment(idAppointment));
        }

        [Route("api/appointment/update")]
        [HttpPost]
        public IHttpActionResult UpdateAppointment(Appointment appointment)
        {
            return Ok(new AppointmentBL().UpdateAppointment(appointment));
        }

        [Route("api/appointment/getAppointment/{idDoctor}/{date}")]
        [HttpGet]
        public IHttpActionResult AppointmentsByDoctor(int idDoctor, string date)
        {
            return Ok(new AppointmentBL().GetAppointmentsByDoctorAsync(idDoctor, date));
        }
    }
}