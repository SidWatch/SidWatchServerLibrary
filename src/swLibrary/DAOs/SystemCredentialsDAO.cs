using System;
using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using Sidwatch.Library.Objects;
using TreeGecko.Library.Mongo.DAOs;

namespace Sidwatch.Library.DAOs
{
    internal class SystemCredentialsDAO: AbstractMongoDAO<SystemCredentials>
    {
        public SystemCredentialsDAO(MongoDatabase _mongoDB)
            : base(_mongoDB)
        {
            HasParent = false;
        }

        public SystemCredentials GetLatestActive()
        {
            IMongoQuery query = GetQuery("Active", Convert.ToString(true));
            MongoCursor cursor = GetCursor(query).SetSortOrder(SortBy.Descending("CreatedDateTime"));
            
            List<SystemCredentials> list = GetList(cursor);
            if (list.Count >= 1)
            {
                return list[0];
            }

            return null;
        }

        public override string TableName
        {
            get { return "SystemCredentials"; }
        }

    }
}
