namespace ApiMedic.UnitTests.AppointmentBusiness
{
    using System.Collections.Generic;
    using BusinessLogic.Classes;
    using BusinessLogic.Interfaces;
    using Entities.Interfaces;
    using Entities.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    [TestClass]
    public class AppointmentBusinessGetAppointmentsTest
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
        public void GetAppointmentsRepositoryReturnsNull()
        {
            IList<Appointment> appointments = null;
            this.appointmentRepository.Setup(it => it.GetAppointments()).ReturnsAsync(appointments);
            IList<Appointment> result = this.appointmentBusiness.GetAppointments().Result;
            Assert.AreEqual(null, result);
        }

        [TestMethod]
        public void GetAppointmentsRepositoryDoesNotReturnsData()
        {
            IList<Appointment> appointments = new List<Appointment>();
            this.appointmentRepository.Setup(it => it.GetAppointments()).ReturnsAsync(appointments);
            IList<Appointment> result = this.appointmentBusiness.GetAppointments().Result;
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void GetAppointmentsSuccess()
        {
            IList<Appointment> appointment = new List<Appointment>() { new Appointment() {
                Active = true
            } };
            this.appointmentRepository.Setup(it => it.GetAppointments()).ReturnsAsync(appointment);
            IList<Appointment> result = this.appointmentBusiness.GetAppointments().Result;
            Assert.IsTrue(result.Count > 0);
        }
    }
}