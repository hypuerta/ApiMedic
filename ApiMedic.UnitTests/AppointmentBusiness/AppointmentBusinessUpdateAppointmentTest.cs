// -------------------------------------------------------------------------------
// <copyright file="AppointmentBusinessUpdateAppointmentTest.cs" company="ApiMedic.Api">
// ApiMedic.Api
// </copyright>
// <author>Herley Puerta</author>
// <email>hypuerta@hotmail.com</email>
// <date>13/02/2018</date>
// <summary>Test Class AppointmentBusinessUpdateAppointmentTest.</summary>
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
    /// Test Class AppointmentBusinessUpdateAppointmentTest.
    /// </summary>
    [TestClass]
    public class AppointmentBusinessUpdateAppointmentTest
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
        /// Test update appointment null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public void UpdateAppointmentNull()
        {
            Appointment appointment = null;
            int result = this.appointmentBusiness.UpdateAppointment(appointment).Result;
            Assert.Equals(0, result);
        }

        /// <summary>
        /// Test update appointment that does not exists.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public void UpdateAppointmentDoesNotExists()
        {
            Appointment appointmentExists = null;
            Appointment appointment = new Appointment();
            this.appointmentRepository.Setup(it => it.GetAppointment(It.IsAny<int>())).ReturnsAsync(appointmentExists);
            int result = this.appointmentBusiness.UpdateAppointment(appointment).Result;
            Assert.AreEqual(0, result);
        }

        /// <summary>
        /// Test update appointment when doctor does not exists.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public void UpdateAppointmentDoctorDoesNotExists()
        {
            Appointment appointment = new Appointment();
            Doctor doctor = null;
            this.doctorAdapter.Setup(it => it.GetDoctor(It.IsAny<int>())).ReturnsAsync(doctor);
            int result = this.appointmentBusiness.UpdateAppointment(appointment).Result;
            Assert.AreEqual(0, result);
        }

        /// <summary>
        /// Test update appointment when patient does not exists.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public void UpdateAppointmentPatientDoesNotExists()
        {
            Appointment appointment = new Appointment();
            Patient patient = null;
            this.doctorAdapter.Setup(it => it.GetDoctor(It.IsAny<int>())).ReturnsAsync(new Doctor());
            this.patientAdapter.Setup(it => it.GetPatient(It.IsAny<int>())).ReturnsAsync(patient);
            int result = this.appointmentBusiness.UpdateAppointment(appointment).Result;
            Assert.AreEqual(0, result);
        }

        /// <summary>
        /// Test update appointment when repository returns error.
        /// </summary>
        [TestMethod]
        public void UpdateAppointmentError()
        {
            Doctor doctor = new Doctor() { Id = 1 };
            Patient patient = new Patient() { Id = 1 };
            this.doctorAdapter.Setup(it => it.GetDoctor(It.IsAny<int>())).ReturnsAsync(doctor);
            this.patientAdapter.Setup(it => it.GetPatient(It.IsAny<int>())).ReturnsAsync(patient);
            Appointment appointment = new Appointment();
            this.appointmentRepository.Setup(it => it.GetAppointment(It.IsAny<int>())).ReturnsAsync(appointment);
            this.appointmentRepository.Setup(it => it.UpdateAppointment(It.IsAny<Appointment>())).ReturnsAsync(0);
            int result = this.appointmentBusiness.UpdateAppointment(appointment).Result;
            Assert.AreEqual(0, result);
        }

        /// <summary>
        /// Test update appointment success.
        /// </summary>
        [TestMethod]
        public void UpdateAppointmentSuccess() 
        {
            Doctor doctor = new Doctor() { Id = 1 };
            Patient patient = new Patient() { Id = 1 };
            this.doctorAdapter.Setup(it => it.GetDoctor(It.IsAny<int>())).ReturnsAsync(doctor);
            this.patientAdapter.Setup(it => it.GetPatient(It.IsAny<int>())).ReturnsAsync(patient);
            Appointment appointment = new Appointment();
            this.appointmentRepository.Setup(it => it.GetAppointment(It.IsAny<int>())).ReturnsAsync(appointment);
            this.appointmentRepository.Setup(it => it.UpdateAppointment(It.IsAny<Appointment>())).ReturnsAsync(1);
            int result = this.appointmentBusiness.UpdateAppointment(appointment).Result;
            Assert.AreEqual(1, result);
        }
    }
}