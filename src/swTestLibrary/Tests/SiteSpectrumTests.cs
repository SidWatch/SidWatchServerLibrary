using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Sidwatch.Library.Managers;
using Sidwatch.Library.Objects;
using TreeGecko.Library.Common.Objects;
using TreeGecko.Library.Common.Security;
using TreeGecko.Library.Geospatial.Objects;

namespace swTestLibrary.Tests
{
    [TestFixture]
    public class SiteSpectrumTests
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
            SiteSpectrum spectrum = new SiteSpectrum
            {
                Active = true,
                DataFileGuid = Guid.NewGuid(),
                FileUrl = "/test/file/name",
                Guid = Guid.NewGuid(),
                NFFT = 1024,
                ParentGuid = m_SiteGuid,
                ReadingDateTime = DateTime.Now,
                SamplesPerSecond = 96000,
                SamplingFormat = 32
            };

            TGSerializedObject tgs = spectrum.GetTGSerializedObject();

            var spectrum2 = new SiteSpectrum();
            spectrum2.LoadFromTGSerializedObject(tgs);

            Assert.AreEqual(spectrum.Active, spectrum2.Active);
            Assert.AreEqual(spectrum.DataFileGuid, spectrum2.DataFileGuid);
            Assert.AreEqual(spectrum.FileUrl, spectrum2.FileUrl);
            Assert.AreEqual(spectrum.Guid, spectrum2.Guid);
            Assert.AreEqual(spectrum.NFFT, spectrum2.NFFT);
            Assert.AreEqual(spectrum.ParentGuid, spectrum2.ParentGuid);
            Assert.AreEqual(spectrum.ReadingDateTime, spectrum2.ReadingDateTime);
            Assert.AreEqual(spectrum.SamplesPerSecond, spectrum2.SamplesPerSecond);
            Assert.AreEqual(spectrum.SamplingFormat, spectrum2.SamplingFormat);
        }

        [Test]
        public void AddSpectrumTest()
        {
            SiteSpectrum spectrum = new SiteSpectrum
            {
                Active = true,
                DataFileGuid = Guid.NewGuid(),
                Guid = Guid.NewGuid(),
                NFFT = 1024,
                ParentGuid = m_SiteGuid,
                ReadingDateTime = DateTime.Now,
                SamplesPerSecond = 96000,
                SamplingFormat = 32
            };

            m_Manager.Persist(spectrum);
        }

        [Test]
        public void UpdateSpectrumTest()
        {
            SiteSpectrum spectrum = new SiteSpectrum
            {
                Active = true,
                DataFileGuid = Guid.NewGuid(),
                FileUrl = "/test/file/name",
                Guid = Guid.NewGuid(),
                NFFT = 1024,
                ParentGuid = m_SiteGuid,
                ReadingDateTime = DateTime.Now,
                SamplesPerSecond = 96000,
                SamplingFormat = 32
            };

            m_Manager.Persist(spectrum);

            SiteSpectrum spectrum2 = m_Manager.GetSiteSpectrum(spectrum.Guid);
            Assert.IsNotNull(spectrum2);

            spectrum2.FileUrl = "/test/file/name/2";
            m_Manager.Persist(spectrum2);

            SiteSpectrum spectrum3 = m_Manager.GetSiteSpectrum(spectrum.Guid);

            Assert.IsNotNull(spectrum3);
            Assert.AreEqual(spectrum2.Active, spectrum3.Active);
            Assert.AreEqual(spectrum2.DataFileGuid, spectrum3.DataFileGuid);
            Assert.AreEqual(spectrum2.FileUrl, spectrum3.FileUrl);
            Assert.AreEqual(spectrum2.Guid, spectrum3.Guid);
            Assert.AreEqual(spectrum2.NFFT, spectrum3.NFFT);
            Assert.AreEqual(spectrum2.ParentGuid, spectrum3.ParentGuid);
            Assert.AreEqual(spectrum2.ReadingDateTime, spectrum3.ReadingDateTime);
            Assert.AreEqual(spectrum2.SamplesPerSecond, spectrum3.SamplesPerSecond);
            Assert.AreEqual(spectrum2.SamplingFormat, spectrum3.SamplingFormat);
        }

    }
}