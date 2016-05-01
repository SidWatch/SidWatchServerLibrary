using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver;
using Sidwatch.Library.Objects;
using TreeGecko.Library.Mongo.DAOs;

namespace Sidwatch.Library.DAOs
{
    internal class StationReadingDAO: AbstractMongoDAO<StationReading>
    {
        public StationReadingDAO(MongoDatabase _mongoDB)
            : base(_mongoDB)
        {
            //Station
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

        }
    }
}
