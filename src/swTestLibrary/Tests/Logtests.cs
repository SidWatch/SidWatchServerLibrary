using System;
using NUnit.Framework;
using Sidwatch.Library.Managers;

namespace swTestLibrary.Tests
{
    [TestFixture]
    public class Logtests
    {
        [Test]
        public void TestLogs()
        {
            SidWatchManager manager = new SidWatchManager();

            manager.LogException(Guid.NewGuid(), "this is an exception");
            manager.LogInfo(Guid.NewGuid(), "this is info");
            manager.LogVerbose(Guid.NewGuid(), "this is verbose");
            manager.LogWarning(Guid.NewGuid(), "this is a warning");
        }

    }
}
