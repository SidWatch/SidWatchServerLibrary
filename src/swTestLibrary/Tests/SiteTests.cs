using System;
using NUnit.Framework;
using Sidwatch.Library.Managers;
using Sidwatch.Library.Objects;
using TreeGecko.Library.Geospatial.Objects;

namespace swTestLibrary.Tests
{
    [TestFixture]
    public class SiteTests
    {
        [Test]
        public void SerializeSite()
        {
            Site site = new Site
            {
                Active = true,
                Guid = Guid.NewGuid(),
                LastModifiedBy = Guid.NewGuid(),
                LastModifiedDateTime = DateTime.Now,
                Location = new GeoPoint(101, 45),
                MonitorID = 1000,
                Name = "TestSite",
                ParentGuid = Guid.NewGuid(),
                PersistedDateTime = DateTime.Now,
                SiteOwnerGuid = Guid.NewGuid(),
                Timezone = "MST",
                UTCOffset = 6,
                VersionGuid = Guid.NewGuid()
            };

            var serialized = site.GetTGSerializedObject();

            Site site2 = new Site();
            site2.LoadFromTGSerializedObject(serialized);

            Assert.AreEqual(site.Active, site2.Active);
            Assert.AreEqual(site.Guid, site2.Guid);
            Assert.AreEqual(site.LastModifiedBy, site2.LastModifiedBy);
            Assert.AreEqual(site.LastModifiedDateTime, site2.LastModifiedDateTime);
            Assert.AreEqual(site.Location.X, site2.Location.X);
            Assert.AreEqual(site.Location.Y, site2.Location.Y);
            Assert.AreEqual(site.MonitorID, site2.MonitorID);
            Assert.AreEqual(site.Name, site2.Name);
            Assert.AreEqual(site.ParentGuid, site2.ParentGuid);
            Assert.AreEqual(site.PersistedDateTime, site2.PersistedDateTime);
            Assert.AreEqual(site.SiteOwnerGuid, site2.SiteOwnerGuid);
            Assert.AreEqual(site.Timezone, site2.Timezone);
            Assert.AreEqual(site.UTCOffset, site2.UTCOffset);
            Assert.AreEqual(site.VersionGuid, site2.VersionGuid);
        }


        [Test]
        public void AddSite()
        {
            Site site = new Site
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

            SidwatchManager manager = new SidwatchManager();
            manager.Persist(site);
        }

        [Test]
        public void GetSite()
        {
            Site site = new Site
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

            SidwatchManager manager = new SidwatchManager();
            manager.Persist(site);

            Site site2 = manager.GetSite(site.Guid);
            Assert.IsNotNull(site2);

            Assert.AreEqual(site.Active, site2.Active);
            Assert.AreEqual(site.Guid, site2.Guid);
            Assert.AreEqual(site.LastModifiedBy, site2.LastModifiedBy);
            Assert.AreEqual(site.LastModifiedDateTime, site2.LastModifiedDateTime);
            Assert.AreEqual(site.Location.X, site2.Location.X);
            Assert.AreEqual(site.Location.Y, site2.Location.Y);
            Assert.AreEqual(site.MonitorID, site2.MonitorID);
            Assert.AreEqual(site.Name, site2.Name);
            Assert.AreEqual(site.ParentGuid, site2.ParentGuid);
            Assert.AreEqual(site.PersistedDateTime, site2.PersistedDateTime);
            Assert.AreEqual(site.SiteOwnerGuid, site2.SiteOwnerGuid);
            Assert.AreEqual(site.Timezone, site2.Timezone);
            Assert.AreEqual(site.UTCOffset, site2.UTCOffset);
        }

        [Test]
        public void UpdateSite()
        {
            Site site = new Site
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

            SidwatchManager manager = new SidwatchManager();
            manager.Persist(site);

            Site site2 = manager.GetSite(site.Guid);
            Assert.IsNotNull(site2);

            site2.Name = "TestSite2";
            manager.Persist(site2);

            Site site3 = manager.GetSite(site.Guid);
            Assert.IsNotNull(site3);
            
            Assert.AreEqual(site2.Active, site3.Active);
            Assert.AreEqual(site2.Guid, site3.Guid);
            Assert.AreEqual(site2.LastModifiedBy, site3.LastModifiedBy);
            Assert.AreEqual(site2.LastModifiedDateTime, site3.LastModifiedDateTime);
            Assert.AreEqual(site2.Location.X, site3.Location.X);
            Assert.AreEqual(site2.Location.Y, site3.Location.Y);
            Assert.AreEqual(site2.MonitorID, site3.MonitorID);
            Assert.AreEqual(site2.Name, site3.Name);
            Assert.AreEqual(site2.ParentGuid, site3.ParentGuid);
            Assert.AreEqual(site2.PersistedDateTime, site3.PersistedDateTime);
            Assert.AreEqual(site2.SiteOwnerGuid, site3.SiteOwnerGuid);
            Assert.AreEqual(site2.Timezone, site3.Timezone);
            Assert.AreEqual(site2.UTCOffset, site3.UTCOffset);
        }
    }
}
