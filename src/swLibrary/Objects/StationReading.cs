using System;
using TreeGecko.Library.Common.Objects;

namespace Sidwatch.Library.Objects
{
    public class StationReading : AbstractTGObject
    {
        //Parent Site
        public Guid StationGuid { get; set; }
        public DateTime ReadingDateTime { get; set; }
        public double ReadingMagnitude { get; set; }
        public Guid FileGuid { get; set; }

        public override TGSerializedObject GetTGSerializedObject()
        {
            TGSerializedObject tgs = base.GetTGSerializedObject();

            tgs.Add("StationGuid", StationGuid);
            tgs.Add("ReadingDateTime", ReadingDateTime);
            tgs.Add("ReadingMagnitude", ReadingMagnitude);
            tgs.Add("FileGuid", FileGuid);

            return tgs;
        }

        public override void LoadFromTGSerializedObject(TGSerializedObject _tgs)
        {
            base.LoadFromTGSerializedObject(_tgs);

            StationGuid = _tgs.GetGuid("StationGuid");
            ReadingDateTime = _tgs.GetDateTime("ReadingDateTime");
            ReadingMagnitude = _tgs.GetDouble("ReadingMagnitude");
            FileGuid = _tgs.GetGuid("FileGuid");
        }
    }
}
