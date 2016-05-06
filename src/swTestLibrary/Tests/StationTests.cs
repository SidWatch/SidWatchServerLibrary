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
    public class StationTests
    {
        private readonly SidWatchManager m_Manager = new SidWatchManager();

        [Test]
        public void SerializationTest()
        {
            Station station = new Station
            {
                Active = true,
                CallSign = RandomString.GetRandomString(6),
                Country = RandomString.GetRandomString(3),
                Frequency = 12330,
                Guid = Guid.NewGuid(),
                Location = new GeoPoint(-110, 45),
                Notes = RandomString.GetRandomString(40)
            };

            TGSerializedObject tgs = station.GetTGSerializedObject();

            Station station2 = new Station();
            station2.LoadFromTGSerializedObject(tgs);

            Assert.AreEqual(station.Active, station2.Active);
            Assert.AreEqual(station.CallSign, station2.CallSign);
            Assert.AreEqual(station.Country, station2.Country);
            Assert.AreEqual(station.Frequency, station2.Frequency);
            Assert.AreEqual(station.Guid, station2.Guid);
            Assert.AreEqual(station.Location.X, station2.Location.X);
            Assert.AreEqual(station.Location.Y, station2.Location.Y);
            Assert.AreEqual(station.Notes, station2.Notes);
        }

        [Test]
        public void AddStation()
        {
            Station station = new Station
            {
                Active = true,
                CallSign = RandomString.GetRandomString(6),
                Country = RandomString.GetRandomString(3),
                Frequency = 12330,
                Guid = Guid.NewGuid(),
                Location = new GeoPoint(-110, 45),
                Notes = RandomString.GetRandomString(40)
            };

            m_Manager.Persist(station);
        }

        [Test]
        public void GetStation()
        {
            Station station = new Station
            {
                Active = true,
                CallSign = RandomString.GetRandomString(6),
                Country = RandomString.GetRandomString(3),
                Frequency = 12330,
                Guid = Guid.NewGuid(),
                Location = new GeoPoint(-110, 45),
                Notes = RandomString.GetRandomString(40)
            };

            m_Manager.Persist(station);

            Station station2 = m_Manager.GetStation(station.Guid);
            Assert.IsNotNull(station2);
        }

        [Test]
        public void UpdateStation()
        {
            Station station = new Station
            {
                Active = true,
                CallSign = RandomString.GetRandomString(6),
                Country = RandomString.GetRandomString(3),
                Frequency = 12330,
                Guid = Guid.NewGuid(),
                Location = new GeoPoint(-110, 45),
                Notes = RandomString.GetRandomString(40)
            };

            m_Manager.Persist(station);

            Station station2 = m_Manager.GetStation(station.Guid);
            Assert.IsNotNull(station2);

            station2.Frequency = 14400;
            m_Manager.Persist(station2);

            Station station3 = m_Manager.GetStation(station.Guid);
            Assert.IsNotNull(station3);
            Assert.AreEqual(station2.Frequency, station3.Frequency);
        }

        [Test]
        public void GetStations()
        {
            Station station1 = new Station
            {
                Active = true,
                CallSign = RandomString.GetRandomString(6),
                Country = RandomString.GetRandomString(3),
                Frequency = 12330,
                Guid = Guid.NewGuid(),
                Location = new GeoPoint(-110, 45),
                Notes = RandomString.GetRandomString(40)
            };
            m_Manager.Persist(station1);

            Station station2 = new Station
            {
                Active = false,
                CallSign = RandomString.GetRandomString(6),
                Country = RandomString.GetRandomString(3),
                Frequency = 12330,
                Guid = Guid.NewGuid(),
                Location = new GeoPoint(-110, 45),
                Notes = RandomString.GetRandomString(40)
            };
            m_Manager.Persist(station2);

            List<Station> stations = m_Manager.GetStations();

            bool has1 = false;
            bool has2 = false;

            foreach (var station in stations)
            {
                if (station.Guid == station1.Guid)
                {
                    has1 = true;
                }

                if (station.Guid == station2.Guid)
                {
                    has2 = true;
                }
            }
            Assert.IsTrue(has1);
            Assert.IsTrue(has2);

            stations = m_Manager.GetActiveStations();

            has1 = false;
            has2 = false;

            foreach (var station in stations)
            {
                if (station.Guid == station1.Guid)
                {
                    has1 = true;
                }

                if (station.Guid == station2.Guid)
                {
                    has2 = true;
                }
            }
            Assert.IsTrue(has1);
            Assert.IsTrue(!has2);
        }
    }
}
