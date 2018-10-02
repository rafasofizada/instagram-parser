//!WARNING Deleted parameterless construct
using System.Linq;
using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;

namespace InstagramParser
{
    public class UserPageInfoExtractor : Extractor
    {
        private readonly HtmlNode _documentNode;
        private const string QueryHashSource = "ProfilePageContainer.js";
        
        
        public UserPageInfoExtractor(string username) : base(UserPageLink(username))
        {
            //Convert the file (downloaded as string) to an html document
            HtmlDocument html = new HtmlDocument();
            html.LoadHtml(File);
            
            _documentNode = html.DocumentNode;
        }



        public string ExtractSharedData()
        {
            string sharedData = _documentNode.SelectNodes("/html/body/script[1]")[0].InnerHtml;
            
            sharedData = sharedData.Replace("window._sharedData = ", "").Replace(";", "");

            return sharedData;
        }

        public string ExtractQueryHashScriptLink()
        {
            HtmlNode link = _documentNode.QuerySelector("[href*=\"" + QueryHashSource + "\"]");
            HtmlAttributeCollection attributes = link.Attributes;

            HtmlAttribute href = attributes.FirstOrDefault(attr => attr.Name == "href");
            string scriptSourceRelative = href.Value;
            string scriptSourceAbsolute = Utility.BaseUrl + scriptSourceRelative;

            return scriptSourceAbsolute;
        }

        private static string UserPageLink(string username) => Utility.BaseUrl + '/' + username;
    }
}