// -------------------------------------------------------------------------------
// <copyright file="AppointmentBusinessGetAppointmentTest.cs" company="ApiMedic.Api">
// ApiMedic.Api
// </copyright>
// <author>Herley Puerta</author>
// <email>hypuerta@hotmail.com</email>
// <date>13/02/2018</date>
// <summary>Test Class AppointmentBusinessGetAppointmentTest.</summary>
// -------------------------------------------------------------------------------
namespace ApiMedic.UnitTests.AppointmentBusiness
{
    using BusinessLogic.Classes;
    using BusinessLogic.Interfaces;
    using Entities.Interfaces;
    using Entities.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    /// <summary>
    /// Test Class AppointmentBusinessGetAppointmentTest.
    /// </summary>
    [TestClass]
    public class AppointmentBusinessGetAppointmentTest
    {
        /// <summary>
        /// Repository appointment.
        /// </summary>
        private Mock<IAppointmentRepository> appointmentRepository = null;

        /// <summary>
        /// Adapter doctor.
        /// </summary>
        private Mock<IDoctorAdapter> doctorAdapter = null;

        /// <summary>
        /// Adapter patient.
        /// </summary>
        private Mock<IPatientAdapter> patientAdapter = null;

        /// <summary>
        /// Business appointment.
        /// </summary>
        private IAppointmentBusiness appointmentBusiness = null;

        /// <summary>
        /// Initialize values to tests.
        /// </summary>
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

        /// <summary>
        /// Test get appointment when repository returns null.
        /// </summary>
        [TestMethod]
        public void GetAppointmentRepositoryReturnsNull()
        {
            Appointment appointment = null;
            this.appointmentRepository.Setup(it => it.GetAppointment(It.IsAny<int>())).ReturnsAsync(appointment);
            Appointment result = this.appointmentBusiness.GetAppointment(It.IsAny<int>()).Result;
            Assert.AreEqual(null, result);
        }

        /// <summary>
        /// Test get appointment success.
        /// </summary>
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