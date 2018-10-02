using System;

namespace InstagramParser
{
    public class QueryConstructor
    {
        private readonly string queryTemplate = Utility.BaseUrl +
                                       "/graphql/query/?query_hash={0}&variables={{\"id\":\"{1}\",\"first\":{2},\"after\":\"";



        public QueryConstructor(string hash, string id, string first)
        {
            queryTemplate = String.Format(queryTemplate, hash, id, first);
        }



        public string GetFullQuery(string after)
        {
            return queryTemplate + after + "\"}";
        }
    }
}