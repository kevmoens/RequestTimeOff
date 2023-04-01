using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RequestTimeOff.Models
{
    public class RequestTimeOffContext : DbContext, IRequestTimeOffRepository
    {

        public RequestTimeOffContext(DbContextOptions<RequestTimeOffContext> options)
            : base(options)
        {
            Database.Migrate();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<TimeOff> TimeOffs { get; set; }
        public DbSet<Holiday> Holidays { get; set; }

        public List<Holiday> HolidayQuery(Expression<Func<Holiday, bool>> expression)
        {
            return Holidays.Where(expression).ToList();
        }
        public List<TimeOff> TimeOffQuery(Expression<Func<TimeOff, bool>> expression)
        {
            return TimeOffs.Where(expression).ToList();
        }
        public List<User> UserQuery(Expression<Func<User, bool>> expression)
        {
            return Users.Where(expression).ToList();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string path = @"TimeOff.sqlite";
            optionsBuilder.UseSqlite(
                $"data source={path}");
        }
    }
}
