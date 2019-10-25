using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using Transavia.Core.Entities;
using Transavia.Core.Enums;
using Transavia.Core.Interfaces;

namespace Transavia.Infrastructure.ContextDB
{
    public class TestContext : IdentityDbContext<User>
    {

        public TestContext(DbContextOptions<TestContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}