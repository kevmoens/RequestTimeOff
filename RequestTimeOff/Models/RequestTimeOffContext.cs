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
        private readonly SemaphoreSlim _lock = new SemaphoreSlim(1,1);
        private readonly ILogger<RequestTimeOffContext> _logger;
        public RequestTimeOffContext(DbContextOptions<RequestTimeOffContext> options, ILogger<RequestTimeOffContext> logger)
            : base(options)
        {
            Database.Migrate();
            _logger = logger; 
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<TimeOff> TimeOffs { get; set; }
        public DbSet<Holiday> Holidays { get; set; }
        
        public bool AddHoliday(Holiday holiday)
        {
            _logger.LogTrace("AddHoliday START");
            _lock.Wait();
            try
            {
                Holidays.Add(holiday);
                SaveChanges();
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "AddHoliday");
                throw;
            }
            finally { _lock.Release(); }
            _logger.LogTrace("AddHoliday END");
            return true;
        }

        public bool AddTimeOff(TimeOff timeOff)
        {
            _logger.LogTrace("AddTimeOff START");
            _lock.Wait();
            try
            {
                TimeOffs.Add(timeOff);
                SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "AddTimeOff");
                throw;
            }
            finally { _lock.Release(); }
            _logger.LogTrace("AddTimeOff END");
            return true;
        }

        public bool AddUser(User user)
        {
            _logger.LogTrace("AddUser START");
            _lock.Wait();
            try
            {
                Users.Add(user);
                SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "AddUser");
                throw;
            }
            finally { _lock.Release(); }
            _logger.LogTrace("AddUser END");
            return true;
        }


        public List<Holiday> HolidayQuery(Func<Holiday, bool> expression)
        {
            _logger.LogTrace("HolidayQuery START");
            _lock.Wait();
            try
            {
                return Holidays.Where(expression).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "HolidayQuery");
                throw;
            }
            finally 
            { 
                _lock.Release(); 
                _logger.LogTrace("HolidayQuery END");
            }
        }

        public bool RemoveHoliday(Holiday holiday)
        {
            _logger.LogTrace("RemoveHoliday START");
            _lock.Wait();
            try
            {
                Holidays.Remove(holiday);
                SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "RemoveHoliday");
                throw;
            }
            finally { _lock.Release(); }
            _logger.LogTrace("RemoveHoliday END");
            return true;
        }

        public bool RemoveTimeOff(TimeOff timeOff)
        {
            _logger.LogTrace("RemoveTimeOff START");
            _lock.Wait();
            try
            {
                TimeOffs.Remove(timeOff);
                SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "RemoveTimeOff");
                throw;
            }
            finally { _lock.Release(); }
            _logger.LogTrace("RemoveTimeOff END");
            return true;
        }

        public bool RemoveUser(User user)
        {
            _logger.LogTrace("RemoveUser START");
            _lock.Wait();
            try
            {
                Users.Remove(user);
                SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "RemoveUser");
                throw;
            }
            finally { _lock.Release(); }
            _logger.LogTrace("RemoveUser END");
            return true;
        }

        public List<TimeOff> TimeOffQuery(Func<TimeOff, bool> expression)
        {
            _logger.LogTrace("TimeOffQuery START");
            _lock.Wait();
            try
            {
                return TimeOffs.Where(expression).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "TimeOffQuery");
                throw;
            }
            finally 
            { 
                _lock.Release();
                _logger.LogTrace("TimeOffQuery END");
            }
        }

        public bool UpdateHoliday(Holiday holiday)
        {
            _logger.LogTrace("UpdateHoliday START");
            _lock.Wait();
            try
            {
                Holidays.Update(holiday);
                SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "UpdateHoliday");
                throw;
            }
            finally { _lock.Release(); }

            _logger.LogTrace("UpdateHoliday END");
            return true;
        }

        public bool UpdateTimeOff(TimeOff timeOff)
        {
            _logger.LogTrace("UpdateTimeOff START");
            _lock.Wait();
            try
            {
                TimeOffs.Update(timeOff);
                SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "UpdateTimeOff");
                throw;
            }
            finally { _lock.Release(); }
            _logger.LogTrace("UpdateTimeOff END");
            return true;
        }

        public bool UpdateUser(User user)
        {
            _logger.LogTrace("UpdateUser START");
            _lock.Wait();
            try
            {
                Users.Update(user);
                SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "UpdateUser");
                throw;
            }
            finally { _lock.Release(); }
            _logger.LogTrace("UpdateUser END");
            return true;
        }

        public List<User> UserQuery(Func<User, bool> expression)
        {
            _logger.LogTrace("UserQuery START");
            _lock.Wait();
            try
            {
                return Users.Where(expression).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "UserQuery");
                throw;
            }
            finally 
            { 
                _lock.Release();
                _logger.LogTrace("UserQuery END");
            }
        }

        public bool AddDepartment(Department department)
        {
            _logger.LogTrace("AddDepartment START");
            _lock.Wait();
            try
            {
                Departments.Add(department);
                SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "AddDepartment");
                throw;
            }
            finally
            { _lock.Release(); }
            _logger.LogTrace("AddDepartment END");
            return true;
        }


        public bool UpdateDepartment(Department department)
        {
            _logger.LogTrace("UpdateDepartment START");
            _lock.Wait();
            try
            {
                Departments.Update(department);
                SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "UpdateDepartment");
                throw;
            }
            finally
            { 
                _lock.Release();
                _logger.LogTrace("UpdateDepartment END");
            }
            return true;
        }
        public List<Department> DepartmentQuery(Func<Department, bool> expression)
        {
            _logger.LogTrace("DepartmentQuery START");
            _lock.Wait();
            try
            {
                return Departments.Where(expression).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "DepartmentQuery");
                throw;
            }
            finally
            { 
                _lock.Release();
                _logger.LogTrace("DepartmentQuery END");
            }
        }
        public bool RemoveDepartment(Department department)
        {
            _logger.LogTrace("RemoveDepartment START");
            _lock.Wait();
            try
            {
                Departments.Remove(department);
                SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "RemoveDepartment");
                throw;
            }
            finally
            { _lock.Release(); }
            _logger.LogTrace("RemoveDepartment END");
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
