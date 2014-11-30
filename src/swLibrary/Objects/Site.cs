using System;
using TreeGecko.Library.Common.Objects;
using TreeGecko.Library.Geospatial.Objects;
using TreeGecko.Library.Geospatial.Extensions;

namespace Sidwatch.Library.Objects
{
    public class Site : AbstractTGObject
    {
        public int MonitorID { get; set; }
        public string Name { get; set; }
        public string Timezone { get; set; }
        public double UTCOffset { get; set; }
        public GeoPoint Location { get; set; }
        public Guid SiteOwnerGuid { get; set; }

        public override TGSerializedObject GetTGSerializedObject()
        {
            TGSerializedObject tgs = base.GetTGSerializedObject();

            tgs.Add("MonitorID", MonitorID);
            tgs.Add("Name", Name);
            tgs.Add("Timezone", Timezone);
            tgs.Add("UTCOffset", UTCOffset);
            tgs.Add("Location", Location);
            tgs.Add("SiteOwnerGuid", SiteOwnerGuid);

            return tgs;
        }

        public override void LoadFromTGSerializedObject(TGSerializedObject _tgs)
        {
            base.LoadFromTGSerializedObject(_tgs);

            MonitorID = _tgs.GetInt32("MonitorID");
            Name = _tgs.GetString("Name");
            Timezone = _tgs.GetString("Timezone");
            UTCOffset = _tgs.GetDouble("UTCOffset");
            Location = _tgs.GetGeoPoint("Location");
            SiteOwnerGuid = _tgs.GetGuid("SiteOwnerGuid");

        }
    }
}
