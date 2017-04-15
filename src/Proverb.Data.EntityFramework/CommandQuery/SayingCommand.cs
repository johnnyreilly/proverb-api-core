using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Proverb.Data.EntityFramework.Models;

namespace Proverb.Data.EntityFramework.CommandQuery
{
    public interface ISayingCommand
    {
        Task<int> CreateAsync(Saying saying);
        Task DeleteAsync(int id);
        Task UpdateAsync(Saying saying);
    }
    
    public class SayingCommand : BaseCommandQuery, ISayingCommand
    {
        public SayingCommand(ProverbContext dbContext) : base(dbContext) { }

        public async Task<int> CreateAsync(Saying saying)
        {
            DbContext.Saying.Add(saying);

            await DbContext.SaveChangesAsync();

            return saying.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var userToDelete = await DbContext.Saying.FindAsync(id);

            DbContext.Saying.Remove(userToDelete);

            await DbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Saying saying)
        {
            DbContext.Entry(saying).State = EntityState.Modified;

            await DbContext.SaveChangesAsync();
        }
    }
}
