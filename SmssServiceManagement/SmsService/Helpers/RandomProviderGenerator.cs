using Microsoft.EntityFrameworkCore;
using SmsService.Data;
using SmsService.Models.Providers;

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
                var lastProviderId = SmsProviders.Last().Id;
                var randomId = GetRandomId(lastProviderId);

                while (SmsProviders.Any(p => p.Id != randomId))
                {
                    randomId = GetRandomId(lastProviderId);
                }
                return randomId;
            }
            return default;
        }

        static int GetRandomId(int lastProviderId)
        {
            var random = new Random();
            return random.Next(1, lastProviderId);
        }
    }
}
