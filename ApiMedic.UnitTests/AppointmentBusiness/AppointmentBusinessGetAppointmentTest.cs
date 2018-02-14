namespace ApiMedic.UnitTests.AppointmentBusiness
{
    using BusinessLogic.Classes;
    using BusinessLogic.Interfaces;
    using Entities.Interfaces;
    using Entities.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    [TestClass]
    public class AppointmentBusinessGetAppointmentTest
    {
        private Mock<IAppointmentRepository> appointmentRepository = null;
        private Mock<IDoctorAdapter> doctorAdapter = null;
        private Mock<IPatientAdapter> patientAdapter = null;
        private IAppointmentBusiness appointmentBusiness = null;

        [TestInitialize]
        public void InitializeTest()
        {
            this.appointmentRepository = new Mock<IAppointmentRepository>();
            this.doctorAdapter = new Mock<IDoctorAdapter>();
            this.patientAdapter = new Mock<IPatientAdapter>();
            this.appointmentBusiness = new AppointmentBusiness(
                this.appointmentRepository.Object,
                this.doctorAdapter.Object,
                this.patientAdapter.Object);
        }

        [TestMethod]
        public void GetAppointmentRepositoryReturnsNull()
        {
            Appointment appointment = null;
            this.appointmentRepository.Setup(it => it.GetAppointment(It.IsAny<int>())).ReturnsAsync(appointment);
            Appointment result = this.appointmentBusiness.GetAppointment(It.IsAny<int>()).Result;
            Assert.AreEqual(null, result);
        }

        [TestMethod]
        public void GetAppointmentSuccess()
        {
            Appointment appointment = new Appointment();
            this.appointmentRepository.Setup(it => it.GetAppointment(It.IsAny<int>())).ReturnsAsync(appointment);
            Appointment result = this.appointmentBusiness.GetAppointment(It.IsAny<int>()).Result;
            Assert.AreNotEqual(null, result);
        }
    }
}