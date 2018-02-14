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
    public class AppointmentBusinessUpdateAppointmentTest
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
        public void UpdateAppointmentNull()
        {
            Appointment appointment = null;
            int result = this.appointmentBusiness.UpdateAppointment(appointment).Result;
            Assert.Equals(0, result);
        }

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