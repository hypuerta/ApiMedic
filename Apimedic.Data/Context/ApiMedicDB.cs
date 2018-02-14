// -------------------------------------------------------------------------------
// <copyright file="ApiMedicDB.cs" company="ApiMedic.Api">
// ApiMedic.Api
// </copyright>
// <author>Herley Puerta</author>
// <email>hypuerta@hotmail.com</email>
// <date>14/02/2018</date>
// <summary>Entity framework context.</summary>
// -------------------------------------------------------------------------------
namespace ApiMedic.Data.Context
{
    using System.Data.Entity;
    using Entities.Models;

    /// <summary>
    /// Entity framework context.
    /// </summary>
    public class ApiMedicDB : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the ApiMedicDB class.
        /// </summary>
        public ApiMedicDB() : base("name=ApiMedicDB")
        {
        }

        /// <summary>
        /// Gets or sets Appointment Entity.
        /// </summary>
        public DbSet<Appointment> Appointments { get; set; }

        /// <summary>
        /// Exetutes on model creating.
        /// </summary>
        /// <param name="modelBuilder">Model builder.</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<ApiMedicDB>(null);
            base.OnModelCreating(modelBuilder);
        }
    }
}