using System;
using Base.Services;
using EitanMedical.DAL.SQL.Entities;
using EitanMedical.Services;
using Microsoft.EntityFrameworkCore;
namespace EitanMedical.DAL.SQL
{
    public abstract class RepositoryBase
    {
        protected IConfiguration _configuration;
        public RepositoryBase(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected virtual AppDbContext? CreateDbContext()
        {
            try
            {
                var connectionString = _configuration["DB_CONNECTION_STRING"];
                var serverVersion = new MySqlServerVersion(new Version(8, 0, 17));
                var options = new DbContextOptionsBuilder<AppDbContext>()
                    .UseMySql(connectionString, serverVersion,
                    mySqlOptions => mySqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 100,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null
                        )

                    )
                    .Options;
                return new AppDbContext(options);
            }
            catch (Exception ex)
            {
                LogService.LogError($"Exception when Create Db Context, Error messsage : {ex.Message}");
                return null;
            }
        }

        public AppDbContext GetDbCtx() => CreateDbContext();
        public async Task CreateOrUpdateAsync<TEntity>(TEntity entity) where TEntity : class
        {
            await using var ctx = GetDbCtx();
            var table = ctx.Set<TEntity>();
            if (await table.ContainsAsync(entity))
            {
                table.Update(entity);
            }
            else
            {
                await table.AddAsync(entity);
            }

            await ctx.SaveChangesAsync();
        }

        public async Task UpdateAsync<TEntity>(TEntity entity) where TEntity : class
        {
            await using var ctx = GetDbCtx();
            var table = ctx.Set<TEntity>();
            table.Update(entity);
            await ctx.SaveChangesAsync();

        }

        public async Task DeleteAsync<TEntity>(TEntity id) where TEntity : class
        {
            await using var ctx = GetDbCtx();
            var table = ctx.Set<TEntity>();
            table.Remove(id);
            await ctx.SaveChangesAsync();
        }

        public virtual async Task<List<PatientEntity>> GetAllPatients()
        {
            await using var ctx = GetDbCtx();
            return await ctx.Patients.ToListAsync();
        }

        public virtual async Task<List<HeartRateReadingEntity>> GetAllHeartRateReadings()
        {
            await using var ctx = GetDbCtx();
            return await ctx.HeartRateReadings.ToListAsync();
        }

        public async Task<PatientEntity?> GetPatientById(string id)
        {
            await using var ctx = GetDbCtx();
            return ctx.Patients.FirstOrDefault(u => u.Id == int.Parse(id));
        }

        public async Task<HeartRateReadingEntity> AddHeartRateReading(HeartRateReadingEntity entity)
        {
            await using var ctx = GetDbCtx();

            ctx.HeartRateReadings.Add(entity);
            await ctx.SaveChangesAsync();
            return entity;
        }

        public async Task<PatientEntity> AddPatient(PatientEntity entity)
        {
            await using var ctx = GetDbCtx();

            ctx.Patients.Add(entity);
            await ctx.SaveChangesAsync();
            return entity;
        }

        public  async Task<PatientEntity> UpdatePatientRequest(PatientEntity entity)
        {
            await using var ctx = GetDbCtx();
            ctx.Patients.Update(entity);
            await ctx.SaveChangesAsync();
            return entity;
        }

    }
}
