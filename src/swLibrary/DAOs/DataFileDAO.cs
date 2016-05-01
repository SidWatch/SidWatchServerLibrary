using System;
using System.Collections.Generic;
using MongoDB.Driver;
using Sidwatch.Library.Objects;
using TreeGecko.Library.Mongo.DAOs;

namespace Sidwatch.Library.DAOs
{
    public class DataFileDAO : AbstractMongoDAO<DataFile>
    {
        public DataFileDAO(MongoDatabase _mongoDB) 
            : base(_mongoDB)
        {
            //Site
            HasParent = true;
        }

        public override string TableName
        {
            get { return "DataFile"; }
        }

        public override void BuildTable()
        {
            base.BuildTable();

            string[] columns = {"ParentGuid", "DateTime"};
            BuildNonuniqueIndex(columns, "PARENT_DATETIME");

            BuildNonuniqueIndex("DateTime", "DATETIME");
        }

        public List<DataFile> GetDataFilesBySite(Guid _siteGuid)
        {
            return GetSorted("ParentGuid", _siteGuid.ToString(), "DateTime");
        }
    }
}
