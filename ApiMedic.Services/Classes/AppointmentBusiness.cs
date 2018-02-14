// -------------------------------------------------------------------------------
// <copyright file="AppointmentBusiness.cs" company="ApiMedic.Api">
// ApiMedic.Api
// </copyright>
// <author>Herley Puerta</author>
// <email>hypuerta@hotmail.com</email>
// <date>13/02/2018</date>
// <summary>Implements Appointment Business Logic.</summary>
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
    using Utilities.Exceptions;
    using Utilities.Resources;

    /// <summary>
    /// Implements Appointment Business Logic.
    /// </summary>
    public class AppointmentBusiness : IAppointmentBusiness
    {
        /// <summary>
        /// Appointment repository.
        /// </summary>
        private readonly IAppointmentRepository appointmentRepository = null;

        /// <summary>
        /// Appointment adapter.
        /// </summary>
        private readonly IDoctorAdapter doctorAdapter = null;

        /// <summary>
        /// Patient adapter.
        /// </summary>
        private readonly IPatientAdapter patientAdapter = null;

        /// <summary>
        /// Initializes a new instance of the AppointmentBusiness class.
        /// </summary>
        public AppointmentBusiness()
        {
            this.appointmentRepository = new AppointmentRepository();
            this.doctorAdapter = new DoctorAdapter();
            this.patientAdapter = new PatientAdapter();
        }

        /// <summary>
        /// Initializes a new instance of the AppointmentBusiness class.
        /// </summary>
        /// <param name="appointmentRepository">Repository appointment.</param>
        /// <param name="doctorAdapter">Adapter doctor.</param>
        /// <param name="patientAdapter">Adapter patient.</param>
        public AppointmentBusiness(
            IAppointmentRepository appointmentRepository,
            IDoctorAdapter doctorAdapter,
            IPatientAdapter patientAdapter)
        {
            this.appointmentRepository = appointmentRepository;
            this.doctorAdapter = doctorAdapter;
            this.patientAdapter = patientAdapter;
        }

        /// <summary>
        /// Get an instance of Appointment.
        /// </summary>
        /// <param name="idAppointmet">Id of Appointment.</param>
        /// <returns>Instance of Appointment</returns>
        public async Task<Appointment> GetAppointment(int idAppointmet)
        {
            try
            {
                return await this.appointmentRepository.GetAppointment(idAppointmet);
            }
            catch (Exception)
            {
                throw new BusinessException(ApiMedicMessages.ErrorGettingAppointment);
            }
        }

        /// <summary>
        /// Get list of Appointments.
        /// </summary>
        /// <returns>List of appointments.</returns>
        public async Task<IList<Appointment>> GetAppointments()
        {
            try
            {
                return await this.appointmentRepository.GetAppointments();
            }
            catch (Exception)
            {
                throw new BusinessException(ApiMedicMessages.ErrorGettingAppointments);
            }
        }

        /// <summary>
        /// Add appointment to database.
        /// </summary>
        /// <param name="appointment">Instance of appointment.</param>
        /// <returns>1 is correct.</returns>
        public async Task<int> AddAppointment(Appointment appointment)
        {
            if (appointment == null)
            {
                throw new BusinessException(ApiMedicMessages.ErrorNoData);
            }

            Doctor doctor = await this.doctorAdapter.GetDoctor(appointment.IdDoctor);
            if (doctor == null || doctor.Id == 0)
            {
                throw new BusinessException(ApiMedicMessages.ErrorDoctorDoesNotExists);
            }

            Patient patient = await this.patientAdapter.GetPatient(appointment.IdPatient);
            if (patient == null || patient.Id == 0)
            {
                throw new BusinessException(ApiMedicMessages.ErrorPatientDoesNotExists);
            }

            IList<Appointment> appointments = await this.appointmentRepository.GetAppointmentsInDateByDoctor(appointment);
            if (appointments.Count > 0)
            {
                throw new BusinessException(ApiMedicMessages.ErrorDoctorWithAppointmentSameDate);
            }

            return await this.appointmentRepository.AddAppointment(appointment);
        }

        /// <summary>
        /// Update an appointment.
        /// </summary>
        /// <param name="appointment">Instance of appointment.</param>
        /// <returns>1 is correct.</returns>
        public async Task<int> UpdateAppointment(Appointment appointment)
        {
            if (appointment == null)
            {
                throw new BusinessException(ApiMedicMessages.ErrorNoData);
            }

            Doctor doctor = await this.doctorAdapter.GetDoctor(appointment.IdDoctor);
            if (doctor == null || doctor.Id == 0)
            {
                throw new BusinessException(ApiMedicMessages.ErrorDoctorDoesNotExists);
            }

            Patient patient = await this.patientAdapter.GetPatient(appointment.IdPatient);
            if (patient == null || patient.Id == 0)
            {
                throw new BusinessException(ApiMedicMessages.ErrorPatientDoesNotExists);
            }

            Appointment appointmentExist = await this.appointmentRepository.GetAppointment(appointment.IdAppointment);
            if (appointmentExist != null)
            {
                return await this.appointmentRepository.UpdateAppointment(appointment);
            }
            else
            {
                throw new BusinessException(ApiMedicMessages.AppointmentDoesNotExists);
            }
        }

        /// <summary>
        /// Set active to false to an appointment.
        /// </summary>
        /// <param name="idAppointment">Id appointment.</param>
        /// <returns>1 is correct.</returns>
        public async Task<int> CancelAppointment(int idAppointment)
        {
            Appointment appointment = await this.appointmentRepository.GetAppointment(idAppointment);
            if (appointment != null)
            {
                appointment.Active = false;
                return await this.appointmentRepository.UpdateAppointment(appointment);
            }
            else
            {
                throw new BusinessException(ApiMedicMessages.AppointmentDoesNotExists);
            }
        }

        /// <summary>
        /// Delete an appointment from database.
        /// </summary>
        /// <param name="idAppointment">Id appointment to delete.</param>
        /// <returns>1 is correct.</returns>
        public async Task<int> DeleteAppointment(int idAppointment)
        {
            Appointment appointment = await this.appointmentRepository.GetAppointment(idAppointment);
            if (appointment != null)
            {
                return await this.appointmentRepository.DeleteAppointment(appointment);
            }
            else
            {
                throw new BusinessException(ApiMedicMessages.AppointmentDoesNotExists);
            }
        }
    }
}