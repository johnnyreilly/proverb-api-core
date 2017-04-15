using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Proverb.Data.EntityFramework.Models;

namespace Proverb.Data.EntityFramework.CommandQuery
{
    public interface IUserCommand
    {
        Task<int> CreateAsync(User user);
        Task DeleteAsync(int id);
        Task UpdateAsync(User user);
    }

    public class UserCommand : BaseCommandQuery, IUserCommand
    {
        public UserCommand(ProverbContext dbContext) : base(dbContext) { }

        public async Task<int> CreateAsync(User user)
        {
            DbContext.User.Add(user);

            await DbContext.SaveChangesAsync();

            return user.Id;
        }

        public async Task DeleteAsync(int id) 
        {
            var userToDelete = await DbContext.User.FindAsync(id);
            
            DbContext.User.Remove(userToDelete);

            await DbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            DbContext.Entry(user).State = EntityState.Modified;

            await DbContext.SaveChangesAsync();
        }
    }
}
