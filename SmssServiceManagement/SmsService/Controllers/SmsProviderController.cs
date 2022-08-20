using Microsoft.AspNetCore.Mvc;
using SmsService.Data.DTOs;
using SmsService.Data.Models.Providers;
using SmsService.Serivces.Provider;

namespace SmsService.Controllers
{
    public class SmsProviderController : ControllerBase
    {
        private readonly ProviderService _providerService;

        public SmsProviderController(ProviderService providerService) =>
            _providerService = providerService ?? throw new ArgumentNullException(nameof(providerService));


        [HttpGet("GetAll")]
        public IActionResult GetAllProviders()
        {
            var response = _providerService.GetAllProviders();
            if (response == null)
                return NotFound("Providers don't exists!");
            return Ok(response);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _providerService.GetProviderById(id);
            if (response == null)
                return NotFound($"Provider with id:{id} not found!");
            return Ok(response);
        }


        [HttpPost("Create")]
        public async Task<IActionResult> CreateProvider(ProviderDTO smsProviderDTO)
        {
            var newProvider = await _providerService.CreateProvider(smsProviderDTO);
            return Ok(newProvider);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateProvider(SmsProvider smsProvider)
        {
            var targetProvider = await _providerService.UpdateSmsProvider(smsProvider);
            if (targetProvider == null)
                return NotFound($"Provider with id:{smsProvider.Id} not exists!");

            return Ok(targetProvider);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteProvider(int id)
        {
            var action = await _providerService.DeleteProvider(id);
            if (action == null)
                return NotFound($"Provider with id:{id} not exists!");
            return Ok(action);
        }
    }
}
