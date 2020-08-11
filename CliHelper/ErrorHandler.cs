using System;

namespace LavaLeak.CliHelper
{
    public static class ErrorHandler
    {
        public static void Log(string message)
        {
            Log(new Exception(message));
        }

        public static void Log(Exception exception)
        {
            var showStackTraceOnError = CliApplication.Current.Options.showStackTraceOnError;
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