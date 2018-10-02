//!WARNING Deleted parameterless construct
namespace InstagramParser
{
    public class HashExtractor : Extractor
    {
        /// <summary>
        /// The piece of code preceding the query hash, needed for post load query
        /// </summary>
        private readonly string _encapsulatingCode = "getState:function(e,t,n,o){var r;return o?null:null===(r=e.profilePosts.byUserId.get(t))||void 0===r?void 0:r.pagination},queryId:\"";
        private readonly int _queryHashLength = 32;



        public HashExtractor(string downloadUrl) : base(downloadUrl) {}

        
        
        /// <summary>
        /// Extracts the query hash, going after the _encapsulatingCode
        /// </summary>
        /// <returns>Query hash string</returns>
        public string ExtractHash()
        {
            int encapsulatingCodeIndex = File.IndexOf(_encapsulatingCode);
            int queryHashStartIndex = encapsulatingCodeIndex + _encapsulatingCode.Length;
            
            return File.Substring(queryHashStartIndex, _queryHashLength);
        }
    }
}