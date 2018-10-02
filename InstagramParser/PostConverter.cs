using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace InstagramParser
{
    public static class PostConverter
    {
        public static List<Post> ConvertPosts(JArray postInfoEdges)
        {
            List<Post> posts = new List<Post>();
            
            for (var i = 0; i < postInfoEdges.Count; i++)
            {
                var node = postInfoEdges[i]["node"];
                posts.Add(NodeToPost(node));
            }

            return posts;
        }

        private static Post NodeToPost(JToken node)
        {
            string postContentUrl = (string) node["display_url"];

            string description = "";
            if (node["edge_media_to_caption"]["edges"] != null &&
                ((JArray) node["edge_media_to_caption"]["edges"]).Count != 0)
                description = (string) node["edge_media_to_caption"]["edges"][0]["node"]["text"];

            int likeCount = (int) node["edge_media_preview_like"]["count"];
            int commentCount = (int) node["edge_media_to_comment"]["count"];

            Post post = new Post(postContentUrl, description, likeCount, commentCount);

            return post;
        }
    }
}