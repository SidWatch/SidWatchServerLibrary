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
    public class StationReadingTests
    {
        private readonly SidWatchManager m_Manager = new SidWatchManager();
        private Station m_Station;
        private Site m_Site;

        [OneTimeSetUp]
        public void Setup()
        {
            m_Station = new Station
            {
                Active = true,
                CallSign = RandomString.GetRandomString(6),
                Country = RandomString.GetRandomString(3),
                Frequency = 12330,
                Guid = Guid.NewGuid(),
                Location = new GeoPoint(-110, 45),
                Notes = RandomString.GetRandomString(40)
            };
            m_Manager.Persist(m_Station);

            m_Site = new Site
            {
                Active = true,
                Guid = Guid.NewGuid(),
                LastModifiedBy = Guid.NewGuid(),
                LastModifiedDateTime = DateTime.Now,
                Location = new GeoPoint(-101, 45),
                MonitorID = 1000,
                Name = "TestSite",
                ParentGuid = Guid.NewGuid(),
                PersistedDateTime = DateTime.Now,
                SiteOwnerGuid = Guid.NewGuid(),
                Timezone = "MST",
                UTCOffset = 6,
                VersionGuid = Guid.NewGuid()
            };
            m_Manager.Persist(m_Site);
        }

        [Test]
        public void SerializationTests()
        {
            StationReading sr = new StationReading
            {
                Active = true,
                FileGuid = Guid.NewGuid(),
                Guid = Guid.NewGuid(),
                ParentGuid = Guid.NewGuid(),
                ReadingDateTime = DateTime.Now,
                ReadingMagnitude = 15.5,
                StationGuid = Guid.NewGuid()
            };

            TGSerializedObject tgs = sr.GetTGSerializedObject();

            StationReading sr2 = new StationReading();
            sr2.LoadFromTGSerializedObject(tgs);

            Assert.AreEqual(sr.Active, sr2.Active);
            Assert.AreEqual(sr.FileGuid, sr2.FileGuid);
            Assert.AreEqual(sr.Guid, sr2.Guid);
            Assert.AreEqual(sr.ParentGuid, sr2.ParentGuid);
            Assert.AreEqual(sr.ReadingDateTime, sr2.ReadingDateTime);
            Assert.AreEqual(sr.ReadingMagnitude, sr2.ReadingMagnitude);
            Assert.AreEqual(sr.StationGuid, sr2.StationGuid);
        }

        [Test]
        public void AddTest()
        {
            StationReading sr = new StationReading
            {
                Active = true,
                FileGuid = Guid.NewGuid(),
                Guid = Guid.NewGuid(),
                ParentGuid = m_Site.Guid,
                ReadingDateTime = DateTime.Now,
                ReadingMagnitude = 15.5,
                StationGuid = m_Station.Guid
            };

            m_Manager.Persist(sr);
        }

        [Test]
        public void GetTest()
        {
            StationReading sr = new StationReading
            {
                Active = true,
                FileGuid = Guid.NewGuid(),
                Guid = Guid.NewGuid(),
                ParentGuid = m_Site.Guid,
                ReadingDateTime = DateTime.Now,
                ReadingMagnitude = 15.5,
                StationGuid = m_Station.Guid
            };

            m_Manager.Persist(sr);

            StationReading sr2 = m_Manager.GetStationReading(sr.Guid);
            Assert.IsNotNull(sr2);

            Assert.AreEqual(sr.Active, sr2.Active);
            Assert.AreEqual(sr.FileGuid, sr2.FileGuid);
            Assert.AreEqual(sr.Guid, sr2.Guid);
            Assert.AreEqual(sr.ParentGuid, sr2.ParentGuid);
            Assert.AreEqual(sr.ReadingDateTime, sr2.ReadingDateTime);
            Assert.AreEqual(sr.ReadingMagnitude, sr2.ReadingMagnitude);
            Assert.AreEqual(sr.StationGuid, sr2.StationGuid);
        }

        [Test]
        public void UpdateTest()
        {
            StationReading sr = new StationReading
            {
                Active = true,
                FileGuid = Guid.NewGuid(),
                Guid = Guid.NewGuid(),
                ParentGuid = m_Site.Guid,
                ReadingDateTime = DateTime.Now,
                ReadingMagnitude = 15.5,
                StationGuid = m_Station.Guid
            };

            m_Manager.Persist(sr);

            StationReading sr2 = m_Manager.GetStationReading(sr.Guid);
            Assert.IsNotNull(sr2);

            sr2.ReadingMagnitude = 20.21;
            m_Manager.Persist(sr2);

            StationReading sr3 = m_Manager.GetStationReading(sr.Guid);
            Assert.IsNotNull(sr3);

            Assert.AreEqual(sr2.ReadingMagnitude, sr3.ReadingMagnitude);
        }

        [Test]
        public void GetReadings()
        {
            StationReading sr = new StationReading
            {
                Active = true,
                FileGuid = Guid.NewGuid(),
                Guid = Guid.NewGuid(),
                ParentGuid = m_Site.Guid,
                ReadingDateTime = DateTime.Now,
                ReadingMagnitude = 15.5,
                StationGuid = m_Station.Guid
            };
            m_Manager.Persist(sr);

            StationReading sr2 = new StationReading
            {
                Active = true,
                FileGuid = Guid.NewGuid(),
                Guid = Guid.NewGuid(),
                ParentGuid = m_Site.Guid,
                ReadingDateTime = DateTime.Now,
                ReadingMagnitude = 15.5,
                StationGuid = Guid.NewGuid()
            };
            m_Manager.Persist(sr2);

            StationReading sr3 = new StationReading
            {
                Active = true,
                FileGuid = Guid.NewGuid(),
                Guid = Guid.NewGuid(),
                ParentGuid = Guid.NewGuid(),
                ReadingDateTime = DateTime.Now,
                ReadingMagnitude = 15.5,
                StationGuid = m_Station.Guid
            };
            m_Manager.Persist(sr3);

            List<StationReading> readings = m_Manager.GetStationReadings(
                m_Station.Guid, 
                m_Site.Guid,
                DateTime.Now.AddYears(-1), 
                DateTime.Now.AddDays(1));

            Assert.Greater(readings.Count, 0);

            bool has1 = false;
            bool has2 = false;
            bool has3 = false;

            foreach (var stationReading in readings)
            {
                if (stationReading.Guid == sr.Guid)
                {
                    has1 = true;
                }

                if (stationReading.Guid == sr2.Guid)
                {
                    has2 = true;
                }

                if (stationReading.Guid == sr3.Guid)
                {
                    has3 = true;
                }
            }

            Assert.IsTrue(has1);
            Assert.IsFalse(has2);
            Assert.IsFalse(has3);
        }

    }
}
