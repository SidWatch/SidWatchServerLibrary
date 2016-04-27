using MongoDB.Driver;
using Sidwatch.Library.Objects;
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
    }
}
