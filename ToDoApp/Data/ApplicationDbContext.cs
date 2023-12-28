using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Modles;

namespace ToDoApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        { }

        
        public virtual DbSet<ItemData> Items { get; set; }
    
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }
    }
}
