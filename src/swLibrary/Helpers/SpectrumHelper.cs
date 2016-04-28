using System;
using System.Collections.Generic;
using Sidwatch.Library.JsonObjects;

namespace Sidwatch.Library.Helpers
{
    public static class SpectrumHelper
    {
        public static Spectrum GetSpectrumFromDataset(
            Guid _spectrumId,
            DateTime _readingDateTime,
            Guid _siteId,
            float[,] _values)
        {
            int rows = _values.GetLength(0);

            if (rows != 2)
            {
                throw new ArgumentOutOfRangeException("_values", "Only 2 rows expected in array");
            }

            int bins = _values.GetLength(1);

            var spectrum = new Spectrum
            {
                Id = _spectrumId, 
                ReadingDateTime = _readingDateTime, 
                SiteId = _siteId
            };

            List<FrequencyVector> values = new List<FrequencyVector>();

            for (int i = 0; i < bins; i++)
            {
                FrequencyVector vector = new FrequencyVector
                {
                    Frequency = _values[0, i], 
                    Magnitude = _values[1, i]
                };

                values.Add(vector);
            }

            spectrum.Values = values;

            return spectrum;
        }


    }
}
