using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RequestTimeOff.Models
{
    public class RequestTimeOffContext : DbContext, IRequestTimeOffRepository
    {

        public RequestTimeOffContext(DbContextOptions<RequestTimeOffContext> options,ILogger<RequestTimeOffContext> logger)
            : base(options)
        {
            _logger = logger;
            Database.Migrate();
        }
        private readonly ILogger<RequestTimeOffContext> _logger;

        public DbSet<Department> Departments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<TimeOff> TimeOffs { get; set; }
        public DbSet<Holiday> Holidays { get; set; }
        
        public bool AddHoliday(Holiday holiday)
        {
            Holidays.Add(holiday);
            SaveChanges();
            return true;
        }

        public bool AddTimeOff(TimeOff timeOff)
        {
            TimeOffs.Add(timeOff);
            SaveChanges();
            return true;
        }

        public bool AddUser(User user)
        {
            Users.Add(user);
            SaveChanges();
            return true;
        }


        public List<Holiday> HolidayQuery(Func<Holiday, bool> expression)
        {
            return Holidays.Where(expression).ToList();
        }

        public bool RemoveHoliday(Holiday holiday)
        {
            Holidays.Remove(holiday);
            SaveChanges();
            return true;
        }

        public bool RemoveTimeOff(TimeOff timeOff)
        {
            TimeOffs.Remove(timeOff);
            SaveChanges();
            return true;
        }

        public bool RemoveUser(User user)
        {
            Users.Remove(user);
            SaveChanges();
            return true;
        }

        public List<TimeOff> TimeOffQuery(Func<TimeOff, bool> expression)
        {
            return TimeOffs.Where(expression).ToList();
        }

        public bool UpdateHoliday(Holiday holiday)
        {
            Holidays.Update(holiday);
            SaveChanges();
            return true;
        }

        public bool UpdateTimeOff(TimeOff timeOff)
        {
            TimeOffs.Update(timeOff);
            SaveChanges();
            return true;
        }

        public bool UpdateUser(User user)
        {
            Users.Update(user);
            SaveChanges();
            return true;
        }

        public List<User> UserQuery(Func<User, bool> expression)
        {
            return Users.Where(expression).ToList();
        }

        public bool AddDepartment(Department department)
        {
            Departments.Add(department);
            SaveChanges();
            return true;
        }


        public bool UpdateDepartment(Department department)
        {
            Departments.Update(department);
            SaveChanges();
            return true;
        }
        public List<Department> DepartmentQuery(Func<Department, bool> expression)
        {
            return Departments.Where(expression).ToList();
        }
        public bool RemoveDepartment(Department department)
        {
            Departments.Remove(department);
            SaveChanges();
            return true;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string path = @"TimeOff.sqlite";
            optionsBuilder.UseSqlite(
                $"data source={path}");
        }
    }
}
