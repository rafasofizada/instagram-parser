using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.VisualBasic;

namespace InstagramParser
{
    
    public class FileDownloader
    {
        private const string UserAgent = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_12_6) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/69.0.3497.81 Safari/537.36";
        public readonly WebClient _downloaderClient;

        /// <summary>
        /// Generate a WebClient with user-agent value declared as a constant in Link (?name of constant class) to download a non-secured file
        /// </summary>
        public FileDownloader() 
        {
            _downloaderClient = new WebClient();
            _downloaderClient.Headers.Add("user-agent", UserAgent);
        }
        
        /// <summary>
        /// Generate a x-instagram-gis header on top of FileDownloader (with user-agent header) to download a secure file
        /// </summary>
        public FileDownloader(string xInstagramGis)
        : this()
        {
            _downloaderClient.Headers.Add("x-instagram-gis", xInstagramGis);
        }


        /// <summary>
        /// Download a file on given url by given WebClient
        /// </summary>
        /// <param name="downloadUrl">Downloaded file url</param>
        public string DownloadFile(string downloadUrl)
        {   
            string file = _downloaderClient.DownloadString(downloadUrl);

            return file;
        }
    }
}