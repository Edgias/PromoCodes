using Microsoft.Extensions.Logging;
using TheRoom.PromoCodes.ApplicationCore.Interfaces;

namespace TheRoom.PromoCodes.Infrastructure.Services
{
    public class LoggingService<T> : IAppLogger<T>
    {
        private readonly ILogger<T> _logger;
        public LoggingService(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<T>();
        }

        public void LogError(string message, params object[] args)
        {
            _logger.LogError(message, args);
        }

        public void LogWarning(string message, params object[] args)
        {
            _logger.LogWarning(message, args);
        }

        public void LogInformation(string message, params object[] args)
        {
            _logger.LogInformation(message, args);
        }

        
    }
}
