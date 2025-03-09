using System.Collections.Concurrent;

namespace MoneyDiary.Services.AuthService
{
    public interface IUserTrackingService
    {
        void AddUser(string userId);
        void RemoveUser(string userId);
        ConcurrentDictionary<string, bool> GetLoggedInUsers();
    }

    public class UserTrackingService : IUserTrackingService
    {
        private readonly ConcurrentDictionary<string, bool> _loggedInUsers = new ConcurrentDictionary<string, bool>();

        public void AddUser(string userId)
        {
            _loggedInUsers[userId] = true;
        }

        public void RemoveUser(string userId)
        {
            _loggedInUsers.TryRemove(userId, out _);
        }

        public ConcurrentDictionary<string, bool> GetLoggedInUsers()
        {
            return _loggedInUsers;
        }
    }
}