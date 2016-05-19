using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_siteGuid"></param>
        /// <returns></returns>
        public SiteSpectrum GetLatest(Guid _siteGuid)
        {            
            return GetOneItem<SiteSpectrum>("ParentGuid", _siteGuid.ToString(), "ReadingDateTime");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_siteGuid"></param>
        /// <param name="_startDateTime"></param>
        /// <param name="_endDateTime"></param>
        /// <returns></returns>
        public List<SiteSpectrum> GetSiteSpectrums(
            Guid _siteGuid,
            DateTime _startDateTime,
            DateTime _endDateTime)
        {
            IMongoQuery query = Query.And(
                Query.EQ("ParentGuid", new BsonString(_siteGuid.ToString())),
                Query.GTE("ReadingDateTime", new BsonString(_startDateTime.ToString("u"))),
                Query.LTE("ReadingDateTime", new BsonString(_endDateTime.ToString("u"))));

            return GetList(query);
        }
    }
}
