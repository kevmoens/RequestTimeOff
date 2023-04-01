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

        public bool AddHoliday(Holiday holiday)
        {
            try
            {
                Holidays.Add(holiday);
                SaveChanges();
                return true;
            } catch (Exception ex)
            {
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
                return false;
            }
        }

        public List<Holiday> HolidayQuery(Expression<Func<Holiday, bool>> expression)
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
                return false;
            }
        }

        public List<TimeOff> TimeOffQuery(Expression<Func<TimeOff, bool>> expression)
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
                return false;
            }
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
