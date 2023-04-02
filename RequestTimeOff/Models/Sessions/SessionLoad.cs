using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestTimeOff.Models.Sessions
{
    public class SessionLoad : ISessionLoad
    {
        private Models.Session _session;
        private IRequestTimeOffRepository _requestTimeOffRepository;
        public SessionLoad(Models.Session session, IRequestTimeOffRepository requestTimeOffRepository)
        {
            _session = session;
            _requestTimeOffRepository = requestTimeOffRepository;
        }
        public async Task Initialize()
        {
            await Task.Run(() =>
            {
                _session.DepartmentColleagues = _requestTimeOffRepository.UserQuery(u => u.Dept == _session.User.Dept);
            });
        }
    }
}
