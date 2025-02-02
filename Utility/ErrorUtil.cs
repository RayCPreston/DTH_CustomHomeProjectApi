using DTH.API.Models;

namespace DTH.API.Utility
{
    public static class ErrorUtil
    {
        /// <summary>
        /// Creates an Error object with the given code and message.
        /// </summary>
        /// <param name="code">string</param>
        /// <param name="message">string</param>
        public static Error CreateError(string code, string message)
        {
            return new Error
            {
                Code = code,
                Message = message
            };
        }
    }
}
