namespace BusinessService.Api.Logger
{
    /// <summary>
    /// 
    /// </summary>
    public interface ILog
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        void Information(string message);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        void Warning(string message);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        void Debug(string message);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        void Error(string message);
    }
}