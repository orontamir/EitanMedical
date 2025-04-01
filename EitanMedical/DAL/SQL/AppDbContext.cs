using EitanMedical.DAL.SQL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace EitanMedical.DAL.SQL
{
    public class AppDbContext : DbContext
    {
        public DbSet<HeartRateReadingEntity> HeartRateReadings { get; set; }

        public DbSet<PatientEntity> Patients { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
    }
}
