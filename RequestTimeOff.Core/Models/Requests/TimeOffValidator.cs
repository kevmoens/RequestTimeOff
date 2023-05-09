using FluentValidation;
using RequestTimeOff.Models;
using RequestTimeOff.Models.Date;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;

namespace RequestTimeOff.Core.Models.Requests
{
    public class TimeOffValidator : AbstractValidator<TimeOff>, ITimeOffValidator
    {
        private IRequestTimeOffRepository _requestTimeOffRepository;
        private Session _session;
        public TimeOffValidator(ISystemDateTime systemDate, IRequestTimeOffRepository requestTimeOffRepository, Session session)
        {
            _requestTimeOffRepository = requestTimeOffRepository;
            _session = session;
            RuleFor(x => x.Username)
                .NotEmpty()
                .WithMessage("Username is required.");
            RuleFor(x => x.Username)
                .Must((string Username) => Username.Any(c => char.IsWhiteSpace(c) || char.IsControl(c)) == false)
                .WithMessage("Remove invalid characters for Username.");
            RuleFor(x => x.Date)
                .GreaterThan(systemDate.Now())
                .WithMessage("Date must be on or after today.");
            RuleFor(x => x.Date)
                .Must((DateTimeOffset Date) => Date.DayOfWeek != DayOfWeek.Saturday && Date.DayOfWeek != DayOfWeek.Sunday)
                .WithMessage("Request cannot be on a weekend.");
            RuleFor(x => x.Date)
                .Must((DateTimeOffset Date) => requestTimeOffRepository.HolidayQuery(h => h.Date == Date).Any() == false)
                .WithMessage("You can't request off a holiday.");
            RuleFor(x => x.Date)
                .Must((DateTimeOffset Date) => requestTimeOffRepository.TimeOffQuery(t => t.Username == _session.User.Username && t.Date == Date).Any() == false)
                .WithMessage(x => $"Unable to add a duplicate date {x.Date.Date.ToShortDateString()}");
            RuleFor(x => x.Date)
                .Must((DateTimeOffset Date) => IsNotACurrentRequest(Date))
                .WithMessage(x => $"Unable to add a duplicate date {x.Date.Date.ToShortDateString()}");
        }
        public List<TimeOff> ExistingRequests { get; set; }
        private bool IsNotACurrentRequest(DateTimeOffset Date)
        {
            if (ExistingRequests?.Any(r => r.Date == Date.Date) ?? false)
            {
                return false; 
            }
           
            return true;
        }
    }
}
