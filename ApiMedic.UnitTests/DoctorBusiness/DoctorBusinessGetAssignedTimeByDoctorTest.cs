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

    [TestClass]
    public class DoctorBusinessGetAssignedTimeByDoctorTest
    {
        private Mock<IDoctorAdapter> doctorAdapter = null;
        private Mock<IPatientAdapter> patientAdapter = null;
        private Mock<IAppointmentRepository> appointmentRepository = null;
        private IDoctorBusiness doctorBusiness = null;

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

        [ExpectedException(typeof(AggregateException))]
        [TestMethod]
        public void GetAssignedTimeByDoctorErrorFormatDate()
        {
            IList<ResponseAppointmentsByDoctor> result = this.doctorBusiness.GetAssignedTimeByDoctor(It.IsAny<int>(), "date").Result;
            Assert.AreEqual(null, result);
        }

        [TestMethod]
        public void GetAssignedTimeByDoctorWithoutAppointments()
        {
            IList<Appointment> appointments = new List<Appointment>();
            this.appointmentRepository.Setup(it => it.GetAppointmentsByDoctor(It.IsAny<int>(), It.IsAny<DateTime>())).ReturnsAsync(appointments);
            IList<ResponseAppointmentsByDoctor> result = this.doctorBusiness.GetAssignedTimeByDoctor(It.IsAny<int>(), "10/10/18").Result;
            Assert.AreEqual(0, result.Count);
        }

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