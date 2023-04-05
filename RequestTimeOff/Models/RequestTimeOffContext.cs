using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RequestTimeOff.Views;
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
        private readonly SemaphoreSlim _departmentLock = new SemaphoreSlim(1);
        private readonly SemaphoreSlim _userLock = new SemaphoreSlim(1);
        private readonly SemaphoreSlim _timeoffLock = new SemaphoreSlim(1);
        private readonly SemaphoreSlim _holidayLock = new SemaphoreSlim(1);
        public RequestTimeOffContext(DbContextOptions<RequestTimeOffContext> options)
            : base(options)
        {
            Database.Migrate();
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<TimeOff> TimeOffs { get; set; }
        public DbSet<Holiday> Holidays { get; set; }
        
        public bool AddHoliday(Holiday holiday)
        {
            _holidayLock.Wait();
            try
            {
                Holidays.Add(holiday);
                SaveChanges();
            }
            catch
            {
                throw;
            }
            finally { _holidayLock.Release(); }
            return true;
        }

        public bool AddTimeOff(TimeOff timeOff)
        {
            _timeoffLock.Wait();
            try
            {
                TimeOffs.Add(timeOff);
                SaveChanges();
            }
            catch
            {
                throw;
            }
            finally { _timeoffLock.Release(); }
            return true;
        }

        public bool AddUser(User user)
        {
            _userLock.Wait();
            try
            {
                Users.Add(user);
                SaveChanges();
            }
            catch
            {
                throw;
            }
            finally { _userLock.Release(); }
            return true;
        }


        public List<Holiday> HolidayQuery(Func<Holiday, bool> expression)
        {
            _holidayLock.Wait();
            try
            {
                return Holidays.Where(expression).ToList();
            }
            catch
            {
                throw;
            }
            finally { _holidayLock.Release(); }
        }

        public bool RemoveHoliday(Holiday holiday)
        {
            _holidayLock.Wait();
            try
            {
                Holidays.Remove(holiday);
                SaveChanges();
            }
            catch
            {
                throw;
            }
            finally { _holidayLock.Release(); }
            return true;
        }

        public bool RemoveTimeOff(TimeOff timeOff)
        {
            _timeoffLock.Wait();
            try
            {
                TimeOffs.Remove(timeOff);
                SaveChanges();
            }
            catch
            {
                throw;
            }
            finally { _timeoffLock.Release(); }
            return true;
        }

        public bool RemoveUser(User user)
        {
            _userLock.Wait();
            try
            {
                Users.Remove(user);
                SaveChanges();
            }
            catch
            {
                throw;
            }
            finally { _userLock.Release(); }
            return true;
        }

        public List<TimeOff> TimeOffQuery(Func<TimeOff, bool> expression)
        {
            _timeoffLock.Wait();
            try
            {
                return TimeOffs.Where(expression).ToList();
            }
            catch
            {
                throw;
            }
            finally { _timeoffLock.Release(); }
        }

        public bool UpdateHoliday(Holiday holiday)
        {
            _holidayLock.Wait();
            try
            {
                Holidays.Update(holiday);
                SaveChanges();
            }
            catch
            {
                throw;
            }
            finally { _holidayLock.Release(); }
       
            return true;
        }

        public bool UpdateTimeOff(TimeOff timeOff)
        {
            _timeoffLock.Wait();
            try
            {
                TimeOffs.Update(timeOff);
                SaveChanges();
            }
            catch
            {
                throw;
            }
            finally { _timeoffLock.Release(); }
            return true;
        }

        public bool UpdateUser(User user)
        {
            _userLock.Wait();
            try
            {
                Users.Update(user);
                SaveChanges();
            }
            catch
            {
                throw;
            }
            finally { _userLock.Release(); }
            return true;
        }

        public List<User> UserQuery(Func<User, bool> expression)
        {
            _userLock.Wait();
            try
            {
                return Users.Where(expression).ToList();
            }
            catch
            {
                throw;
            }
            finally { _userLock.Release(); }
        }

        public bool AddDepartment(Department department)
        {
            _departmentLock.Wait();
            try
            {
                Departments.Add(department);
                SaveChanges();
            }
            catch
            {
                throw;
            }
            finally
            { _departmentLock.Release(); }
            return true;
        }


        public bool UpdateDepartment(Department department)
        {
            _departmentLock.Wait();
            try
            {
                Departments.Update(department);
                SaveChanges();
            }
            catch
            {
                throw;
            }
            finally
            { _departmentLock.Release(); }
            return true;
        }
        public List<Department> DepartmentQuery(Func<Department, bool> expression)
        {
            _departmentLock.Wait();
            try
            {
                return Departments.Where(expression).ToList();
            }
            catch
            {
                throw;
            }
            finally
            { _departmentLock.Release(); }
        }
        public bool RemoveDepartment(Department department)
        {
            _departmentLock.Wait();
            try
            {
                Departments.Remove(department);
                SaveChanges();
            }
            catch
            {
                throw;
            }
            finally
            { _departmentLock.Release(); }
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
