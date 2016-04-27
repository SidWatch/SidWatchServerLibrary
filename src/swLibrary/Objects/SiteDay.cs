using System;
using TreeGecko.Library.Common.Objects;

namespace Sidwatch.Library.Objects
{
    public class SiteDay : AbstractTGObject
    {
        //Parent - Site
        public DateTime Date { get; set; }
        public int DateFiles { get; set; }
    }
}
