using MongoDB.Driver;
using Sidwatch.Library.Objects;
using TreeGecko.Library.Mongo.DAOs;

namespace Sidwatch.Library.DAOs
{
    public class UserDAO : AbstractMongoDAO<User>
    {
        public UserDAO(MongoDatabase _mongoDB) : base(_mongoDB)
        {
            HasParent = false;
        }
        
        public override string TableName
        {
            get { return "TGUsers"; }
        }

        public override void BuildTable()
        {
            base.BuildTable();

            BuildUniqueIndex("Username", "USERNAME");
            BuildUniqueSparceIndex("EmailAddress", "EMAIL");
        }

        public User Get(string _username)
        {
            return GetOneItem<User>("Username", _username);
        }

        public User GetByEmail(string _emailAddress)
        {
            return GetOneItem<User>("EmailAddress", _emailAddress);
        }
        
        public User GetUser(string _username)
        {
            return GetOneItem<User>("Username", _username);
        }
    }
}
