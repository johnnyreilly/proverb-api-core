using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Proverb.Data.EntityFramework.Models;

namespace Proverb.Data.EntityFramework.CommandQuery
{
    public interface ISageQuery
    {
        Task<ICollection<User>> GetAllAsync();
        Task<ICollection<User>> GetAllWithSayingsAsync();
        Task<User> GetByIdAsync(int id);
        Task<User> GetByIdWithSayingsAsync(int id);
    }

    public class SageQuery : BaseCommandQuery, ISageQuery
    {
        public SageQuery(ProverbContext dbContext) : base(dbContext) { }

        public async Task<ICollection<User>> GetAllAsync()
        {
            var sages = await DbContext.User.ToListAsync();
            
            return sages;
        }

        public async Task<ICollection<User>> GetAllWithSayingsAsync()
        {
            var sagesWithSayings = await DbContext.User.Include(x => x.Saying).ToListAsync();
            
            return sagesWithSayings;
        }

        public async Task<User> GetByIdAsync(int id)
        {
            var sage = await DbContext.User.FindAsync(id);
            
            return sage;
        }

        public async Task<User> GetByIdWithSayingsAsync(int id)
        {
            var sageWithSayings = await DbContext.User
                .Include(x => x.Saying)
                .SingleOrDefaultAsync(x => x.Id == id);

            return sageWithSayings;
        }

        private IQueryable<User> GetSagesWithSayings()
        {
            var sageWithSayings = 
                from s in DbContext.User.Include(x => x.Saying)
                select s;

            return sageWithSayings;
        }
    }
}
