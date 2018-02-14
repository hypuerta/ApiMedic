// -------------------------------------------------------------------------------
// <copyright file="DoctorBusinessGetAssignedTimeByDoctorTest.cs" company="ApiMedic.Api">
// ApiMedic.Api
// </copyright>
// <author>Herley Puerta</author>
// <email>hypuerta@hotmail.com</email>
// <date>13/02/2018</date>
// <summary>Test Class DoctorBusinessGetAssignedTimeByDoctorTest.</summary>
// -------------------------------------------------------------------------------
namespace ApiMedic.UnitTests.AppointmentBusiness
{
    using System;
    using System.Collections.Generic;
    using BusinessLogic.Classes;
    using BusinessLogic.Interfaces;
    using Entities.Interfaces;
    using Entities.Models;
    using Entities.Responses;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    /// <summary>
    /// Test Class DoctorBusinessGetAssignedTimeByDoctorTest.
    /// </summary>
    [TestClass]
    public class DoctorBusinessGetAssignedTimeByDoctorTest
    {
        /// <summary>
        /// Adapter doctor.
        /// </summary>
        private Mock<IDoctorAdapter> doctorAdapter = null;

        /// <summary>
        /// adapter patient.
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
        /// Test get assigned times by doctor when error format date.
        /// </summary>
        [ExpectedException(typeof(AggregateException))]
        [TestMethod]
        public void GetAssignedTimeByDoctorErrorFormatDate()
        {
            IList<ResponseAppointmentsByDoctor> result = this.doctorBusiness.GetAssignedTimeByDoctor(It.IsAny<int>(), "date").Result;
            Assert.AreEqual(null, result);
        }

        /// <summary>
        /// Test get assigned times by doctor when doctor has not appointments.
        /// </summary>
        [TestMethod]
        public void GetAssignedTimeByDoctorWithoutAppointments()
        {
            IList<Appointment> appointments = new List<Appointment>();
            this.appointmentRepository.Setup(it => it.GetAppointmentsByDoctor(It.IsAny<int>(), It.IsAny<DateTime>())).ReturnsAsync(appointments);
            IList<ResponseAppointmentsByDoctor> result = this.doctorBusiness.GetAssignedTimeByDoctor(It.IsAny<int>(), "10/10/18").Result;
            Assert.AreEqual(0, result.Count);
        }

        /// <summary>
        /// Test get assigned times when doctor has appointments.
        /// </summary>
        [TestMethod]
        public void GetAssignedTimeByDoctorWithAppointments()
        {
            IList<Appointment> appointments = new List<Appointment>()
            {
                new Appointment()
                {
                    Active = true
                }
            };

            this.appointmentRepository.Setup(it => it.GetAppointmentsByDoctor(It.IsAny<int>(), It.IsAny<DateTime>())).ReturnsAsync(appointments);
            this.doctorAdapter.Setup(it => it.GetDoctor(It.IsAny<int>())).ReturnsAsync(new Doctor());
            this.patientAdapter.Setup(it => it.GetPatient(It.IsAny<int>())).ReturnsAsync(new Patient());
            IList<ResponseAppointmentsByDoctor> result = this.doctorBusiness.GetAssignedTimeByDoctor(It.IsAny<int>(), "10/10/18").Result;
            Assert.IsTrue(result.Count > 0);
        }
    }
}