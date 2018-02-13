namespace ApiMedic.BusinessLogic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Data.ExternalAgents;
    using Data.Repositories;
    using Entities.Interfaces;
    using Entities.Models;
    using Entities.Responses;

    public class AppointmentBL
    {
        private readonly IDoctorAdapter doctorAdapter = null;
        private readonly IPatientAdapter patientAdapter = null;
        private readonly IAppointmentRepository appointmentRepository = null;

        public AppointmentBL()
        {
            this.doctorAdapter = new DoctorAdapter();
            this.appointmentRepository = new AppointmentRepository();
            this.patientAdapter = new PatientAdapter();
        }

        public AppointmentBL(IAppointmentRepository appointmentRepository, IDoctorAdapter doctorAdapter, IPatientAdapter patientAdapter)
        {
            this.appointmentRepository = appointmentRepository;
            this.doctorAdapter = doctorAdapter;
            this.patientAdapter = patientAdapter;
        }

        public Appointment GetAppointment(int idAppointmet)
        {
            Appointment response = this.appointmentRepository.GetAppointment(idAppointmet);
            return response;
        }

        public IQueryable<Appointment> GetAppointments()
        {
            return this.appointmentRepository.GetAppointments();
        }

        public int AddAppointment(Appointment appointment)
        {
            return this.appointmentRepository.AddAppointment(appointment);
        }

        public int UpdateAppointment(Appointment appointment)
        {
            Appointment appointmentExist = this.appointmentRepository.GetAppointment(appointment.IdAppointment);
            if (appointmentExist != null)
            {
                appointment.Active = false;
                return this.appointmentRepository.UpdateAppointment(appointment);
            }

            return 0;
        }

        public int CancelAppointment(int idAppointment)
        {
            Appointment appointment = this.appointmentRepository.GetAppointment(idAppointment);
            if (appointment != null)
            {
                appointment.Active = false;
                return this.appointmentRepository.UpdateAppointment(appointment);
            }

            return 0;
        }

        public IList<ResponseAppointmentsByDoctor> GetAppointmentsByDoctorAsync(int idDoctor, string date)
        {
            DateTime dateAppointment = Convert.ToDateTime(date);
            IList<Appointment> appointments = this.appointmentRepository.GetAppointmentsByDoctor(idDoctor, dateAppointment);
            Doctor doctor = this.doctorAdapter.GetDoctor(idDoctor);
            List<ResponseAppointmentsByDoctor> response = new List<ResponseAppointmentsByDoctor>();
            foreach (Appointment appointment in appointments)
            {
                Patient patient = this.patientAdapter.GetPatient(appointment.IdPatient);
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
    }
}