using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace InstagramParser
{
    public class User
    {
        public string Id { get; }
        private string _username;
        private string _fullName;
        private string _description;
        private string _profilePicture;

        private int _postCount;
        private int _followersCount;
        private int _followCount;

        private List<Post> _posts;
        
        

        public User(JToken userJson)
        {
            Id = (string) userJson["id"];
            _username = (string) userJson["username"];
            _fullName = (string) userJson["full_name"];
            _description = (string) userJson["biography"];
            _profilePicture = (string) userJson["profile_pic_url"];

            _postCount = (int) userJson["edge_owner_to_timeline_media"]["count"];
            _followersCount = (int) userJson["edge_followed_by"]["count"];
            _followCount = (int) userJson["edge_follow"]["count"];
        }



        public List<Post> GetPosts(string gis, string hash)
        {
            List<Post> posts = new List<Post>();
            PostGetter postGetter = new PostGetter(gis, hash, Id);
            
            posts = postGetter.GetAllPosts();

            return posts;
        }
    }
}