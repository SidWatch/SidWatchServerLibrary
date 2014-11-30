using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver;
using Sidwatch.Library.Objects;
using TreeGecko.Library.Mongo.DAOs;

namespace Sidwatch.Library.DAOs
{
    internal class StationDAO : AbstractMongoDAO<Station>
    {
        public StationDAO(MongoDatabase _mongoDB)
            : base(_mongoDB)
        {
        }

        public override string TableName
        {
            get { return "Stations"; }
        }

        public override void BuildTable()
        {
            base.BuildTable();
        }
    }
}
