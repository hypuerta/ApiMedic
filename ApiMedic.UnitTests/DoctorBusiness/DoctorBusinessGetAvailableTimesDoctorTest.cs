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
    public class DoctorBusinessGetAvailableTimesDoctorTest
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
        public void GetAvailableTimesDoctorErrorFormatDate()
        {
            IList<string> result = this.doctorBusiness.GetAvailableTimesDoctor(It.IsAny<int>(), "date").Result;
            Assert.AreEqual(null, result);
        }

        [TestMethod]
        public void GetAvailableTimesDoctorWithoutAppointments()
        {
            IList<Appointment> appointments = new List<Appointment>();
            this.appointmentRepository.Setup(it => it.GetAppointmentsByDoctor(It.IsAny<int>(), It.IsAny<DateTime>())).ReturnsAsync(appointments);
            IList<string> result = this.doctorBusiness.GetAvailableTimesDoctor(It.IsAny<int>(), "10/10/18").Result;
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("00:00 - 23:59", result[0]);
        }

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