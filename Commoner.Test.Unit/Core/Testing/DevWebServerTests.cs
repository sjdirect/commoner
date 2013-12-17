using System.Diagnostics;
using System.IO;
using NUnit.Framework;
using Commoner.Core.Testing;

namespace Commoner.Test.Unit.Testing
{
    [TestFixture]
    public class DevWebServerTests
    {
        private const string TEST_DIRECTORY = "testapp";
        public const string TEST_FILE_NAME = "index.html";

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            Directory.CreateDirectory(TEST_DIRECTORY + "/");
            File.AppendAllText(TEST_DIRECTORY + "/" + TEST_FILE_NAME, "<h1>Here is some content</h1>");
        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            string file = TEST_DIRECTORY + "/" + TEST_FILE_NAME;
            if (File.Exists(file))
            {
                File.Delete(file);
            }
        }

        [Test]
        public void RunAndStop()
        {
            DevWebServer server = new DevWebServer(1111, Path.GetFullPath(TEST_DIRECTORY));
            Assert.AreNotEqual(null, server.WebServerExecutablePath);

            server.Run();
            System.Threading.Thread.Sleep(3000);
            int serverProcessID = server.ProcessID;
            Process serverProcess = System.Diagnostics.Process.GetProcessById(serverProcessID);
            Assert.AreEqual(true, serverProcess.Responding);
            Assert.AreEqual(false, serverProcess.HasExited);

            server.Stop();
            server.Dispose();
            Assert.AreEqual(true, serverProcess.HasExited);
        }
    }
}
