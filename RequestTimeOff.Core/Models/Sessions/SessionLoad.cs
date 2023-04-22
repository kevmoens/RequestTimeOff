using System.Threading.Tasks;

namespace RequestTimeOff.Models.Sessions
{
    public class SessionLoad : ISessionLoad
    {
        private readonly Session _session;
        private readonly IRequestTimeOffRepository _requestTimeOffRepository;
        public SessionLoad(Session session, IRequestTimeOffRepository requestTimeOffRepository)
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
