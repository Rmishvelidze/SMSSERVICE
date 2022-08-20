using Microsoft.EntityFrameworkCore;
using SmsService.Data;
using SmsService.Data.DTOs;
using SmsService.Data.Models.Providers;

namespace SmsService.Serivces.Provider
{
    public class ProviderService
    {
        private readonly AppDbContext _context;
        private DbSet<SmsProvider> SmsProviders => _context.SmsProviders;

        public ProviderService(AppDbContext context) =>
            _context = context ?? throw new ArgumentNullException(nameof(context));

        //GetAll
        public List<SmsProvider> GetAllProviders() => SmsProviders.ToList();

        //GetById
        public async Task<SmsProvider?> GetProviderById(int id)
        {
            var targetProvider = await SmsProviders.FindAsync(id);
            if (targetProvider == null) return null;

            return targetProvider;
        }

        //Create
        public async Task<SmsProvider> CreateProvider(ProviderDTO providerDTO)
        {
            var newSmsProvider = new SmsProvider()
            {
                //We can handle this also with AutoMapper
                Name = providerDTO.Name
            };

            await SmsProviders.AddAsync(newSmsProvider);
            _context.SaveChanges();
            return newSmsProvider;
        }

        //Update
        public async Task<SmsProvider?> UpdateSmsProvider(SmsProvider smsProvider)
        {
            var targetProvider = await SmsProviders
                .FirstOrDefaultAsync(p => p.Id == smsProvider.Id);
            if (targetProvider == null)
                return null;

            targetProvider.Name = smsProvider.Name;
            _context.SaveChanges();

            return targetProvider;
        }

        //Delete
        public async Task<string?> DeleteProvider(int id)
        {
            var targetProvider = await SmsProviders.FindAsync(id);
            if (targetProvider == null) return null;

            SmsProviders.Remove(targetProvider);
            _context.SaveChanges();
            return $"Provider with id: {id} succesfully deleted!";
        }
    }
}
