using System;
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
    public class UserAuthenticationTests
    {
        private readonly SidWatchManager m_Manager = new SidWatchManager();

        [Test]
        public void TestSerialization()
        {
            TGUserAuthorization userAuth = new TGUserAuthorization
            {
                Active = true,
                AuthorizationDateTime = DateTime.Now,
                AuthorizationToken = RandomString.GetRandomString(50),
                DeviceType = RandomString.GetRandomString(5),
                Guid = Guid.NewGuid(),
                ParentGuid = Guid.NewGuid(),
                PersistedDateTime = DateTime.Now
            };

            TGSerializedObject tgs = userAuth.GetTGSerializedObject();

            TGUserAuthorization userAuth2 = new TGUserAuthorization();
            userAuth2.LoadFromTGSerializedObject(tgs);

            Assert.AreEqual(userAuth.Active, userAuth2.Active);
            Assert.AreEqual(userAuth.AuthorizationDateTime, userAuth2.AuthorizationDateTime);
            Assert.AreEqual(userAuth.AuthorizationToken, userAuth2.AuthorizationToken);
            Assert.AreEqual(userAuth.DeviceType, userAuth2.DeviceType);
            Assert.AreEqual(userAuth.Guid, userAuth2.Guid);
            Assert.AreEqual(userAuth.ParentGuid, userAuth2.ParentGuid);
            Assert.AreEqual(userAuth.PersistedDateTime, userAuth2.PersistedDateTime);
        }

        [Test]
        public void AddUserAuth()
        {
            User u = new User
            {
                Guid = Guid.NewGuid(),
                UserType = UserTypes.User,
                Username = RandomString.GetRandomString(10)
            };
            m_Manager.Persist(u);

            TGUserAuthorization userAuth =  TGUserAuthorization.GetNew(u.Guid, RandomString.GetRandomString(5));
            m_Manager.Persist(userAuth);
        }

        [Test]
        public void GetUserAuth()
        {
            User u = new User
            {
                Guid = Guid.NewGuid(),
                UserType = UserTypes.User,
                Username = RandomString.GetRandomString(10)
            };
            m_Manager.Persist(u);

            TGUserAuthorization userAuth = TGUserAuthorization.GetNew(u.Guid, RandomString.GetRandomString(5));
            m_Manager.Persist(userAuth);

            TGUserAuthorization userAuth2 = m_Manager.GetUserAuthorization(u.Guid, userAuth.AuthorizationToken);
            Assert.IsNotNull(userAuth2);
        }

        [Test]
        public void VerifyUserToken()
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

            TGUserAuthorization userAuth = TGUserAuthorization.GetNew(user1.Guid, RandomString.GetRandomString(5));
            m_Manager.Persist(userAuth);

            User user2;
            bool result = m_Manager.ValidateUser(user1.Username, userAuth.AuthorizationToken, out user2);
            Assert.IsTrue(result);
            Assert.IsNotNull(user2);

            User user3;
            result = m_Manager.ValidateUser(user1.Username, RandomString.GetRandomString(50), out user3);
            Assert.IsTrue(!result);
            Assert.IsNull(user3);

        }
    }
}
