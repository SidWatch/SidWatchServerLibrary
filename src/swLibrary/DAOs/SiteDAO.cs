using MongoDB.Driver;
using Sidwatch.Library.Objects;
using TreeGecko.Library.Mongo.DAOs;

namespace Sidwatch.Library.DAOs
{
    internal class SiteDAO :AbstractMongoDAO<Site>
    {
        public SiteDAO(MongoDatabase _mongoDB) 
            : base(_mongoDB)
        {
            HasParent = false;
        }

        public override string TableName
        {
            get { return "Sites"; }
        }

        public override void BuildTable()
        {
            base.BuildTable();
        }
    }
}
