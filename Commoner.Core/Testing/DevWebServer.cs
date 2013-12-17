using System;
using System.Diagnostics;
using System.IO;

namespace Commoner.Core.Testing
{
    /// <summary>
    /// Runs a local test web server (Cassini by default but is configurable)
    /// </summary>
    public class DevWebServer : IDisposable
    {
        private Process _testServer;
        private int _port;
        private string _webServerExecutablePath;
        private string _projectToRunPath;

        /// <summary>
        /// The path to the web server exe (defaults to C:\Program Files\Common Files\Microsoft Shared\DevServer\9.0\WebDev.WebServer.EXE)
        /// </summary>
        public string WebServerExecutablePath
        {
            get
            {
                return _webServerExecutablePath;
            }
            set
            {
                _webServerExecutablePath = value;
            }
        }

        /// <summary>
        /// Path to the project that should be run by the test server
        /// </summary>
        public string ProjectToRunPath
        {
            get
            {
                return _projectToRunPath;
            }
            set
            {
                _projectToRunPath = value;
            }
        }

        /// <summary>
        /// The id of the process that is running the server
        /// </summary>
        public int ProcessID
        {
            get
            {
                return _testServer.Id;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="port">Port number the test server should run on</param>
        /// <param name="projectToRunPath">Path to the project that should be run by the test server</param>
        public DevWebServer(int portNumber, string projectToRunPath)
        {
            _port = portNumber;
            ProjectToRunPath = Path.GetFullPath(projectToRunPath);
            WebServerExecutablePath = @"C:\Program Files (x86)\Common Files\microsoft shared\DevServer\9.0\WebDev.WebServer.EXE";
        }

        /// <summary>
        /// Starts the test web server
        /// </summary>
        public void Run()
        {
            _testServer = new Process();

            _testServer.StartInfo.FileName = WebServerExecutablePath;
            _testServer.StartInfo.Arguments = "/port:" + _port + " /path:\"" + ProjectToRunPath + "\"";
            _testServer.Start();
        }

        /// <summary>
        /// Stops the test web server and releases resources. (This may take a few seconds)
        /// </summary>
        public void Stop()
        {
            _testServer.Kill();
            _testServer.WaitForExit();
        }

        /// <summary>
        /// Releases the unmanaged resources used by the System.ComponentModel.Component and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _testServer.Dispose();
            }
        }

        /// <summary>
        /// Releases all resources used by the System.ComponentModel.Component.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

