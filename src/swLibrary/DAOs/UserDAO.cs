using MongoDB.Driver;
using Sidwatch.Library.Objects;
using TreeGecko.Library.Net.DAOs;

namespace Sidwatch.Library.DAOs
{
    internal class UserDAO :TGUserDAO
    {
        public UserDAO(MongoDatabase _mongoDB) : base(_mongoDB)
        {
        }

        public User GetUser(string _username)
        {
            return GetOneItem<User>("Username", _username);
        }
    }
}
