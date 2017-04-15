using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Proverb.Data.EntityFramework.Models;

namespace Proverb.Data.EntityFramework.CommandQuery
{
    public interface ISageCommand
    {
        Task<int> CreateAsync(User sage);
        Task DeleteAsync(int id);
        Task UpdateAsync(User sage);
    }

    public class SageCommand : BaseCommandQuery, ISageCommand
    {
        public SageCommand(ProverbContext dbContext) : base(dbContext) { }

        public async Task<int> CreateAsync(User sage)
        {
            DbContext.User.Add(sage);

            await DbContext.SaveChangesAsync();

            return sage.Id;
        }

        public async Task DeleteAsync(int id) 
        {
            var userToDelete = await DbContext.User.FindAsync(id);
            
            DbContext.User.Remove(userToDelete);

            await DbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(User sage)
        {
            DbContext.Entry(sage).State = EntityState.Modified;

            await DbContext.SaveChangesAsync();
        }
    }
}
