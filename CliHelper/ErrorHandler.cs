namespace CliHelper
{
    using System;

    /// <summary>
    /// A static class to log error using stacktrace or not.
    /// </summary>
    internal static class ErrorHandler
    {
        /// <summary>
        /// Log error from a string message.
        /// </summary>
        /// <param name="message">The error message.</param>
        internal static void Log(string message)
        {
            Log(new Exception(message));
        }

        /// <summary>
        /// Log error from a exception.
        /// </summary>
        /// <param name="exception">One exception.</param>
        internal static void Log(Exception exception)
        {
            var showStackTraceOnError = CliApplication.Current.Options.ShowStackTraceOnError;
            if (showStackTraceOnError)
            {
                throw exception;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(exception.Message);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
}