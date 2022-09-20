using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace EntityManyToManyProject
{
    class Doctor
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<Patient> Patients { set; get; } = new();
        public List<Service> Services { set; get; } = new();
    }

    class Patient
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<Doctor> Doctors { set; get; } = new();
        public List<Service> Services { set; get; } = new();
    }

    class Service
    {
        public int DoctorId { get; set; }
        public Doctor? Doctors { get; set; }

        public int PatientId { get; set; }
        public Patient? Patients { get; set; }

        public int Room { get; set; }
    }
    class AmbulanceContext : DbContext
    {
        public DbSet<Doctor> Doctors { get; set; } = null!;
        public DbSet<Patient> Patients { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Initial Catalog=UsersDb;Integrated Security=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Doctor>()
                        .HasMany(d => d.Patients)
                        .WithMany(p => p.Doctors)
                        
                        //.UsingEntity<Service>
                        //(
                        //    s => s.HasOne(dp => dp.Doctors)
                        //            .WithMany(d => d.Services)
                        //            .HasForeignKey(dp => dp.DoctorId),
                        //    s => s.HasOne(dp => dp.Patients)
                        //            .WithMany(p => p.Services)
                        //            .HasForeignKey(dp => dp.PatientId),
                        //    s =>
                        //    {
                        //        s.Property(dp => dp.Room).HasDefaultValue(5);
                        //        s.HasKey(k => new { k.DoctorId, k.PatientId });
                        //        s.ToTable("Registration");
                        //    }
                        //);
        }
    }
}
