using System;
using Microsoft.EntityFrameworkCore;
using Web_Framework.Models;

namespace Web_Framework.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> dbContext) : base(dbContext)
        {

        }

        public DbSet<Employee> Employee { get; set; }
    }
}
