namespace ApiMedic.Api.Models
{
    using Entities.Models;
    using System.Data.Entity;

    public class ApiMedicDB : DbContext
    {
        public ApiMedicDB() : base("name=ApiMedicDB")
        {
        }

        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<ApiMedicDB>(null);
            base.OnModelCreating(modelBuilder);
        }
    }
}