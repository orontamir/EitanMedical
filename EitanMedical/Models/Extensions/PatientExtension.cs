using Base.Models;
using EitanMedical.DAL.SQL.Entities;

namespace EitanMedical.Models.Extensions
{
    public static class PatientExtension
    {
        public static PatientEntity ToEntity(this Patient s)
        {
            if (s == null)
                return new PatientEntity();
            return new PatientEntity
            {
                Id = int.Parse(s.Id),
                Name = s.Name,
                Age = s.Age,
                Gender = s.Gender,
                Request = s.Request 
            };
        }

        public static Patient ToModel(this PatientEntity s)
        {

            return new Patient
            {
                Id = s.Id.ToString(),
                Name = s.Name,
                Age = s.Age,
                Gender = s.Gender,
                Request = s.Request
            };


        }
    }
}
