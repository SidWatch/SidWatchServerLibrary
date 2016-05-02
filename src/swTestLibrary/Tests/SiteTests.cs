using System;
using System.Collections.Generic;
using NUnit.Framework;
using Sidwatch.Library.Managers;
using Sidwatch.Library.Objects;
using TreeGecko.Library.Common.Security;
using TreeGecko.Library.Geospatial.Objects;

namespace swTestLibrary.Tests
{
    [TestFixture]
    public class SiteTests
    {
        SidWatchManager m_Manager = new SidWatchManager();

        [OneTimeSetUp]
        public void Setup()
        {
        }

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

            m_Manager.Persist(site);
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

            m_Manager.Persist(site);

            Site site2 = m_Manager.GetSite(site.Guid);
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

            m_Manager.Persist(site);

            Site site2 = m_Manager.GetSite(site.Guid);
            Assert.IsNotNull(site2);

            site2.Name = "TestSite2";
            m_Manager.Persist(site2);

            Site site3 = m_Manager.GetSite(site.Guid);
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

        [Test]
        public void GetAllSites()
        {
            Site site1 = new Site
            {
                Active = true,
                Guid = Guid.NewGuid(),
                LastModifiedBy = Guid.NewGuid(),
                LastModifiedDateTime = DateTime.Now,
                Location = new GeoPoint(-101, 45),
                MonitorID = 1000,
                Name = RandomString.GetRandomString(10),
                ParentGuid = Guid.NewGuid(),
                PersistedDateTime = DateTime.Now,
                SiteOwnerGuid = Guid.NewGuid(),
                Timezone = "MST",
                UTCOffset = 6,
                VersionGuid = Guid.NewGuid()
            };
            m_Manager.Persist(site1);

            Site site2 = new Site
            {
                Active = false,
                Guid = Guid.NewGuid(),
                LastModifiedBy = Guid.NewGuid(),
                LastModifiedDateTime = DateTime.Now,
                Location = new GeoPoint(-101, 45),
                MonitorID = 1000,
                Name = RandomString.GetRandomString(10),
                ParentGuid = Guid.NewGuid(),
                PersistedDateTime = DateTime.Now,
                SiteOwnerGuid = Guid.NewGuid(),
                Timezone = "MST",
                UTCOffset = 6,
                VersionGuid = Guid.NewGuid()
            };
            m_Manager.Persist(site2);

            List<Site> sites = m_Manager.GetSites();
            bool found1 = false;
            bool found2 = false;

            foreach (var site in sites)
            {
                if (site.Guid == site1.Guid)
                {
                    found1 = true;
                }

                if (site.Guid == site2.Guid)
                {
                    found2 = true;
                }
            }

            Assert.IsTrue(found1);
            Assert.IsTrue(found2);
        }

        [Test]
        public void GetActiveSites()
        {
            Site site1 = new Site
            {
                Active = true,
                Guid = Guid.NewGuid(),
                LastModifiedBy = Guid.NewGuid(),
                LastModifiedDateTime = DateTime.Now,
                Location = new GeoPoint(-101, 45),
                MonitorID = 1000,
                Name = RandomString.GetRandomString(10),
                ParentGuid = Guid.NewGuid(),
                PersistedDateTime = DateTime.Now,
                SiteOwnerGuid = Guid.NewGuid(),
                Timezone = "MST",
                UTCOffset = 6,
                VersionGuid = Guid.NewGuid()
            };
            m_Manager.Persist(site1);

            Site site2 = new Site
            {
                Active = false,
                Guid = Guid.NewGuid(),
                LastModifiedBy = Guid.NewGuid(),
                LastModifiedDateTime = DateTime.Now,
                Location = new GeoPoint(-101, 45),
                MonitorID = 1000,
                Name = RandomString.GetRandomString(10),
                ParentGuid = Guid.NewGuid(),
                PersistedDateTime = DateTime.Now,
                SiteOwnerGuid = Guid.NewGuid(),
                Timezone = "MST",
                UTCOffset = 6,
                VersionGuid = Guid.NewGuid()
            };
            m_Manager.Persist(site2);

            List<Site> sites = m_Manager.GetActiveSites();
            bool found1 = false;
            bool found2 = false;

            foreach (var site in sites)
            {
                if (site.Guid == site1.Guid)
                {
                    found1 = true;
                }

                if (site.Guid == site2.Guid)
                {
                    found2 = true;
                }
            }

            Assert.IsTrue(found1);
            Assert.IsTrue(!found2);
        }
    }
}
