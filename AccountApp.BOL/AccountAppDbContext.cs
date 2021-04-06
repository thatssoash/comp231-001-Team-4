using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccountApp.BOL
{
    public class AccountAppDbContext:DbContext
    {
        public AccountAppDbContext()
        {

        }
        public AccountAppDbContext(DbContextOptions<AccountAppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=accountappdb.database.windows.net;Database=AccountAppDB;User Id=accountappdbAdmin;password=Account@123;Trusted_Connection=False;");
        }

        public DbSet<Business> Businesses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<CustomForm> CustomForms { get; set; }
        public DbSet<UserRoles> UserRoles { get; set; }
    }
}
