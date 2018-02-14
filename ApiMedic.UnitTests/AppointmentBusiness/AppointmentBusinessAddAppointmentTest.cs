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

    [TestClass]
    public class AppointmentBusinessAddAppointmentTest
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
        public void AddAppointmentNull()
        {
            Appointment appointment = null;
            int result = this.appointmentBusiness.AddAppointment(appointment).Result;
            Assert.Equals(0, result);
        }

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