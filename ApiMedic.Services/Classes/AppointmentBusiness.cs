// -------------------------------------------------------------------------------
// <copyright file="AppointmentBusiness.cs">
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
        private readonly IAppointmentRepository appointmentRepository = null;
        private readonly IDoctorAdapter doctorAdapter = null;
        private readonly IPatientAdapter patientAdapter = null;

        /// <summary>
        /// Class Constructor.
        /// </summary>
        public AppointmentBusiness()
        {
            this.appointmentRepository = new AppointmentRepository();
            this.doctorAdapter = new DoctorAdapter();
            this.patientAdapter = new PatientAdapter();
        }

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
        /// <returns></returns>
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