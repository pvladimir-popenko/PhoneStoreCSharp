using MvcApplicationCSharp5.Models;

namespace MvcApplicationCSharp5.Repositories
{
    public interface ICompanyRepository : IRepository<Company>
    {

    }

    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        public CompanyRepository(ApplicationDbContext context) : base(context)
        {
            DbSet = context.Companies;
        }
    }
}
