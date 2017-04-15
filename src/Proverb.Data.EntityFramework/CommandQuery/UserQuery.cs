using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Proverb.Data.EntityFramework.Models;

namespace Proverb.Data.EntityFramework.CommandQuery
{
    public interface IUserQuery
    {
        Task<ICollection<User>> GetAllAsync();
        Task<User> GetByIdAsync(int id);
        Task<User> GetByUserNameAsync(string userName);
    }

    public class UserQuery : BaseCommandQuery, IUserQuery
    {
        public UserQuery(ProverbContext dbContext) : base(dbContext) { }

        public async Task<ICollection<User>> GetAllAsync()
        {
            var users = await DbContext.User.ToListAsync();

            return users;
        }

        public async Task<User> GetByIdAsync(int id)
        {
            var user = await DbContext.User.FindAsync(id);

            return user;
        }

        public async Task<User> GetByUserNameAsync(string userName)
        {
            var user = await DbContext.User.SingleOrDefaultAsync(x => x.UserName == userName);

            return user;
        }
    }
}
