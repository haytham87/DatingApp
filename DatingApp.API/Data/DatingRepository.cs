using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
    public class DatingRepository : IDatingRepository
    {
        private readonly DataContext _context;
        public DatingRepository(DataContext context)
        {
            _context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<User> GetUser(int UserID)
        {
            var user = await _context.Users.Include(p=> p.Photos).FirstOrDefaultAsync(u => u.UserID==UserID);
            return user;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = await _context.Users.Include(p=> p.Photos).ToListAsync();
            return users;
        }

        public async Task<bool> SaveAll()
        {
           return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Photo> GetPhoto(int iD)
        {
            var photo = await _context.photos.FirstOrDefaultAsync(u => u.ID==iD);
            return photo;
        }

        public async Task<Photo> GetMainPhotoForUser(int userID)
        {
            return await _context.photos.Where(u => u.UserID==userID).FirstOrDefaultAsync(p=>p.IsMain);
        }
    }
}