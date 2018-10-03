using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace InstagramParser
{
    public class User
    {
        private readonly SharedDataProcessor _sharedDataProcessor;
        private readonly HashExtractor _hashExtractor;
        
        private string _id;
        private string _username;
        private string _fullName;
        private string _description;
        private string _profilePicture;

        private int _postCount;
        private int _followersCount;
        private int _followCount;

        private List<Post> _posts;



        public List<Post> Posts
        {
            get
            {
                GetPosts();
                return _posts;
            }
        }



        public User(string username)
        {
            UserPageInfoExtractor _userPageInfoExtractor = new UserPageInfoExtractor(username);
            
            _sharedDataProcessor = new SharedDataProcessor(_userPageInfoExtractor.ExtractSharedData());
            _hashExtractor = new HashExtractor(_userPageInfoExtractor.ExtractQueryHashScriptLink());
            
            InitializeUser(_sharedDataProcessor.UserJson);
        }



        private List<Post> GetPosts()
        {
            string hash = _hashExtractor.ExtractHash();
            string gis = _sharedDataProcessor.RhxGis;
            
            List<Post> posts = new List<Post>();
            PostGetter postGetter = new PostGetter(gis, hash, _id);
            
            posts = postGetter.GetAllPosts();
            //Update local posts
            _posts = posts;

            return posts;
        }

        private void InitializeUser(JToken userJson)
        {
            _id = (string) userJson["id"];
            _username = (string) userJson["username"];
            _fullName = (string) userJson["full_name"];
            _description = (string) userJson["biography"];
            _profilePicture = (string) userJson["profile_pic_url"];

            _postCount = (int) userJson["edge_owner_to_timeline_media"]["count"];
            _followersCount = (int) userJson["edge_followed_by"]["count"];
            _followCount = (int) userJson["edge_follow"]["count"];
        }
    }
}