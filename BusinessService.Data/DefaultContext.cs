using BusinessService.Data.DBModel;
using Microsoft.EntityFrameworkCore;

namespace BusinessService.Data
{
    public class DefaultContext : DbContext
    {
        public DefaultContext(DbContextOptions<DefaultContext> options) : base(options)
        {
        }

        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<School> Schools { get; set; }


    }
}