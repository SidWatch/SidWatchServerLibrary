using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using Sidwatch.Library.Objects;
using TreeGecko.Library.Common.Helpers;
using TreeGecko.Library.Mongo.DAOs;

namespace Sidwatch.Library.DAOs
{
    public class SiteDayDAO : AbstractMongoDAO<SiteDay>
    {
        public SiteDayDAO(MongoDatabase _mongoDB)
            : base(_mongoDB)
        {
        }

        public override string TableName
        {
            get { return "SiteDay"; }
        }

        public override void BuildTable()
        {
            base.BuildTable();

            string[] columns = { "ParentGuid", "Date" };
            BuildNonuniqueIndex(columns, "SITE_DATE");
        }

        public SiteDay Get(Guid _siteGuid, DateTime _date)
        {
            NameValueCollection nvc = new NameValueCollection
            {
                {"ParentGuid", _siteGuid.ToString()},
                {"Date", DateHelper.ToString(_date)}
            };

            return GetOneItem<SiteDay>(nvc);
        }

        public List<SiteDay> GetSiteDays(Guid _siteGuid)
        {
            return GetSorted("ParentGuid", _siteGuid.ToString(), "Date");
        }
    }
}
