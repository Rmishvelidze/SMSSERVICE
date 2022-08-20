using Microsoft.AspNetCore.Mvc;
using SmsService.Serivces.SMS;

namespace SmsService.Controllers
{
    public class SMSServiceController : ControllerBase
    {
        private readonly ISMSService _sMSService;

        public SMSServiceController(ISMSService sMSService)
        {
            _sMSService = sMSService ?? throw new ArgumentNullException(nameof(sMSService));
        }

        [HttpPost("PercentProviderSelector")]
        public IActionResult PercentProviderSelector(List<string> messages)
        {
            if (messages == null)
                return NotFound("There aren't any messages for send!");
            var result = _sMSService.SendMessageWithPercentProviderSelector(messages);

            if (result.Count == 0)
                return NotFound("There aren't any sms providers!");

            return Ok(result);
        }



        [HttpPost("RandomProviderSelector")]
        public IActionResult RandomProviderSelector(List<string> messages) =>
            Ok(_sMSService.SendMessageWithRandomProviderSelector(messages));
    }
}
