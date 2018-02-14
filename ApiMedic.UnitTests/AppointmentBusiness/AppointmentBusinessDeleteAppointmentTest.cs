// -------------------------------------------------------------------------------
// <copyright file="AppointmentBusinessDeleteAppointmentTest.cs" company="ApiMedic.Api">
// ApiMedic.Api
// </copyright>
// <author>Herley Puerta</author>
// <email>hypuerta@hotmail.com</email>
// <date>13/02/2018</date>
// <summary>Test Class AppointmentBusinessDeleteAppointmentTest.</summary>
// -------------------------------------------------------------------------------
namespace ApiMedic.UnitTests.AppointmentBusiness
{
    using System;
    using BusinessLogic.Classes;
    using BusinessLogic.Interfaces;
    using Entities.Interfaces;
    using Entities.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    /// <summary>
    /// Test Class AppointmentBusinessDeleteAppointmentTest.
    /// </summary>
    [TestClass]
    public class AppointmentBusinessDeleteAppointmentTest
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
        /// Initialize values to test.
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
        /// Test delete appointment when appointment does not exists.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public void DeleteAppointmentDoesNotExists()
        {
            Appointment appointmentExists = null;
            this.appointmentRepository.Setup(it => it.GetAppointment(It.IsAny<int>())).ReturnsAsync(appointmentExists);
            int result = this.appointmentBusiness.DeleteAppointment(1).Result;
            Assert.AreEqual(0, result);
        }

        /// <summary>
        /// Test delete appointment when repository returns error.
        /// </summary>
        [TestMethod]
        public void DeleteAppointmentError()
        {
            Appointment appointment = new Appointment();
            this.appointmentRepository.Setup(it => it.GetAppointment(It.IsAny<int>())).ReturnsAsync(appointment);
            this.appointmentRepository.Setup(it => it.DeleteAppointment(It.IsAny<Appointment>())).ReturnsAsync(0);
            int result = this.appointmentBusiness.DeleteAppointment(1).Result;
            Assert.AreEqual(0, result);
        }

        /// <summary>
        /// Test delete appointment success.
        /// </summary>
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