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
    public class SiteDayTests
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
        public void SerializationTest()
        {
            SiteDay sd = new SiteDay
            {
                Active = true,
                DataFileCount = 100,
                Date = DateTime.Now.Date,
                Guid = Guid.NewGuid(),
                ParentGuid = m_SiteGuid
            };

            TGSerializedObject tgs = sd.GetTGSerializedObject();

            SiteDay sd2 = new SiteDay();
            sd2.LoadFromTGSerializedObject(tgs);

            Assert.AreEqual(sd.Active, sd2.Active);
            Assert.AreEqual(sd.DataFileCount, sd2.DataFileCount);
            Assert.AreEqual(sd.Date, sd2.Date);
            Assert.AreEqual(sd.Guid, sd2.Guid);
            Assert.AreEqual(sd.ParentGuid, sd2.ParentGuid);
        }

        [Test]
        public void AddSiteDay()
        {
            SiteDay sd = new SiteDay
            {
                Active = true,
                DataFileCount = 18,
                Date = DateTime.Now.Date.AddYears(-5),
                Guid = Guid.NewGuid(),
                ParentGuid = m_SiteGuid
            };

            m_Manager.Persist(sd);
        }

        [Test]
        public void GetSiteDay()
        {
            SiteDay sd = new SiteDay
            {
                Active = true,
                DataFileCount = 18,
                Date = DateTime.Now.Date.AddYears(-4),
                Guid = Guid.NewGuid(),
                ParentGuid = m_SiteGuid
            };

            m_Manager.Persist(sd);

            SiteDay sd2 = m_Manager.GetSiteDay(sd.Guid);
            Assert.IsNotNull(sd2);

            Assert.AreEqual(sd.Active, sd2.Active);
            Assert.AreEqual(sd.DataFileCount, sd2.DataFileCount);
            Assert.AreEqual(sd.Date, sd2.Date);
            Assert.AreEqual(sd.Guid, sd2.Guid);
            Assert.AreEqual(sd.ParentGuid, sd2.ParentGuid);
        }

        [Test]
        public void UpdateSiteDay()
        {
            SiteDay sd = new SiteDay
            {
                Active = true,
                DataFileCount = 18,
                Date = DateTime.Now.Date.AddYears(-3),
                Guid = Guid.NewGuid(),
                ParentGuid = m_SiteGuid
            };

            m_Manager.Persist(sd);

            SiteDay sd2 = m_Manager.GetSiteDay(sd.Guid);
            Assert.IsNotNull(sd2);

            sd2.DataFileCount = 19;
            m_Manager.Persist(sd2);

            SiteDay sd3 = m_Manager.GetSiteDay(sd.Guid);

            Assert.AreEqual(sd2.Active, sd3.Active);
            Assert.AreEqual(sd2.DataFileCount, sd3.DataFileCount);
            Assert.AreEqual(sd2.Date, sd3.Date);
            Assert.AreEqual(sd2.Guid, sd3.Guid);
            Assert.AreEqual(sd2.ParentGuid, sd3.ParentGuid);
        }

        [Test]
        public void SetSiteDay()
        {
            DateTime now = DateTime.Now.AddYears(-2).Date;
            m_Manager.SetSiteDayFiles(m_SiteGuid, now, 5);

            m_Manager.SetSiteDayFiles(m_SiteGuid, now, 4);

            var siteDay = m_Manager.GetSiteDay(m_SiteGuid, now);
            Assert.IsNotNull(siteDay);

            Assert.AreEqual(siteDay.DataFileCount, 4);
        }

        [Test]
        public void AddToSiteDay()
        {
            DateTime now = DateTime.Now.AddYears(-1).Date;
            m_Manager.SetSiteDayFiles(m_SiteGuid, now, 5);

            m_Manager.AddFileToSiteDay(m_SiteGuid, now);

            var siteDay = m_Manager.GetSiteDay(m_SiteGuid, now);
            Assert.IsNotNull(siteDay);

            Assert.AreEqual(siteDay.DataFileCount, 6);
        }

        [Test]
        public void AddToSiteDay2()
        {
            DateTime now = DateTime.Now.AddYears(-10).Date;

            m_Manager.AddFileToSiteDay(m_SiteGuid, now);
            m_Manager.AddFileToSiteDay(m_SiteGuid, now);

            var siteDay = m_Manager.GetSiteDay(m_SiteGuid, now);
            Assert.IsNotNull(siteDay);

            Assert.AreEqual(siteDay.DataFileCount, 2);
        }

        [Test]
        public void GetSiteDays()
        {
            DateTime now = DateTime.Now.AddYears(-1).Date;
            m_Manager.SetSiteDayFiles(m_SiteGuid, now.AddDays(-2), 5);
            m_Manager.SetSiteDayFiles(m_SiteGuid, now.AddDays(-1), 6);
            m_Manager.SetSiteDayFiles(m_SiteGuid, now, 7);

            List<SiteDay> siteDays = m_Manager.GetSiteDays(m_SiteGuid);

            bool foundMinus2 = false;
            bool foundMinus1 = false;
            bool foundToday = false;

            foreach (var siteDay in siteDays)
            {
                if (siteDay.Date == now.AddDays(-2))
                {
                    foundMinus2 = true;
                }

                if (siteDay.Date == now.AddDays(-1))
                {
                    foundMinus1 = true;
                }

                if (siteDay.Date == now)
                {
                    foundToday = true;
                }
            }

            Assert.IsTrue(foundMinus2);
            Assert.IsTrue(foundMinus1);
            Assert.IsTrue(foundToday);
        }
    }
}
