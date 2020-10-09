namespace CliHelper
{
    public class CliOptions
    {
        /// <summary>
        /// Show the stack trace if throws exception.
        /// If false console write the exception message only.
        /// </summary>
        public readonly bool showStackTraceOnError;
        
        /// <summary>
        /// Add the --help command.
        /// </summary>
        public readonly bool addHelp;

        /// <summary>
        /// The CLI Application options.
        /// </summary>
        /// <param name="showStackTraceOnError">
        /// Show the stack trace if throws exception.
        /// If false console write the exception message only.
        /// </param>
        /// <param name="addHelp">Add the --help command.</param>
        public CliOptions(bool showStackTraceOnError = false, bool addHelp = true)
        {
            this.showStackTraceOnError = showStackTraceOnError;
            this.addHelp = addHelp;
        }
    }
}