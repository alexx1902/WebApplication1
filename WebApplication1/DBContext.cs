using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1
{
  
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }
        public DbSet<ValueModel> Value { get; set; }
        public DbSet<ResultModel> Results { get; set; }

       
    }
}
