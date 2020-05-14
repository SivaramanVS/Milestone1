using NLog;

namespace BusinessService.Api.Logger
{
    /// <summary>
    /// 
    /// </summary>
    public class LogNLog : ILog
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// 
        /// </summary>
        public LogNLog()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void Information(string message)
        {
            Logger.Info(message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void Warning(string message)
        {
            Logger.Warn(message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void Debug(string message)
        {
            Logger.Debug(message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void Error(string message)
        {
            Logger.Error(message);
        }
    }
}