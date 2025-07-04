using System;
using System.Threading.Tasks;
using Flagsmith.Providers;

namespace Flagsmith.Cache
{
    internal class IdentityFlagListCache : FlagListCache
    {
        private readonly IdentityWrapper _identityWrapper;

        public IdentityFlagListCache(IdentityWrapper identityWrapper,
            IDateTimeProvider dateTimeProvider,
            int cacheDurationInMinutes) :
            base(dateTimeProvider,
                cacheDurationInMinutes)
        {
            _identityWrapper = identityWrapper;
        }

        public async ValueTask<IFlags> GetLatestFlags(Func<IdentityWrapper, ValueTask<IFlags>> getFlagsDelegate)
        {
            if (IsCacheStale())
            {
                _flags = await getFlagsDelegate(_identityWrapper);
                _timestamp = _dateTimeProvider.Now();
            }

            return _flags;
        }
    }
}