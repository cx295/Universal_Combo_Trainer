using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("EDS.Logging.Tests")]

namespace EDS.Logging
{
    public class Logger : ILogger
    {
        public Logger()
        {
        }

        ~Logger()
        {
            CloseAllOpenStreamWriters();
        }

        private void CloseAllOpenStreamWriters()
        {
            CloseStreamWriter(_swDefaultLogFile);
            foreach (var registeredLogger in _registeredLoggers)
            {
                CloseStreamWriter(registeredLogger.Value);
            }
            _registeredLoggers.Clear();
        }

        private void CloseStreamWriter(StreamWriter sw)
        {
            if (sw != null)
            {
                if (sw.BaseStream != null)
                {
                    if (sw.BaseStream.CanWrite)
                    {
                        sw.Close();
                    }
                }
            }
        }

        private StreamWriter _swDefaultLogFile = null;

        private readonly Dictionary<String, StreamWriter> _registeredLoggers =
                     new Dictionary<String, StreamWriter>();

        void ILogger.SetDefaultLogFile(String path)
        {
            if (path == null)
            {
                CloseStreamWriter(_swDefaultLogFile);
            }
            else
            {
                // This is how StreamWriter works:
                // If the file exists AND append is true,
                // any text written to it gets appended;
                // Otherwise, a new file is created that
                // contains the text being written to it.
                _swDefaultLogFile = new StreamWriter(path, true);
            }
        }

        void ILogger.Register(String logFilePath)
        {
            var loggerName = Assembly.GetCallingAssembly().GetName().Name;
            if (_registeredLoggers.ContainsKey(loggerName))
            {
                CloseStreamWriter(_registeredLoggers[loggerName]);
            }
            _registeredLoggers[loggerName] = new StreamWriter(logFilePath, true);
        }

        void ILogger.UnRegister()
        {
            var loggerName = Assembly.GetCallingAssembly().GetName().Name;
            if (_registeredLoggers.ContainsKey(loggerName))
            {
                CloseStreamWriter(_registeredLoggers[loggerName]);
                _registeredLoggers.Remove(loggerName);
            }
        }

        private void WriteLine(String message, String loggerName)
        {
            if (!(String.IsNullOrWhiteSpace(message)))
            {
                var sw = GetStreamWriter(loggerName);
                sw.WriteLine(String.Format("[{0}] {1}", Now, message));
                sw.Flush();
            }
        }

        private String Now
        {
            get
            {
                const String FORMAT = "yyyy-MM-dd HH:mm:ss.ffffff";
                return DateTime.Now.ToString(FORMAT);
            }
        }

        private StreamWriter GetStreamWriter(String loggerName)
        {
            StreamWriter sw = null;
            if (_registeredLoggers.ContainsKey(loggerName))
            {
                sw = _registeredLoggers[loggerName];
            }
            else
            {
                sw = _swDefaultLogFile;
            }
            return sw;
        }

        void ILogger.Log(String message)
        {
            var loggerName = Assembly.GetCallingAssembly().GetName().Name;
            WriteLine(message, loggerName);
        }

        void ILogger.LogException(Exception ex)
        {
            var loggerName = Assembly.GetCallingAssembly().GetName().Name;
            WriteLine(ex.ToString(), loggerName);
        }

        void ILogger.LogException(Exception ex, String message)
        {
            var loggerName = Assembly.GetCallingAssembly().GetName().Name;
            var msg = String.Format("{0}{2}{1}", ex.ToString(), message, Environment.NewLine);
            WriteLine(msg, loggerName);
        }
    }
}
