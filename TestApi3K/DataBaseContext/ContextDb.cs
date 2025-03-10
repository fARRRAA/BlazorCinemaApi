using Microsoft.EntityFrameworkCore;
using CinemaDigestApi.Model;

namespace CinemaDigestApi.DataBaseContext
{
    public class ContextDb : DbContext
    {
        public ContextDb(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieChat> MovieChats { get; set; }
        public DbSet<UserChat> UserChats { get; set; }
        public DbSet<MovieChatMessage> MovieChatMessages { get; set; }
        public DbSet<UserChatMesaage> UserChatMessages { get; set; }
        public DbSet<UnityUser> UnityUsers { get; set; }
    }
}
