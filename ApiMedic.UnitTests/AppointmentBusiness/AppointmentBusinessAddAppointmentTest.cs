// -------------------------------------------------------------------------------
// <copyright file="AppointmentBusinessAddAppointmentTest.cs" company="ApiMedic.Api">
// ApiMedic.Api
// </copyright>
// <author>Herley Puerta</author>
// <email>hypuerta@hotmail.com</email>
// <date>13/02/2018</date>
// <summary>Test Class AppointmentBusinessAddAppointmentTest.</summary>
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
    /// Test Class AppointmentBusinessAddAppointmentTest.
    /// </summary>
    [TestClass]
    public class AppointmentBusinessAddAppointmentTest
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
        /// Appointmet business.
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
        /// Test Add appointment null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public void AddAppointmentNull()
        {
            Appointment appointment = null;
            int result = this.appointmentBusiness.AddAppointment(appointment).Result;
            Assert.Equals(0, result);
        }

        /// <summary>
        /// Test add appointment when doctor does not exists.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public void AddAppointmentDoctorDoesNotExists()
        {
            Appointment appointment = new Appointment();
            Doctor doctor = null;
            this.doctorAdapter.Setup(it => it.GetDoctor(It.IsAny<int>())).ReturnsAsync(doctor);
            int result = this.appointmentBusiness.AddAppointment(appointment).Result;
            Assert.AreEqual(0, result);
        }

        /// <summary>
        /// Test add appointmnet when patient does not exists.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public void AddAppointmentPatientDoesNotExists()
        {
            Appointment appointment = new Appointment();
            Patient patient = null;
            this.doctorAdapter.Setup(it => it.GetDoctor(It.IsAny<int>())).ReturnsAsync(new Doctor());
            this.patientAdapter.Setup(it => it.GetPatient(It.IsAny<int>())).ReturnsAsync(patient);
            int result = this.appointmentBusiness.AddAppointment(appointment).Result;
            Assert.AreEqual(0, result);
        }

        /// <summary>
        /// Test add appoinment when repository returns error.
        /// </summary>
        [TestMethod]
        public void AddAppointmentError()
        {
            Doctor doctor = new Doctor() { Id = 1 };
            Patient patient = new Patient() { Id = 1 };
            this.doctorAdapter.Setup(it => it.GetDoctor(It.IsAny<int>())).ReturnsAsync(doctor);
            this.patientAdapter.Setup(it => it.GetPatient(It.IsAny<int>())).ReturnsAsync(patient);
            Appointment appointment = new Appointment();
            IList<Appointment> appointments = new List<Appointment>();
            this.appointmentRepository.Setup(it => it.GetAppointmentsInDateByDoctor(It.IsAny<Appointment>())).ReturnsAsync(appointments);
            this.appointmentRepository.Setup(it => it.AddAppointment(It.IsAny<Appointment>())).ReturnsAsync(0);
            int result = this.appointmentBusiness.AddAppointment(appointment).Result;
            Assert.AreEqual(0, result);
        }

        /// <summary>
        /// Test add appointment when doctor has appointmnent in same date.
        /// </summary>
        [ExpectedException(typeof(AggregateException))]
        [TestMethod]
        public void AddAppointmentDoctorHasAppointmentSameDate()
        {
            this.doctorAdapter.Setup(it => it.GetDoctor(It.IsAny<int>())).ReturnsAsync(new Doctor());
            this.patientAdapter.Setup(it => it.GetPatient(It.IsAny<int>())).ReturnsAsync(new Patient());
            Appointment appointment = new Appointment();
            IList<Appointment> appointments = new List<Appointment>()
            {
                new Appointment()
                {
                    Active = true
                }
            };

            this.appointmentRepository.Setup(it => it.GetAppointmentsInDateByDoctor(It.IsAny<Appointment>())).ReturnsAsync(appointments);
            int result = this.appointmentBusiness.AddAppointment(appointment).Result;
            Assert.AreEqual(0, result);
        }

        /// <summary>
        /// Test add appointment success.
        /// </summary>
        [TestMethod]
        public void AddAppointmentSuccess() 
        {
            Doctor doctor = new Doctor() { Id = 1 };
            Patient patient = new Patient() { Id = 1 };
            this.doctorAdapter.Setup(it => it.GetDoctor(It.IsAny<int>())).ReturnsAsync(doctor);
            this.patientAdapter.Setup(it => it.GetPatient(It.IsAny<int>())).ReturnsAsync(patient);
            IList<Appointment> appointments = new List<Appointment>();
            this.appointmentRepository.Setup(it => it.GetAppointmentsInDateByDoctor(It.IsAny<Appointment>())).ReturnsAsync(appointments);
            Appointment appointment = new Appointment();
            this.appointmentRepository.Setup(it => it.AddAppointment(It.IsAny<Appointment>())).ReturnsAsync(1);
            int result = this.appointmentBusiness.AddAppointment(appointment).Result;
            Assert.AreEqual(1, result);
        }
    }
}