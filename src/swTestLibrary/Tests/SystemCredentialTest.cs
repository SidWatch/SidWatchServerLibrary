using System;
using System.Collections.Generic;
using NUnit.Framework;
using Sidwatch.Library.Managers;
using Sidwatch.Library.Objects;
using TreeGecko.Library.Common.Objects;
using TreeGecko.Library.Common.Security;

namespace swTestLibrary.Tests
{
    [TestFixture]
    public class SystemCredentialTest
    {
        private readonly SidWatchManager m_Manager = new SidWatchManager();

        [Test]
        public void SerializationTest()
        {
            SystemCredentials sc = new SystemCredentials
            {
                AccessKey = RandomString.GetRandomString(10),
                Active = true,
                BucketName = RandomString.GetRandomString(10),
                CreatedDateTime = DateTime.Now,
                Guid = Guid.NewGuid(),
                SecretKey = RandomString.GetRandomString(10)
            };

            TGSerializedObject tgs = sc.GetTGSerializedObject();

            SystemCredentials sc2 = new SystemCredentials();
            sc2.LoadFromTGSerializedObject(tgs);

            Assert.AreEqual(sc.AccessKey, sc2.AccessKey);
            Assert.AreEqual(sc.Active, sc2.Active);
            Assert.AreEqual(sc.BucketName, sc2.BucketName);
            Assert.AreEqual(sc.CreatedDateTime, sc.CreatedDateTime);
            Assert.AreEqual(sc.Guid, sc2.Guid);
            Assert.AreEqual(sc.SecretKey, sc2.SecretKey);
        }

        [Test]
        public void AddTest()
        {
            SystemCredentials sc = new SystemCredentials
            {
                AccessKey = RandomString.GetRandomString(10),
                Active = true,
                BucketName = RandomString.GetRandomString(10),
                CreatedDateTime = DateTime.Now,
                Guid = Guid.NewGuid(),
                SecretKey = RandomString.GetRandomString(10)
            };

            m_Manager.Persist(sc);
        }

        [Test]
        public void GetTest()
        {
            SystemCredentials sc = new SystemCredentials
            {
                AccessKey = RandomString.GetRandomString(10),
                Active = true,
                BucketName = RandomString.GetRandomString(10),
                CreatedDateTime = DateTime.Now,
                Guid = Guid.NewGuid(),
                SecretKey = RandomString.GetRandomString(10)
            };

            m_Manager.Persist(sc);

            SystemCredentials sc2 = m_Manager.GetSystemCredential(sc.Guid);
            Assert.IsNotNull(sc2);
        }

        [Test]
        public void UpdateTest()
        {
            SystemCredentials sc = new SystemCredentials
            {
                AccessKey = RandomString.GetRandomString(10),
                Active = true,
                BucketName = RandomString.GetRandomString(10),
                CreatedDateTime = DateTime.Now,
                Guid = Guid.NewGuid(),
                SecretKey = RandomString.GetRandomString(10)
            };

            m_Manager.Persist(sc);

            SystemCredentials sc2 = m_Manager.GetSystemCredential(sc.Guid);
            Assert.IsNotNull(sc2);

            sc2.Active = false;
            m_Manager.Persist(sc2);

            SystemCredentials sc3 = m_Manager.GetSystemCredential(sc.Guid);
            Assert.IsNotNull(sc3);
            Assert.IsFalse(sc3.Active);
        }

        [Test]
        public void GetAll()
        {
            SystemCredentials sc1 = new SystemCredentials
            {
                AccessKey = RandomString.GetRandomString(10),
                Active = true,
                BucketName = RandomString.GetRandomString(10),
                CreatedDateTime = DateTime.Now,
                Guid = Guid.NewGuid(),
                SecretKey = RandomString.GetRandomString(10)
            };
            m_Manager.Persist(sc1);

            SystemCredentials sc2 = new SystemCredentials
            {
                AccessKey = RandomString.GetRandomString(10),
                Active = false,
                BucketName = RandomString.GetRandomString(10),
                CreatedDateTime = DateTime.Now.AddDays(-1),
                Guid = Guid.NewGuid(),
                SecretKey = RandomString.GetRandomString(10)
            };
            m_Manager.Persist(sc2);

            List<SystemCredentials> all = m_Manager.GetSystemCredentials();

            bool has1 = false;
            bool has2 = false;

            foreach (var sc in all)
            {
                if (sc.Guid == sc1.Guid)
                {
                    has1 = true;
                }

                if (sc.Guid == sc2.Guid)
                {
                    has2 = true;
                }
            }

            Assert.IsTrue(has1);
            Assert.IsTrue(has2);
        }

        [Test]
        public void GetActive()
        {
            SystemCredentials sc1 = new SystemCredentials
            {
                AccessKey = RandomString.GetRandomString(10),
                Active = true,
                BucketName = RandomString.GetRandomString(10),
                CreatedDateTime = DateTime.Now,
                Guid = Guid.NewGuid(),
                SecretKey = RandomString.GetRandomString(10)
            };
            m_Manager.Persist(sc1);

            SystemCredentials sc2 = new SystemCredentials
            {
                AccessKey = RandomString.GetRandomString(10),
                Active = false,
                BucketName = RandomString.GetRandomString(10),
                CreatedDateTime = DateTime.Now.AddDays(-1),
                Guid = Guid.NewGuid(),
                SecretKey = RandomString.GetRandomString(10)
            };
            m_Manager.Persist(sc2);

            List<SystemCredentials> all = m_Manager.GetActiveSystemCredentials();

            bool has1 = false;
            bool has2 = false;

            foreach (var sc in all)
            {
                if (sc.Guid == sc1.Guid)
                {
                    has1 = true;
                }

                if (sc.Guid == sc2.Guid)
                {
                    has2 = true;
                }
            }

            Assert.IsTrue(has1);
            Assert.IsFalse(has2);
        }

        [Test]
        public void GetLatestActive()
        {
            SystemCredentials sc1 = new SystemCredentials
            {
                AccessKey = RandomString.GetRandomString(10),
                Active = true,
                BucketName = RandomString.GetRandomString(10),
                CreatedDateTime = DateTime.Now.AddMinutes(5),
                Guid = Guid.NewGuid(),
                SecretKey = RandomString.GetRandomString(10)
            };
            m_Manager.Persist(sc1);

            SystemCredentials sc2 = new SystemCredentials
            {
                AccessKey = RandomString.GetRandomString(10),
                Active = false,
                BucketName = RandomString.GetRandomString(10),
                CreatedDateTime = DateTime.Now.AddDays(-1),
                Guid = Guid.NewGuid(),
                SecretKey = RandomString.GetRandomString(10)
            };
            m_Manager.Persist(sc2);

            SystemCredentials latest = m_Manager.GetLatestActive();
            Assert.IsNotNull(latest);
            Assert.AreEqual(latest.Guid, sc1.Guid);
        }

        [Test]
        public void GetLatestActiveNull()
        {
            List<SystemCredentials> active = m_Manager.GetActiveSystemCredentials();

            foreach (var systemCredential in active)
            {
                systemCredential.Active = false;
                m_Manager.Persist(systemCredential);
            }

            SystemCredentials sc = m_Manager.GetLatestActive();
            Assert.IsNull(sc);
        }
    }
}
