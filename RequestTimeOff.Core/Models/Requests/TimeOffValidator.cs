using FluentValidation;
using RequestTimeOff.Models;
using RequestTimeOff.Models.Date;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;

namespace RequestTimeOff.Core.Models.Requests
{
    public class TimeOffValidator : AbstractValidator<TimeOff>, ITimeOffValidator
    {
        private readonly IRequestTimeOffRepository _requestTimeOffRepository;
        private readonly Session _session;
        public TimeOffValidator(ISystemDateTime systemDate, IRequestTimeOffRepository requestTimeOffRepository, Session session)
        {
            _requestTimeOffRepository = requestTimeOffRepository;
            _session = session;
            RuleFor(x => x.Username)
                .NotEmpty()
                .WithMessage("Username is required.");
            RuleFor(x => x.Username)
                .Must((string Username) => string.IsNullOrEmpty(Username) == false && Username.Any(c => (char.IsWhiteSpace(c) || char.IsControl(c))) == false)
                .WithMessage("Remove invalid characters for Username.");
            RuleFor(x => x.Date)
                .GreaterThan(systemDate.Now())
                .WithMessage($"Date must be on or after {systemDate.Now().Date.ToShortDateString()}");
            RuleFor(x => x.Date)
                .Must((DateTimeOffset Date) => Date.DayOfWeek != DayOfWeek.Saturday && Date.DayOfWeek != DayOfWeek.Sunday)
                .WithMessage("Request cannot be on a weekend.");
            RuleFor(x => x.Date)
                .Must((DateTimeOffset Date) => requestTimeOffRepository.HolidayQuery(h => h.Date.Date == Date.Date).Any() == false)
                .WithMessage("You can't request off a holiday.");
            RuleFor(x => x)
                .Must((TimeOff newTimeOff) => requestTimeOffRepository.TimeOffQuery(t => t.Username == _session.User.Username && t.Date == newTimeOff.Date && t.Id != newTimeOff.Id).Any() == false)
                .WithMessage(x => $"Unable to add a duplicate date {x.Date.Date.ToShortDateString()}");
            RuleFor(x => x.Date)
                .Must((DateTimeOffset Date) => IsNotACurrentRequest(Date))
                .WithMessage(x => $"Unable to add a duplicate date {x.Date.Date.ToShortDateString()}");
            RuleFor(x => x)
                .Must((TimeOff timeOff) => IsNotADuplicate(timeOff.Date.Date, timeOff.Username))
                .WithSeverity(Severity.Warning)
                .WithMessage("Two or more employees of the same department for the same day needs supervisor approval.");   
        }
        public List<TimeOff> ExistingRequests { get; set; }
        private bool IsNotACurrentRequest(DateTimeOffset Date)
        {
            if (ExistingRequests?.Any(r => r.Date.Date == Date.Date) ?? false)
            {
                return false;
            }

            return true;
        }
        [ExcludeFromCodeCoverage]
        private bool IsNotADuplicate(DateTime date, string userName)
        {
            var currUser = _requestTimeOffRepository.UserQuery(u => u.Username == userName).FirstOrDefault();
            var users = _requestTimeOffRepository.UserQuery(u => u.Dept == currUser?.Dept && u.Username != currUser?.Username);
            var userTimeOffRecords = _requestTimeOffRepository.TimeOffQuery(t => t.Date.Date == date && users.Any(u => u.Username == t.Username));

            return userTimeOffRecords.Count() == 0;
        }
    }
}
