using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace OpenTelemetryWebAPI
{
    public class UserService : IUserService
    {
        private static readonly List<User> _users = new()
        {
            new User("shahbaz","password007")
        };

        private readonly ILogger _logger;
        private readonly ActivitySource _activitySource = new("Tracing.NET");
        public UserService(ILogger<UserService> logger)
        {
            _logger = logger;
        }
        public bool Login(string username, string password)
        {
            using var activity = _activitySource.StartActivity("Login");
            _logger.LogInformation("Attempting to log in user: {UserName}", username);

            bool userExists = _users.Any(u => u.Equals(new User(username, password)));

            if (userExists)
            {
                _logger.LogInformation("User found, logging in");
            }
            else
            {
                _logger.LogWarning("User not found");
            }

            return userExists;
        }
    }
}
