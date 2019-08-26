using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MvcApplicationCSharp5.Models;

namespace MvcApplicationCSharp5.Repositories
{
    public interface IPhoneRepository : IRepository<Phone>
    {
        IEnumerable<Phone> GetAllIncludeCompanies();
    }

    public class PhoneRepository : Repository<Phone>, IPhoneRepository
    {
        public PhoneRepository(ApplicationDbContext context) : base(context)
        {
            DbSet = context.Phones;
        }

       public IEnumerable<Phone> GetAllIncludeCompanies()
        {
            return DbSet
                .Include(p => p.Company)
                .ToList();
        }

        public override Phone GetById(int id)
        {
            return DbSet
                .Include(p => p.Company)
                .FirstOrDefault(p => p.Id == id);
        }
    }
}
