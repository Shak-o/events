using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcademyApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AcademyApp.Data.Context
{
    public class AccountContext : IdentityDbContext <UserIdentity>
    {
        private readonly string _connectionString;

        public AccountContext(string connectionString)
        {
            _connectionString = connectionString;
        }
        public AccountContext()
        {

        }
        public AccountContext(DbContextOptions<AccountContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AccountContext).Assembly);
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Seed();
        }
    }
}
