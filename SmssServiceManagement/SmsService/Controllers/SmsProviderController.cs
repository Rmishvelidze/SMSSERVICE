using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmsService.Data;
using SmsService.Models.Providers;

namespace SmsService.Controllers
{
    public class SmsProviderController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SmsProviderController(AppDbContext context) =>
            _context = context ?? throw new ArgumentNullException(nameof(context));

        private DbSet<SmsProvider> SmsProviders => _context.SmsProviders;

        [HttpGet("GetAll")]
        public IActionResult GetAllProviders()
        {
            var response = _context.SmsProviders;
            return Ok(response);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await SmsProviders.FindAsync(id);
            if(response == null)
                return NotFound($"Provider with id:{id} not found!");
            return Ok(response);
        }


        [HttpPost("Create")]
        public async Task<IActionResult> CreateProvider(SmsProvider smsProvider)
        {
            await SmsProviders.AddAsync(smsProvider);
            _context.SaveChanges();
            return Ok(smsProvider);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateProvider(SmsProvider smsProvider)
        {
            var targetProvider = await SmsProviders.FindAsync(smsProvider);
            if (targetProvider == null)
                return NotFound($"Provider with id:{smsProvider.Id} not found!");

            targetProvider.Name = smsProvider.Name;
            _context.SaveChanges();

            return Ok(smsProvider);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteProvider(int id)
        {
            var targetProvider = await SmsProviders.FindAsync(id);
                return NotFound($"Provider with id:{id} not found!");

            SmsProviders.Remove(targetProvider);
            _context.SaveChanges();

            return Ok();
        }
    }
}
