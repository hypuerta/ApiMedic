namespace ApiMedic.BusinessLogic
{
    using System;
    using System.Collections.Generic;
    using Data.ExternalAgents;
    using Data.Repositories;
    using Entities.Interfaces;
    using Entities.Models;

    public class DoctorBL
    {
        private readonly IDoctorAdapter doctorAdapter = null;
        private readonly IAppointmentRepository appointmentRepository = null;

        public DoctorBL()
        {
            this.doctorAdapter = new DoctorAdapter();
            this.appointmentRepository = new AppointmentRepository();
        }

        public DoctorBL(IDoctorAdapter doctorAdapter)
        {
            this.doctorAdapter = doctorAdapter;
        }

        public List<string> GetAvailableTimesDoctor(int idDoctor, string date)
        {
            DateTime dateAppointment = Convert.ToDateTime(date);
            IList<Appointment> appointments = this.appointmentRepository.GetAppointmentsByDoctor(idDoctor, dateAppointment);
            List<string> availableTime = new List<string>();
            if (appointments.Count == 0)
            {
                availableTime.Add("00:00 - 23:59");
            }
            else
            {
                string startTime = "00:00";
                foreach (Appointment appointment in appointments)
                {
                    availableTime.Add(startTime + "-" + appointment.StartDate.ToString("HH:mm"));
                    startTime = appointment.EndDate.ToString("HH:mm");
                }

                availableTime.Add(startTime + "-" + "23:59");
            }

            return availableTime;
        }
    }
}