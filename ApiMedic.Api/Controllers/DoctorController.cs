namespace ApiMedic.Api.Controllers
{
    using System;
    using System.Web.Http;
    using BusinessLogic;

    public class DoctorController : ApiController
    {
        [Route("api/doctor/availableTimeSlots/{idDoctor}/{date}")]
        [HttpGet]
        public IHttpActionResult AvailableTimeSlots(int idDoctor, string date)
        {
            return Ok(new DoctorBL().GetAvailableTimesDoctor(idDoctor, date));
        }
    }
}
