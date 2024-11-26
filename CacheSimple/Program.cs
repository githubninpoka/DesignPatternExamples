namespace CacheSimple
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IDataDownloader slowDataDownloader = new SlowDataDownloader();
            IDataDownloader dataDownloader = new CachedDataDownloader(slowDataDownloader);

            Console.WriteLine(dataDownloader.DownloadData("id1"));
            Console.WriteLine(dataDownloader.DownloadData("id2"));
            Console.WriteLine(dataDownloader.DownloadData("id3"));
            Console.WriteLine(dataDownloader.DownloadData("id1"));
            Console.WriteLine(dataDownloader.DownloadData("id3"));

            Console.ReadKey();
        }

        public interface IDataDownloader
        {
            string DownloadData(string resourceId);
        }

        public class Cache<T1,T2>
        {
            private readonly Dictionary<T1, T2> _cachedData = new();

            public T2 Get(T1 item, Func<T1, T2> getForFirstTime)
            {
                if (!_cachedData.ContainsKey(item))
                {
                    _cachedData.Add(item, getForFirstTime(item));
                }
                return _cachedData[item];
            }
        }

        public class CachedDataDownloader : IDataDownloader
        {
            private readonly IDataDownloader _dataDownloader;
            private readonly Cache<string, string> _cache = new();
            public CachedDataDownloader(IDataDownloader dataDownloader)
            {
                _dataDownloader = dataDownloader;
            }
            public string DownloadData(string resourceId)
            {
                return _cache.Get(resourceId, _dataDownloader.DownloadData);
            }
        }

        public class SlowDataDownloader : IDataDownloader
        {
            public string DownloadData(string resourceId)
            {
                //let's imagine this method downloads real data,
                //and it does it slowly
                Thread.Sleep(1000);
                return $"Some data for {resourceId}";
            }

        }
    }
}
