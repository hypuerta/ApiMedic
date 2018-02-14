// -------------------------------------------------------------------------------
// <copyright file="AppointmentBusinessGetAppointmentsTest.cs" company="ApiMedic.Api">
// ApiMedic.Api
// </copyright>
// <author>Herley Puerta</author>
// <email>hypuerta@hotmail.com</email>
// <date>13/02/2018</date>
// <summary>Test Class AppointmentBusinessGetAppointmentsTest.</summary>
// -------------------------------------------------------------------------------
namespace ApiMedic.UnitTests.AppointmentBusiness
{
    using System.Collections.Generic;
    using BusinessLogic.Classes;
    using BusinessLogic.Interfaces;
    using Entities.Interfaces;
    using Entities.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    /// <summary>
    /// Test Class AppointmentBusinessGetAppointmentsTest.
    /// </summary>
    [TestClass]
    public class AppointmentBusinessGetAppointmentsTest
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
        /// Test get appointments when repository returns null.
        /// </summary>
        [TestMethod]
        public void GetAppointmentsRepositoryReturnsNull()
        {
            IList<Appointment> appointments = null;
            this.appointmentRepository.Setup(it => it.GetAppointments()).ReturnsAsync(appointments);
            IList<Appointment> result = this.appointmentBusiness.GetAppointments().Result;
            Assert.AreEqual(null, result);
        }

        /// <summary>
        /// Test get appointments when repository does not returns data.
        /// </summary>
        [TestMethod]
        public void GetAppointmentsRepositoryDoesNotReturnsData()
        {
            IList<Appointment> appointments = new List<Appointment>();
            this.appointmentRepository.Setup(it => it.GetAppointments()).ReturnsAsync(appointments);
            IList<Appointment> result = this.appointmentBusiness.GetAppointments().Result;
            Assert.AreEqual(0, result.Count);
        }

        /// <summary>
        /// Test get appointments success.
        /// </summary>
        [TestMethod]
        public void GetAppointmentsSuccess()
        {
            IList<Appointment> appointment = new List<Appointment>()
            {
                new Appointment()
                {
                    Active = true
                }
            };
            this.appointmentRepository.Setup(it => it.GetAppointments()).ReturnsAsync(appointment);
            IList<Appointment> result = this.appointmentBusiness.GetAppointments().Result;
            Assert.IsTrue(result.Count > 0);
        }
    }
}