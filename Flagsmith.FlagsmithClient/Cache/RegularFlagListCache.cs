using System;
using System.Threading.Tasks;
using Flagsmith.Providers;

namespace Flagsmith.Cache
{
    internal class RegularFlagListCache : FlagListCache
    {
        public RegularFlagListCache(IDateTimeProvider dateTimeProvider,
            int cacheDurationInMinutes) :
            base(dateTimeProvider, cacheDurationInMinutes)
        {
        }

        public async ValueTask<IFlags> GetLatestFlags(Func<ValueTask<IFlags>> getFlagsDelegate)
        {
            if (IsCacheStale())
            {
                _flags = await getFlagsDelegate();
                _timestamp = _dateTimeProvider.Now();
            }

            return _flags;
        }
    }
}