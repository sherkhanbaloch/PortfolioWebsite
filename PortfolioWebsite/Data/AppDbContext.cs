using Microsoft.EntityFrameworkCore;
using PortfolioWebsite.Models;

namespace PortfolioWebsite.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Tbl_Category { get; set; }
        public DbSet<Contact> Tbl_Contact { get; set; }
        public DbSet<PersonalInfo> Tbl_PersonalInfo { get; set; }
        public DbSet<Project> Tbl_Project { get; set; }
        public DbSet<Service> Tbl_Service { get; set; }
    }
}
