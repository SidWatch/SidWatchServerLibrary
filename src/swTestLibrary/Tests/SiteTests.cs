using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Sidwatch.Library.Managers;
using Sidwatch.Library.Objects;
using TreeGecko.Library.Common.Helpers;
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
            Assert.AreEqual(site.Location.Y, site.Location.Y);
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
            string test = Config.GetSettingValue("SW_DBPORT");

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

    }
}
