using System;
using NUnit.Framework;
using Sidwatch.Library.Objects;
using TreeGecko.Library.Common.Objects;

namespace swTestLibrary.Tests
{
    [TestFixture]
    public class DataFileTests
    {
        [Test]
        public void SerializeDataFile()
        {
            DataFile file = new DataFile
            {
                Active = true,
                Archived = false,
                Available = true,
                DateTime = DateTime.Now,
                Filename = "Test1",
                Guid = Guid.NewGuid(),
                ParentGuid = Guid.NewGuid(),
                Processed = true
            };

            TGSerializedObject tgs = file.GetTGSerializedObject();

            DataFile file2 = new DataFile();
            file2.LoadFromTGSerializedObject(tgs);

            Assert.AreEqual(file.Active, file2.Active);
            Assert.AreEqual(file.Archived, file2.Archived);
            Assert.AreEqual(file.Available, file2.Available);
            Assert.AreEqual(file.DateTime, file2.DateTime);
            Assert.AreEqual(file.Filename, file2.Filename);
            Assert.AreEqual(file.Guid, file2.Guid);
            Assert.AreEqual(file.ParentGuid, file2.ParentGuid);
            Assert.AreEqual(file.Processed, file2.Processed);
        }


    }
}
