using System;
using NUnit.Framework;
using Sidwatch.Library.Objects;
using TreeGecko.Library.Common.Objects;
using TreeGecko.Library.Common.Security;

namespace swTestLibrary.Tests
{
    [TestFixture]
    public class SystemCredentialTest
    {
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

    }
}
