namespace InstagramParser
{
    public abstract class Extractor
    {
        protected readonly string File;
        private readonly FileDownloader _fileDownloader;
        
        protected Extractor()
        {
            _fileDownloader = new FileDownloader();
        }
        
        protected Extractor(string downloadUrl)
            : this()
        {
            File = _fileDownloader.DownloadFile(downloadUrl);
        }
    }
}