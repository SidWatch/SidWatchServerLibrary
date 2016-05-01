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
            HasParent = false;
        }

        public override string TableName
        {
            get { return "Stations"; }
        }
    }
}
