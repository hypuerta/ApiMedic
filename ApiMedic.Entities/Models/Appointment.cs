// -------------------------------------------------------------------------------
// <copyright file="Appointment.cs" company="ApiMedic.Api">
// ApiMedic.Api
// </copyright>
// <author>Herley Puerta</author>
// <email>hypuerta@hotmail.com</email>
// <date>13/02/2018</date>
// <summary>Model Appointment.</summary>
// -------------------------------------------------------------------------------
namespace ApiMedic.Entities.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Model Appointment.
    /// </summary>
    public class Appointment
    {
        /// <summary>
        /// Gets or sets IdAppointment.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int IdAppointment { get; set; }

        /// <summary>
        /// Gets or sets IdDoctor.
        /// </summary>
        public virtual int IdDoctor { get; set; }

        /// <summary>
        /// Gets or sets IdPatient.
        /// </summary>
        public virtual int IdPatient { get; set; }

        /// <summary>
        /// Gets or sets Active.
        /// </summary>
        public virtual bool Active { get; set; }

        /// <summary>
        /// Gets or sets StartDate.
        /// </summary>
        public virtual DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets EndDate.
        /// </summary>
        public virtual DateTime EndDate { get; set; }
    }
}