using EitanMedical.DAL.SQL;

namespace EitanMedical.Services
{
    public abstract class DALService
    {
        protected RepositoryBase Repository { get; }

        protected DALService(RepositoryBase repo)
        {
            Repository = repo;
        }
    }
}
