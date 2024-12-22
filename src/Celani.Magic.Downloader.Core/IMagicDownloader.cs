namespace Celani.Magic.Downloader.Core;

public interface IMagicDownloader
{
    public string Backend { get; }

    public Task<DownloadedMagicList> DownloadDeckAsync(string id);
}
