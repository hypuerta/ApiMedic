// -------------------------------------------------------------------------------
// <copyright file="DoctorBusiness.cs" company="ApiMedic.Api">
// ApiMedic.Api
// </copyright>
// <author>Herley Puerta</author>
// <email>hypuerta@hotmail.com</email>
// <date>14/02/2018</date>
// <summary>Implements Doctor Business.</summary>
// -------------------------------------------------------------------------------
namespace ApiMedic.BusinessLogic.Classes
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using BusinessLogic.Interfaces;
    using Data.ExternalAgents;
    using Data.Repositories;
    using Entities.Interfaces;
    using Entities.Models;
    using Entities.Responses;
    using Utilities.Exceptions;
    using Utilities.Resources;

    /// <summary>
    /// Implements Doctor Business.
    /// </summary>
    public class DoctorBusiness : IDoctorBusiness
    {
        /// <summary>
        /// Doctor adapter.
        /// </summary>
        private readonly IDoctorAdapter doctorAdapter = null;

        /// <summary>
        /// Patient adapter.
        /// </summary>
        private readonly IPatientAdapter patientAdapter = null;

        /// <summary>
        /// Appointment repository.
        /// </summary>
        private readonly IAppointmentRepository appointmentRepository = null;

        /// <summary>
        /// Initializes a new instance of the DoctorBusiness class.
        /// </summary>
        public DoctorBusiness()
        {
            this.doctorAdapter = new DoctorAdapter();
            this.patientAdapter = new PatientAdapter();
            this.appointmentRepository = new AppointmentRepository();
        }

        /// <summary>
        /// Initializes a new instance of the DoctorBusiness class.
        /// </summary>
        /// <param name="doctorAdapter">Adapter doctor.</param>
        /// <param name="patientAdapter">Adapter patient.</param>
        /// <param name="appointmentRepository">Repository appointment.</param>
        public DoctorBusiness(IDoctorAdapter doctorAdapter, IPatientAdapter patientAdapter, IAppointmentRepository appointmentRepository)
        {
            this.doctorAdapter = doctorAdapter;
            this.patientAdapter = patientAdapter;
            this.appointmentRepository = appointmentRepository;
        }

        /// <summary>
        /// Get available time slots to doctor in date.
        /// </summary>
        /// <param name="idDoctor">Id doctor.</param>
        /// <param name="date">Date to search.</param>
        /// <returns>String with time slots.</returns>
        public async Task<IList<string>> GetAvailableTimesDoctor(int idDoctor, string date)
        {
            DateTime dateAppointment;
            if (DateTime.TryParse(date, out dateAppointment))
            {
                IList<Appointment> appointments = await this.appointmentRepository.GetAppointmentsByDoctor(idDoctor, dateAppointment);
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
            else
            {
                throw new BusinessException(ApiMedicMessages.ErrorFormatDate);
            }
        }

        /// <summary>
        /// Get assigned times by doctor in date.
        /// </summary>
        /// <param name="idDoctor">Id doctor.</param>
        /// <param name="date">Date to search.</param>
        /// <returns>List of assigned times by doctor.</returns>
        public async Task<IList<ResponseAppointmentsByDoctor>> GetAssignedTimeByDoctor(int idDoctor, string date)
        {
            DateTime dateAppointment;
            if (DateTime.TryParse(date, out dateAppointment))
            {
                IList<Appointment> appointments = await this.appointmentRepository.GetAppointmentsByDoctor(idDoctor, dateAppointment);
                Doctor doctor = await this.doctorAdapter.GetDoctor(idDoctor);
                List<ResponseAppointmentsByDoctor> response = new List<ResponseAppointmentsByDoctor>();
                foreach (Appointment appointment in appointments)
                {
                    Patient patient = await this.patientAdapter.GetPatient(appointment.IdPatient);
                    ResponseAppointmentsByDoctor reponseApponitment = new ResponseAppointmentsByDoctor();
                    reponseApponitment.DoctorIdentification = doctor.Identification;
                    reponseApponitment.DoctorName = doctor.FirstName + " " + doctor.LastName;
                    reponseApponitment.PatientIdentification = patient.Identification;
                    reponseApponitment.PatientName = patient.FirstName + " " + patient.LastName;
                    reponseApponitment.DateAppointment = appointment.StartDate.ToString("dd/MM/yyyy HH:mm") + " -- " + appointment.EndDate.ToString("dd/MM/yyyy HH:mm");
                    response.Add(reponseApponitment);
                }

                return response;
            }
            else
            {
                throw new BusinessException(ApiMedicMessages.ErrorFormatDate);
            }
        }
    }
}