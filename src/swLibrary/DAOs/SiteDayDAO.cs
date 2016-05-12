using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using MongoDB.Bson;
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
            //Site
            HasParent = true;
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

        public List<SiteDay> GetSiteDays(Guid _siteGuid, DateTime _startDate)
        {
            IMongoQuery query = Query.And(
                Query.EQ("ParentGuid", new BsonString(_siteGuid.ToString())),
                Query.GTE("Date", new BsonString(DateHelper.ToString(_startDate))));

            MongoCursor cursor = GetCursor(query);
            cursor.SetSortOrder(SortBy.Ascending("Date"));

            return GetList(cursor);
        }

        public List<SiteDay> GetSiteDays(Guid _siteGuid, DateTime _startDate, DateTime _endDate)
        {
            
            IMongoQuery query = Query.And(
                Query.EQ("ParentGuid", new BsonString(_siteGuid.ToString())),
                Query.GTE("Date", new BsonString(DateHelper.ToString(_startDate))),
                Query.LTE("Date", new BsonString(DateHelper.ToString(_endDate))));

            MongoCursor cursor = GetCursor(query);
            cursor.SetSortOrder(SortBy.Ascending("Date"));

            return GetList(cursor);
        }

        public SiteDay GetMaxSiteDay(Guid _siteGuid)
        {
            IMongoQuery query = GetQuery("ParentGuid", _siteGuid.ToString());
            MongoCursor cursor = GetCursor(query);
            cursor.SetSortOrder(SortBy.Descending("Date"));

            return GetOneItem<SiteDay>(cursor);
        }

        public SiteDay GetMinSiteDay(Guid _siteGuid)
        {
            IMongoQuery query = GetQuery("ParentGuid", _siteGuid.ToString());
            MongoCursor cursor = GetCursor(query);
            cursor.SetSortOrder(SortBy.Ascending("Date"));

            return GetOneItem<SiteDay>(cursor);
        }

    }
}
