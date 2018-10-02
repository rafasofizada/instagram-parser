using System;

namespace InstagramParser
{
    class Program
    {
        static void Main()
        {
            var userPage = new UserPageInfoExtractor("hamidliii");
            /**/ var sharedDataProcessor = new SharedDataProcessor(userPage.ExtractSharedData());
            /*****/ var gis = sharedDataProcessor.RhxGis;
            /**/ var hashExtractor = new HashExtractor(userPage.ExtractQueryHashScriptLink());
            /*****/ var hash = hashExtractor.ExtractHash();
            /*****/ var user = new User(sharedDataProcessor.UserJson);
            /********/ var posts = user.GetPosts(gis, hash);
        }
    }
}
