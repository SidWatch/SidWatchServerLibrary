using System;
using System.IO;
using TreeGecko.Library.Common.Objects;

namespace Sidwatch.Library.Objects
{
    public class DataFile : AbstractTGObject
    {
        public DateTime DateTime { get; set; }
        public string Filename { get; set; }
        public bool Processed { get; set; }
        public bool Archived { get; set; }
        public bool Available { get; set; }

        public override TGSerializedObject GetTGSerializedObject()
        {
            TGSerializedObject tgs = base.GetTGSerializedObject();

            tgs.Add("DateTime", DateTime);
            tgs.Add("Filename", Filename);
            tgs.Add("Processed", Processed);
            tgs.Add("Archived", Archived);
            tgs.Add("Available", Available);

            return tgs;
        }

        public override void LoadFromTGSerializedObject(TGSerializedObject _tgs)
        {
            base.LoadFromTGSerializedObject(_tgs);

            DateTime = _tgs.GetDateTime("DateTime");
            Filename = _tgs.GetString("Filename");
            Processed = _tgs.GetBoolean("Processed");
            Archived = _tgs.GetBoolean("Archived");
            Available = _tgs.GetBoolean("Available");
        }
    }
}
