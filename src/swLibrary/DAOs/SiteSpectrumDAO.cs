using MongoDB.Driver;
using Sidwatch.Library.Objects;
using TreeGecko.Library.Mongo.DAOs;

namespace Sidwatch.Library.DAOs
{
    public class SiteSpectrumDAO: AbstractMongoDAO<SiteSpectrum>
    {
        public SiteSpectrumDAO(MongoDatabase _mongoDB) 
            : base(_mongoDB)
        {
            //Site
            HasParent = true;
        }

        public override string TableName
        {
            get { return "SiteSpectrums"; }
        }
    }
}
