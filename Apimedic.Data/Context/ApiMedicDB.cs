namespace ApiMedic.Data.Context
{
    using System.Data.Entity;
    using Entities.Models;

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