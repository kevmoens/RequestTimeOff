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
            try
            {
                Holidays.Add(holiday);
                SaveChanges();
                return true;
            } catch (Exception ex)
            {
                _logger.LogError(ex, "AddHoliday");
                return false;
            }
        }

        public bool AddTimeOff(TimeOff timeOff)
        {
            try
            {
                TimeOffs.Add(timeOff);
                SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "AddTimeOff");
                return false;
            }
        }

        public bool AddUser(User user)
        {
            try
            {
                Users.Add(user);
                SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "AddUser");
                return false;
            }
        }


        public List<Holiday> HolidayQuery(Func<Holiday, bool> expression)
        {
            return Holidays.Where(expression).ToList();
        }

        public bool RemoveHoliday(Holiday holiday)
        {
            try
            {
                Holidays.Remove(holiday);
                SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "RemoveHoliday");
                return false;
            }
        }

        public bool RemoveTimeOff(TimeOff timeOff)
        {
            try
            {
                TimeOffs.Remove(timeOff);
                SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "RemoveTimeOff");
                return false;
            }
        }

        public bool RemoveUser(User user)
        {
            try
            {
                Users.Remove(user);
                SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "RemoveUser");
                return false;
            }
        }

        public List<TimeOff> TimeOffQuery(Func<TimeOff, bool> expression)
        {
            return TimeOffs.Where(expression).ToList();
        }

        public bool UpdateHoliday(Holiday holiday)
        {
            try
            {
                Holidays.Update(holiday);
                SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "UpdateHoliday");
                return false;
            }
        }

        public bool UpdateTimeOff(TimeOff timeOff)
        {
            try
            {
                TimeOffs.Update(timeOff);
                SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "UpdateTimeOff");
                return false;
            }
        }

        public bool UpdateUser(User user)
        {
            try
            {
                Users.Update(user);
                SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "UpdateUser");
                return false;
            }
        }

        public List<User> UserQuery(Func<User, bool> expression)
        {
            return Users.Where(expression).ToList();
        }

        public bool AddDepartment(Department department)
        {

            try
            {
                Departments.Add(department);
                SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "AddDepartment");
                return false;
            }
        }


        public bool UpdateDepartment(Department department)
        {

            try
            {
                Departments.Update(department);
                SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "UpdateDepartment");
                return false;
            }
        }
        public List<Department> DepartmentQuery(Func<Department, bool> expression)
        {
            return Departments.Where(expression).ToList();
        }
        public bool RemoveDepartment(Department department)
        {

            try
            {
                Departments.Remove(department);
                SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "RemoveDepartment");
                return false;
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string path = @"TimeOff.sqlite";
            optionsBuilder.UseSqlite(
                $"data source={path}");
        }
    }
}
