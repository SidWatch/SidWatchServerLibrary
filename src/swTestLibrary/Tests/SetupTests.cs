using NUnit.Framework;
using Sidwatch.Library.Managers;

namespace swTestLibrary.Tests
{
    [SetUpFixture]
    public class SetupTests
    {
        [OneTimeSetUp]
        public void Setup()
        {
            SidWatchStructureManager manager = new SidWatchStructureManager();
            manager.BuildDB();
        }

    }
}
