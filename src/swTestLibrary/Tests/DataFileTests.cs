using System;
using System.Collections.Generic;
using NUnit.Framework;
using Sidwatch.Library.Managers;
using Sidwatch.Library.Objects;
using TreeGecko.Library.Common.Objects;
using TreeGecko.Library.Common.Security;
using TreeGecko.Library.Geospatial.Objects;

namespace swTestLibrary.Tests
{
    [TestFixture]
    public class DataFileTests
    {
        private SidWatchManager m_Manager = new SidWatchManager();
        private Guid m_SiteGuid;

        [OneTimeSetUp]
        public void Setup()
        {
            Site site = new Site
            {
                Name = RandomString.GetRandomString(10),
                Guid = Guid.NewGuid(),
                Active = true,
                Location = new GeoPoint(-100, 39)
            };

            m_SiteGuid = site.Guid;

            m_Manager.Persist(site);
        }

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

        [Test]
        public void AddDataFile()
        {
            DataFile file = new DataFile
            {
                Active = true,
                Archived = false,
                Available = true,
                DateTime = DateTime.Now,
                Filename = RandomString.GetRandomString(10),
                Guid = Guid.NewGuid(),
                ParentGuid = m_SiteGuid,
                Processed = true
            };

            m_Manager.Persist(file);
        }

        [Test]
        public void GetDataFile()
        {
            DataFile file = new DataFile
            {
                Active = true,
                Archived = false,
                Available = true,
                DateTime = DateTime.Now,
                Filename = RandomString.GetRandomString(10),
                Guid = Guid.NewGuid(),
                ParentGuid = m_SiteGuid,
                Processed = true
            };

            m_Manager.Persist(file);

            DataFile file2 = m_Manager.GetDataFile(file.Guid);

            Assert.AreEqual(file.Active, file2.Active);
            Assert.AreEqual(file.Archived, file2.Archived);
            Assert.AreEqual(file.Available, file2.Available);
            Assert.AreEqual(file.DateTime, file2.DateTime);
            Assert.AreEqual(file.Filename, file2.Filename);
            Assert.AreEqual(file.Guid, file2.Guid);
            Assert.AreEqual(file.ParentGuid, file2.ParentGuid);
            Assert.AreEqual(file.Processed, file2.Processed);
        }

        [Test]
        public void UpdateDataFile()
        {
            DataFile file = new DataFile
            {
                Active = true,
                Archived = false,
                Available = true,
                DateTime = DateTime.Now,
                Filename = RandomString.GetRandomString(10),
                Guid = Guid.NewGuid(),
                ParentGuid = m_SiteGuid,
                Processed = true
            };

            m_Manager.Persist(file);

            DataFile file2 = m_Manager.GetDataFile(file.Guid);
            file2.Filename = RandomString.GetRandomString(10);
            m_Manager.Persist(file2);

            DataFile file3 = m_Manager.GetDataFile(file.Guid);
            
            Assert.AreEqual(file2.Active, file3.Active);
            Assert.AreEqual(file2.Archived, file3.Archived);
            Assert.AreEqual(file2.Available, file3.Available);
            Assert.AreEqual(file2.DateTime, file3.DateTime);
            Assert.AreEqual(file2.Filename, file3.Filename);
            Assert.AreEqual(file2.Guid, file3.Guid);
            Assert.AreEqual(file2.ParentGuid, file3.ParentGuid);
            Assert.AreEqual(file2.Processed, file3.Processed);
        }

        [Test]
        public void GetDataFiles()
        {
            DataFile file1 = new DataFile
            {
                Active = true,
                Archived = false,
                Available = true,
                DateTime = DateTime.Now,
                Filename = RandomString.GetRandomString(10),
                Guid = Guid.NewGuid(),
                ParentGuid = m_SiteGuid,
                Processed = true
            };
            m_Manager.Persist(file1);

            DataFile file2 = new DataFile
            {
                Active = true,
                Archived = false,
                Available = true,
                DateTime = DateTime.Now,
                Filename = RandomString.GetRandomString(10),
                Guid = Guid.NewGuid(),
                ParentGuid = m_SiteGuid,
                Processed = true
            };
            m_Manager.Persist(file2);

            List<DataFile> files = m_Manager.GetDataFiles(m_SiteGuid);

            bool found1 = false;
            bool found2 = false;

            foreach (var dataFile in files)
            {
                if (dataFile.Guid == file1.Guid)
                {
                    found1 = true;
                }

                if (dataFile.Guid == file2.Guid)
                {
                    found2 = true;
                }
            }

            Assert.IsTrue(found1);
            Assert.IsTrue(found2);
        }
    }
}
