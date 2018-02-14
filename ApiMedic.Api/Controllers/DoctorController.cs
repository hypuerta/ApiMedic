namespace ApiMedic.Api.Controllers
{
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Http;
    using BusinessLogic.Classes;

    public class DoctorController : ApiController
    {
        [Route("api/doctor/availableTimeSlots/{idDoctor}/{date}")]
        [HttpGet]
        public async Task<IHttpActionResult> AvailableTimeSlots(int idDoctor, string date)
        {
            return Content(HttpStatusCode.OK, await new DoctorBusiness().GetAvailableTimesDoctor(idDoctor, date));
        }

        [Route("api/doctor/getAppointment/{idDoctor}/{date}")]
        [HttpGet]
        public async Task<IHttpActionResult> AppointmentsByDoctor(int idDoctor, string date)
        {
            return Content(HttpStatusCode.OK, await new DoctorBusiness().GetAssignedTimeByDoctor(idDoctor, date));
        }
    }
}