using Microsoft.EntityFrameworkCore;
using SmsService.Data;
using SmsService.Data.Models.Providers;

namespace SmsService.Helpers
{
    public class RandomProviderGenerator
    {
        private readonly AppDbContext _context;

        public RandomProviderGenerator(AppDbContext context) =>
            _context = context ?? throw new ArgumentNullException(nameof(context));


        private DbSet<SmsProvider> SmsProviders => _context.SmsProviders;


        public SmsProvider? GetRandomProvider()
        {
            var randomProviderId = GetRandomProviderId();
            if (randomProviderId == 0)
                return null;

            var randomProvider = SmsProviders.FirstOrDefault(x => x.Id == randomProviderId);
            if (randomProvider == null)
                return null;

            return randomProvider;
        }

        private int GetRandomProviderId()
        {
            if (SmsProviders != null)
            {
                var orderedSmosProvidersIds = SmsProviders.OrderBy(p => p.Id).Select(p => p.Id);

                if (orderedSmosProvidersIds != null)
                {
                    var lastProviderId = orderedSmosProvidersIds.LastOrDefault();
                    var randomId = GetRandomId(lastProviderId);

                    while (!orderedSmosProvidersIds.Contains(randomId))
                    {
                        randomId = GetRandomId(lastProviderId);
                    }
                    return randomId;
                }
            }
            return default;
        }

        static int GetRandomId(int lastProviderId)
        {
            var random = new Random();
            return random.Next(1, lastProviderId + 1);
        }
    }
}
