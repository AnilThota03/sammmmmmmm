using Microsoft.EntityFrameworkCore;
using PractiseVilla.Models;

namespace PractiseVilla.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Villa> Villas { get; set; }

        public DbSet<VillaNumber> VillaNumbers { get;set; }

       
    }
}
