namespace ApiMedic.UnitTests.AppointmentBusiness
{
    using System;
    using BusinessLogic.Classes;
    using BusinessLogic.Interfaces;
    using Entities.Interfaces;
    using Entities.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    [TestClass]
    public class AppointmentBusinessDeleteAppointmentTest
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
        [ExpectedException(typeof(AggregateException))]
        public void DeleteAppointmentDoesNotExists()
        {
            Appointment appointmentExists = null;
            this.appointmentRepository.Setup(it => it.GetAppointment(It.IsAny<int>())).ReturnsAsync(appointmentExists);
            int result = this.appointmentBusiness.DeleteAppointment(1).Result;
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void DeleteAppointmentError()
        {
            Appointment appointment = new Appointment();
            this.appointmentRepository.Setup(it => it.GetAppointment(It.IsAny<int>())).ReturnsAsync(appointment);
            this.appointmentRepository.Setup(it => it.DeleteAppointment(It.IsAny<Appointment>())).ReturnsAsync(0);
            int result = this.appointmentBusiness.DeleteAppointment(1).Result;
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void DeleteAppointmentSuccess() 
        {
            Appointment appointment = new Appointment();
            this.appointmentRepository.Setup(it => it.GetAppointment(It.IsAny<int>())).ReturnsAsync(appointment);
            this.appointmentRepository.Setup(it => it.DeleteAppointment(It.IsAny<Appointment>())).ReturnsAsync(1);
            int result = this.appointmentBusiness.DeleteAppointment(1).Result;
            Assert.AreEqual(1, result);
        }
    }
}