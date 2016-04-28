using System;
using System.Collections.Generic;

namespace Sidwatch.Library.JsonObjects
{
    public class Spectrum
    {
        public Guid Id { get; set; }
        public Guid SiteId { get; set; }
        public DateTime ReadingDateTime { get; set; }
        public List<FrequencyVector> Values { get; set; } 
    }
}
