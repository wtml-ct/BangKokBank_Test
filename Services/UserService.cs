using BangKokBank.Models;
using BangKokBank.Data;
using Microsoft.EntityFrameworkCore;

namespace BangKokBank.Services
{
    public class UserService
    {
        private readonly AppDbContext _dbcontext;

        public UserService(AppDbContext context)
        {
            _dbcontext = context;
        }

        public async Task InitializeAsync()
        {
            if (await _dbcontext.Users.AnyAsync()) return;

            var sampleUsers = new List<User>
            {
                new User { Name = "Leanne Graham", Username = "Bret", Email = "Sincere@april.biz", Phone = "1-770-736-8031 x56442", Website = "hildegard.org" },
                new User { Name = "Ervin Howell", Username = "Antonette", Email = "Shanna@melissa.tv", Phone = "010-692-6593 x09125", Website = "anastasia.net" },
                new User { Name = "Clementine Bauch", Username = "Samantha", Email = "Nathan@yesenia.net", Phone = "1-463-123-4447", Website = "ramiro.info" },
                new User { Name = "Patricia Lebsack", Username = "Karianne", Email = "Julianne.OConner@kory.org", Phone = "493-170-9623 x156", Website = "kale.biz" },
                new User { Name = "Chelsey Dietrich", Username = "Kamren", Email = "Lucio_Hettinger@annie.ca", Phone = "(254)954-1289", Website = "demarco.info" }
            };

            _dbcontext.Users.AddRange(sampleUsers);
            await _dbcontext.SaveChangesAsync();
        }


        // get All
        public async Task<List<User>> GetAllAsync()
        {
            return await _dbcontext.Users.ToListAsync();
        }

        // get by ID 
        public async Task<User?> GetByIdAsync(long userid)
        {
            return await _dbcontext.Users
                                   .Where(x => x.Id == userid)
                                   .FirstOrDefaultAsync();
        }

        // Create
        public async Task<User> CreateAsync(User user)
        {
            _dbcontext.Users.Add(user);
            await _dbcontext.SaveChangesAsync();
            return user;
        }

        // Update
        public async Task<bool> UpdateAsync(long userid, User updatedUser)
        {
            var user = await _dbcontext.Users
                                       .Where(x => x.Id == userid)
                                       .FirstOrDefaultAsync();
            if (user == null) return false;

            user.Name = updatedUser.Name;
            user.Username = updatedUser.Username;
            user.Email = updatedUser.Email;
            user.Phone = updatedUser.Phone;
            user.Website = updatedUser.Website;

            await _dbcontext.SaveChangesAsync();
            return true;
        }

        // Delete 
        public async Task<bool> DeleteAsync(long userid)
        {
            var user = await _dbcontext.Users
                                       .Where(x => x.Id == userid)
                                       .FirstOrDefaultAsync();
            if (user == null) return false;

            _dbcontext.Users.Remove(user);
            await _dbcontext.SaveChangesAsync();
            return true;
        }
    }
}
