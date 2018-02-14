// -------------------------------------------------------------------------------
// <copyright file="DoctorBusinessGetAvailableTimesDoctorTest.cs" company="ApiMedic.Api">
// ApiMedic.Api
// </copyright>
// <author>Herley Puerta</author>
// <email>hypuerta@hotmail.com</email>
// <date>13/02/2018</date>
// <summary>Test Class DoctorBusinessGetAvailableTimesDoctorTest.</summary>
// -------------------------------------------------------------------------------
namespace ApiMedic.UnitTests.AppointmentBusiness
{
    using System;
    using System.Collections.Generic;
    using BusinessLogic.Classes;
    using BusinessLogic.Interfaces;
    using Entities.Interfaces;
    using Entities.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    /// <summary>
    /// Test Class DoctorBusinessGetAvailableTimesDoctorTest.
    /// </summary>
    [TestClass]
    public class DoctorBusinessGetAvailableTimesDoctorTest
    {
        /// <summary>
        /// Adapter doctor.
        /// </summary>
        private Mock<IDoctorAdapter> doctorAdapter = null;

        /// <summary>
        /// Adapter patient.
        /// </summary>
        private Mock<IPatientAdapter> patientAdapter = null;

        /// <summary>
        /// Repository appointment.
        /// </summary>
        private Mock<IAppointmentRepository> appointmentRepository = null;

        /// <summary>
        /// Business doctor.
        /// </summary>
        private IDoctorBusiness doctorBusiness = null;

        /// <summary>
        /// Initialize values to tests.
        /// </summary>
        [TestInitialize]
        public void InitializeTest()
        {
            this.doctorAdapter = new Mock<IDoctorAdapter>();
            this.patientAdapter = new Mock<IPatientAdapter>();
            this.appointmentRepository = new Mock<IAppointmentRepository>();
            this.doctorBusiness = new DoctorBusiness(
                this.doctorAdapter.Object,
                this.patientAdapter.Object,
                this.appointmentRepository.Object);
        }

        /// <summary>
        /// Test get available times of doctor with error format date.
        /// </summary>
        [ExpectedException(typeof(AggregateException))]
        [TestMethod]
        public void GetAvailableTimesDoctorErrorFormatDate()
        {
            IList<string> result = this.doctorBusiness.GetAvailableTimesDoctor(It.IsAny<int>(), "date").Result;
            Assert.AreEqual(null, result);
        }

        /// <summary>
        /// Test get available times doctor without appointments.
        /// </summary>
        [TestMethod]
        public void GetAvailableTimesDoctorWithoutAppointments()
        {
            IList<Appointment> appointments = new List<Appointment>();
            this.appointmentRepository.Setup(it => it.GetAppointmentsByDoctor(It.IsAny<int>(), It.IsAny<DateTime>())).ReturnsAsync(appointments);
            IList<string> result = this.doctorBusiness.GetAvailableTimesDoctor(It.IsAny<int>(), "10/10/18").Result;
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("00:00 - 23:59", result[0]);
        }

        /// <summary>
        /// Test get available times doctor without appointments.
        /// </summary>
        [TestMethod]
        public void GetAvailableTimesDoctorWithAppointments()
        {
            IList<Appointment> appointments = new List<Appointment>()
            {
                new Appointment()
                {
                    Active = true
                }
            };
            this.appointmentRepository.Setup(it => it.GetAppointmentsByDoctor(It.IsAny<int>(), It.IsAny<DateTime>())).ReturnsAsync(appointments);
            IList<string> result = this.doctorBusiness.GetAvailableTimesDoctor(It.IsAny<int>(), "10/10/18").Result;
            Assert.IsTrue(result.Count > 1);
        }
    }
}