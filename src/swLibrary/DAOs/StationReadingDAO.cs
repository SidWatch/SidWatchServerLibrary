using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using Sidwatch.Library.Objects;
using TreeGecko.Library.Mongo.DAOs;

namespace Sidwatch.Library.DAOs
{
    internal class StationReadingDAO: AbstractMongoDAO<StationReading>
    {
        public StationReadingDAO(MongoDatabase _mongoDB)
            : base(_mongoDB)
        {
            //Site
            HasParent = true;
        }

        public override string TableName
        {
            get { return "StationReadings"; }
        }

        public override void BuildTable()
        {
            base.BuildTable();

            string[] columns = { "FileGuid", "ReadingDateTime" };
            BuildNonuniqueIndex(columns, "FILE_DATETIME");

            columns =new [] { "StationGuid", "ReadingDateTime" };
            BuildNonuniqueIndex(columns, "STATION_DATETIME");

            columns = new[] { "ParentGuid", "ReadingDateTime" };
            BuildNonuniqueIndex(columns, "SITE_DATETIME");

            columns = new[] { "ParentGuid", "StationGuid", "ReadingDateTime" };
            BuildNonuniqueIndex(columns, "SITE_STATION_DATETIME");
        }

        public List<StationReading> GetReadings(
            Guid _stationGuid,
            Guid _siteGuid,
            DateTime _startDateTime,
            DateTime _endDateTime)
        {
            IMongoQuery query = Query.And(
                Query.EQ("ParentGuid", new BsonString(_siteGuid.ToString())),
                Query.EQ("StationGuid", new BsonString(_stationGuid.ToString())),
                Query.GTE("ReadingDateTime", new BsonString(_startDateTime.ToString("u"))),
                Query.LTE("ReadingDateTime", new BsonString(_endDateTime.ToString("u"))));

            return GetList(query);
        }
    }
}
