using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json.Linq;

namespace InstagramParser
{
    public class PostGetter
    {
        private const int PostNumberLoaded = 50;
        
        private readonly QueryConstructor _queryConstructor;

        private readonly string _gis;
        private readonly string _id;
        private readonly string _first = PostNumberLoaded.ToString();
        
        

        public PostGetter(string gis, string hash, string id)
        {
            _gis = gis;
            _id = id;
            
            _queryConstructor = new QueryConstructor(hash, id, _first);
        }
        
        
        
        public List<Post> GetAllPosts()
        {
            List<Post> posts = new List<Post>();
            
            string after = "";
            JObject queryJson;
            
            
            do
            {
                FileDownloader _fileDownloader = GenerateFileDownloader(after);
                
                //Query
                string query = _queryConstructor.GetFullQuery(after);
                //Post
                queryJson = JObject.Parse(_fileDownloader.DownloadFile(query));
                posts.AddRange(GetPostChunkInfo(queryJson));
                
                after = GetAfter(queryJson);
            } 
            while (HasNextPage(queryJson));

            return posts;
        }
        
        private string GetAfter(JObject queryJson)
        {
            return (string) queryJson["data"]["user"]["edge_owner_to_timeline_media"]["page_info"]["end_cursor"] ?? "";
        }
        
        private bool HasNextPage(JObject queryJson)
        {
            return (bool) queryJson["data"]["user"]["edge_owner_to_timeline_media"]["page_info"]["has_next_page"];
        }

        private FileDownloader GenerateFileDownloader(string after)
        {
            XInstagramGisHash xInstagramGisHash = new XInstagramGisHash(_gis, _id, _first, after);
            FileDownloader _fileDownloader = new FileDownloader(xInstagramGisHash.GenerateXInstagramGisHash());

            return _fileDownloader;
        }
        
        private List<Post> GetPostChunkInfo(JObject postJson)
        {
            var postInfoEdges = (JArray) postJson["data"]["user"]["edge_owner_to_timeline_media"]["edges"];

            return PostConverter.ConvertPosts(postInfoEdges);
        }
    }
}