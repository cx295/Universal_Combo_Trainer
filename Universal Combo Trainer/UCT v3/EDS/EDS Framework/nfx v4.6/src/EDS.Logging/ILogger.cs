using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDS.Logging
{
    public interface ILogger
    {
        /// <summary>
        /// Provides the ability to use an alternate log file by
        /// associating the calling assembly (using its type) to
        /// the specified <paramref name="path"/> of a log file.
        /// </summary>
        /// <param name="path">Path of the alternate log file.</param>
        void Register(String path);

        /// <summary>
        /// Unregisters the alternate log file of the calling assembly.
        /// </summary>
        void UnRegister();

        /// <summary>
        /// Sets the <paramref name="path"/> of the default log file.
        /// </summary>
        /// <param name="path">Path of the default log file.</param>
        void SetDefaultLogFile(String path);

        /// <summary>
        /// Writes the specified <paramref name="message"/> either to the registered log file of the
        /// calling assembly or, if the calling assembly is not registered, to the default log file.
        /// </summary>
        /// <param name="message">A message to be logged.</param>
        void Log(String message);

        /// <summary>
        /// Logs the specified <paramref name="ex"/>
        /// to the currently registered log file.
        /// </summary>
        /// <param name="ex">An exception to be logged.</param>
        void LogException(Exception ex);

        /// <summary>
        /// Logs the specified <paramref name="ex"/>
        /// and <paramref name="message"/>
        /// to the currently registered log file.
        /// </summary>
        /// <param name="ex">An exception to be logged.</param>
        /// <param name="message">A custom message to be logged.</param>
        void LogException(Exception ex, String message);
    }
}
