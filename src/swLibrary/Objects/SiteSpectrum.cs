using System;
using TreeGecko.Library.Common.Objects;

namespace Sidwatch.Library.Objects
{
    public class SiteSpectrum : AbstractTGObject
    {
        //Parent - Site
        public DateTime ReadingDateTime { get; set; }
        public Double SamplesPerSecond { get; set; }
        public Double NFFT { get; set; }
        public int SamplingFormat { get; set; }
        public Guid DataFileGuid { get; set; }
        public string FileUrl { get; set; }

        public override TGSerializedObject GetTGSerializedObject()
        {
            TGSerializedObject tgs = base.GetTGSerializedObject();

            tgs.Add("ReadingDateTime", ReadingDateTime);
            tgs.Add("SamplesPerSecond", SamplesPerSecond);
            tgs.Add("NFFT", NFFT);
            tgs.Add("SamplingFormat", SamplingFormat);
            tgs.Add("DataFileGuid", DataFileGuid);
            tgs.Add("FileUrl", FileUrl);

            return tgs;
        }

        public override void LoadFromTGSerializedObject(TGSerializedObject _tgs)
        {
            base.LoadFromTGSerializedObject(_tgs);

            ReadingDateTime = _tgs.GetDateTime("ReadingDateTime");
            SamplesPerSecond = _tgs.GetDouble("SamplesPerSecond");
            NFFT = _tgs.GetDouble("NFFT");
            SamplingFormat = _tgs.GetInt32("SamplingFormat");
            DataFileGuid = _tgs.GetGuid("DataFileGuid");
            FileUrl = _tgs.GetString("FileUrl");
        }
    }
}
