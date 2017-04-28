using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Proverb.Data.EntityFramework.Models;

namespace Proverb.Data.EntityFramework.CommandQuery
{
    public interface ISayingQuery
    {
        Task<ICollection<Saying>> GetAllAsync();
        Task<Saying> GetByIdAsync(int id);
        Task<ICollection<Saying>> GetBySageIdAsync(int sageId);
    }

    public class SayingQuery : BaseCommandQuery, ISayingQuery
    {
        public SayingQuery(ProverbContext dbContext) : base(dbContext) { }

        public async Task<ICollection<Saying>> GetAllAsync()
        {
            var sayings = await DbContext.Saying
                .Include(saying => saying.Sage)
                .ToListAsync();

            return sayings;
        }

        public async Task<Saying> GetByIdAsync(int id)
        {
            var sayings = await DbContext.Saying
                .Include(saying => saying.Sage)
                .SingleAsync(saying => saying.Id == id);

            return sayings;
        }

        public async Task<ICollection<Saying>> GetBySageIdAsync(int sageId)
        {
            var sayings = await DbContext.Saying.Where(x => x.SageId == sageId).ToListAsync();

            return sayings;
        }
    }
}
