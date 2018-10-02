namespace InstagramParser
{
    public class Post
    {
        private string _postContentUrl;
        private string _description;
        private int _likeCount;
        private int _commentCount;
        
        
        
        public Post(string postContentUrl, string description, int likeCount, int commentCount)
        {
            _postContentUrl = postContentUrl;
            _description = description;
            _likeCount = likeCount;
            _commentCount = commentCount;
        }
    }
}