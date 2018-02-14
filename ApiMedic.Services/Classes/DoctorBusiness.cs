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

    public class DoctorBusiness : IDoctorBusiness
    {
        private readonly IDoctorAdapter doctorAdapter = null;
        private readonly IPatientAdapter patientAdapter = null;
        private readonly IAppointmentRepository appointmentRepository = null;

        public DoctorBusiness()
        {
            this.doctorAdapter = new DoctorAdapter();
            this.patientAdapter = new PatientAdapter();
            this.appointmentRepository = new AppointmentRepository();
        }

        public DoctorBusiness(IDoctorAdapter doctorAdapter, IPatientAdapter patientAdapter, IAppointmentRepository appointmentRepository)
        {
            this.doctorAdapter = doctorAdapter;
            this.patientAdapter = patientAdapter;
            this.appointmentRepository = appointmentRepository;
        }

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