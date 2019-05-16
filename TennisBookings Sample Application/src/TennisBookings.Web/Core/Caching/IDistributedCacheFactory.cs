namespace TennisBookings.Web.Core.Caching
{
    public interface IDistributedCacheFactory
    {
        IDistributedCache<T> GetCache<T>();
    }
}
