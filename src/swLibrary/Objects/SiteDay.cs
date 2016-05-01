using System;
using TreeGecko.Library.Common.Objects;

namespace Sidwatch.Library.Objects
{
    public class SiteDay : AbstractTGObject
    {
        //Parent - Site
        public DateTime Date { get; set; }
        public int DataFileCount { get; set; }

        public override TGSerializedObject GetTGSerializedObject()
        {
            TGSerializedObject tgs = base.GetTGSerializedObject();

            tgs.Add("Date", Date);
            tgs.Add("DataFileCount", DataFileCount);

            return tgs;
        }

        public override void LoadFromTGSerializedObject(TGSerializedObject _tgs)
        {
            base.LoadFromTGSerializedObject(_tgs);

            Date = _tgs.GetDateTime("Date");
            DataFileCount = _tgs.GetInt32("DataFileCount");
        }
    }
}
