using TreeGecko.Library.Common.Objects;
using TreeGecko.Library.Geospatial.Objects;
using TreeGecko.Library.Geospatial.Extensions;

namespace Sidwatch.Library.Objects
{
    public class Station : AbstractTGObject
    {
        public string CallSign { get; set; }
        public string Country { get; set; }
        public string Notes { get; set; }
        public double Frequency { get; set; }
        public GeoPoint Location { get; set; }

        public override TGSerializedObject GetTGSerializedObject()
        {
            TGSerializedObject tgs = base.GetTGSerializedObject();

            tgs.Add("CallSign", CallSign);
            tgs.Add("Country", Country);
            tgs.Add("Notes", Notes);
            tgs.Add("Frequency", Frequency);
            tgs.Add("Location", Location);

            return tgs;
        }

        public override void LoadFromTGSerializedObject(TGSerializedObject _tgs)
        {
            base.LoadFromTGSerializedObject(_tgs);

            CallSign = _tgs.GetString("CallSign");
            Country = _tgs.GetString("Country");
            Notes = _tgs.GetString("Notes");
            Frequency = _tgs.GetDouble("Frequency");
            Location = _tgs.GetGeoPoint("Location");
        }
    }
}
