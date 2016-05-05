using System;
using System.Collections.Generic;
using NUnit.Framework;
using Sidwatch.Library.Managers;
using Sidwatch.Library.Objects;
using TreeGecko.Library.Common.Enums;
using TreeGecko.Library.Common.Objects;
using TreeGecko.Library.Common.Security;
using TreeGecko.Library.Net.Objects;

namespace swTestLibrary.Tests
{
    [TestFixture]
    public class UserTests
    {
        private SidWatchManager m_Manager = new SidWatchManager();

        [OneTimeSetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void TestSerialization()
        {
            User user = new User
            {
                Active = true,
                DisplayName = RandomString.GetRandomString(10),
                EmailAddress = RandomString.GetRandomString(10) + "@" + RandomString.GetRandomString(10) + ".com",
                EulaAccepted = true,
                FamilyName = RandomString.GetRandomString(10),
                GivenName = RandomString.GetRandomString(8),
                Guid = Guid.NewGuid(),
                IsVerified = true,
                ParentGuid = null,
                UserType = UserTypes.User,
                Username = RandomString.GetRandomString(10)
            };

            TGSerializedObject tgs = user.GetTGSerializedObject();

            User user2 = new User();
            user2.LoadFromTGSerializedObject(tgs);

            Assert.AreEqual(user.Active, user2.Active);
            Assert.AreEqual(user.DisplayName, user2.DisplayName);
            Assert.AreEqual(user.EmailAddress, user2.EmailAddress);
            Assert.AreEqual(user.EulaAccepted, user2.EulaAccepted);
            Assert.AreEqual(user.FamilyName, user2.FamilyName);
            Assert.AreEqual(user.GivenName, user2.GivenName);
            Assert.AreEqual(user.Guid, user2.Guid);
            Assert.AreEqual(user.IsVerified, user2.IsVerified);
            Assert.AreEqual(user.ParentGuid, user2.ParentGuid);
            Assert.AreEqual(user.UserType, user2.UserType);
            Assert.AreEqual(user.Username, user2.Username);
        }

        [Test]
        public void AddUser()
        {
            User user = new User
            {
                Active = true,
                DisplayName = RandomString.GetRandomString(10),
                EmailAddress = RandomString.GetRandomString(10) + "@" + RandomString.GetRandomString(10) + ".com",
                EulaAccepted = true,
                FamilyName = RandomString.GetRandomString(10),
                GivenName = RandomString.GetRandomString(8),
                Guid = Guid.NewGuid(),
                IsVerified = true,
                ParentGuid = null,
                UserType = UserTypes.User,
                Username = RandomString.GetRandomString(10)
            };

            m_Manager.Persist(user);
        }

        [Test]
        public void GetUser()
        {
            User user = new User
            {
                Active = true,
                DisplayName = RandomString.GetRandomString(10),
                EmailAddress = RandomString.GetRandomString(10) + "@" + RandomString.GetRandomString(10) + ".com",
                EulaAccepted = true,
                FamilyName = RandomString.GetRandomString(10),
                GivenName = RandomString.GetRandomString(8),
                Guid = Guid.NewGuid(),
                IsVerified = true,
                ParentGuid = null,
                UserType = UserTypes.User,
                Username = RandomString.GetRandomString(10)
            };

            m_Manager.Persist(user);

            User user2 = m_Manager.GetUser(user.Guid);
            Assert.IsNotNull(user2);

            User user3 = m_Manager.GetUser(user.Username);
            Assert.IsNotNull(user3);
        }

        [Test]
        public void UpdateUser()
        {
            User user = new User
            {
                Active = true,
                DisplayName = RandomString.GetRandomString(10),
                EmailAddress = RandomString.GetRandomString(10) + "@" + RandomString.GetRandomString(10) + ".com",
                EulaAccepted = true,
                FamilyName = RandomString.GetRandomString(10),
                GivenName = RandomString.GetRandomString(8),
                Guid = Guid.NewGuid(),
                IsVerified = true,
                ParentGuid = null,
                UserType = UserTypes.User,
                Username = RandomString.GetRandomString(10)
            };

            m_Manager.Persist(user);

            User user2 = m_Manager.GetUser(user.Guid);
            Assert.IsNotNull(user2);

            user2.FamilyName = RandomString.GetRandomString(10);
            m_Manager.Persist(user2);

            User user3 = m_Manager.GetUser(user.Guid);
            Assert.IsNotNull(user3);
            Assert.AreEqual(user3.FamilyName, user2.FamilyName);
        }

        [Test]
        public void GetUsers()
        {
            User user1 = new User
            {
                Active = true,
                DisplayName = RandomString.GetRandomString(10),
                EmailAddress = RandomString.GetRandomString(10) + "@" + RandomString.GetRandomString(10) + ".com",
                EulaAccepted = true,
                FamilyName = RandomString.GetRandomString(10),
                GivenName = RandomString.GetRandomString(8),
                Guid = Guid.NewGuid(),
                IsVerified = true,
                ParentGuid = null,
                UserType = UserTypes.User,
                Username = RandomString.GetRandomString(10)
            };

            m_Manager.Persist(user1);

            User user2 = new User
            {
                Active = false,
                DisplayName = RandomString.GetRandomString(10),
                EmailAddress = RandomString.GetRandomString(10) + "@" + RandomString.GetRandomString(10) + ".com",
                EulaAccepted = true,
                FamilyName = RandomString.GetRandomString(10),
                GivenName = RandomString.GetRandomString(8),
                Guid = Guid.NewGuid(),
                IsVerified = true,
                ParentGuid = null,
                UserType = UserTypes.User,
                Username = RandomString.GetRandomString(10)
            };

            m_Manager.Persist(user2);

            List<User> users = m_Manager.GetUsers();

            bool has1 = false;
            bool has2 = false;

            foreach (var user in users)
            {
                if (user.Guid == user1.Guid)
                {
                    has1 = true;
                }

                if (user.Guid == user2.Guid)
                {
                    has2 = true;
                }
            }

            Assert.IsTrue(has1);
            Assert.IsTrue(has2);

            users = m_Manager.GetActiveUsers();

            has1 = false;
            has2 = false;

            foreach (var user in users)
            {
                if (user.Guid == user1.Guid)
                {
                    has1 = true;
                }

                if (user.Guid == user2.Guid)
                {
                    has2 = true;
                }
            }

            Assert.IsTrue(has1);
            Assert.IsTrue(!has2);
        }

        [Test]
        public void VerifyUserPassword()
        {
            User user1 = new User
            {
                Active = true,
                DisplayName = RandomString.GetRandomString(10),
                EmailAddress = RandomString.GetRandomString(10) + "@" + RandomString.GetRandomString(10) + ".com",
                EulaAccepted = true,
                FamilyName = RandomString.GetRandomString(10),
                GivenName = RandomString.GetRandomString(8),
                Guid = Guid.NewGuid(),
                IsVerified = true,
                ParentGuid = null,
                UserType = UserTypes.User,
                Username = RandomString.GetRandomString(10)
            };
            m_Manager.Persist(user1);

            string password = RandomString.GetRandomString(10);
            TGUserPassword up = TGUserPassword.GetNew(user1.Guid, user1.Username, password);
            m_Manager.Persist(up);

            bool result = m_Manager.ValidateUser(user1, RandomString.GetRandomString(10));
            Assert.IsTrue(!result);

            result = m_Manager.ValidateUser(user1, password);
            Assert.IsTrue(result);
        }



    }
}
