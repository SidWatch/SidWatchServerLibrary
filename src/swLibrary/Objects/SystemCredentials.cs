using System;
using TreeGecko.Library.Common.Objects;

namespace Sidwatch.Library.Objects
{
    public class SystemCredentials : AbstractTGObject
    {
        public string AccessKey { get; set; }

        public string SecretKey { get; set; }

        public string BucketName { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public DateTime ExpirationDateTime { get; set; }

        public override TGSerializedObject GetTGSerializedObject()
        {
            TGSerializedObject tgs = base.GetTGSerializedObject();

            tgs.Add("AccessKey", AccessKey);
            tgs.Add("SecretKey", SecretKey);
            tgs.Add("BucketName", BucketName);
            tgs.Add("CreatedDateTime", CreatedDateTime);
            tgs.Add("ExpirationDateTime", ExpirationDateTime);

            return tgs;
        }

        public override void LoadFromTGSerializedObject(TGSerializedObject _tgs)
        {
            base.LoadFromTGSerializedObject(_tgs);

            AccessKey = _tgs.GetString("AccessKey");
            SecretKey = _tgs.GetString("SecretKey");
            BucketName = _tgs.GetString("BucketName");
            CreatedDateTime = _tgs.GetDateTime("CreatedDateTime");
            ExpirationDateTime = _tgs.GetDateTime("ExpirationDateTime");
        }
    }
}
