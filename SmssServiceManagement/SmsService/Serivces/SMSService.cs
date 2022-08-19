using SmsService.Data;
using SmsService.Extentions;
using SmsService.Helpers;

namespace SmsService.Serivces
{
    public class SMSService : IService
    {
        private readonly AppDbContext _context;

        private RandomProviderGenerator RandomProviderGenerator => new(_context);


        public SMSService(AppDbContext context) =>
            _context = context ?? throw new ArgumentNullException(nameof(context));
        

        public void SendMessageWithPercentProviderSelector(List<string> messages)
        {
            var messagesList = MessagesSplitter.GetSplitLists(messages);

            if (RandomProviderGenerator != null && messagesList != null)
                foreach (var item in messagesList)
                {
                    var randomPovider = RandomProviderGenerator.GetRandomProvider();
                    if(randomPovider != null)
                        randomPovider.SendMessages(item);
                }
        }

        public void SendMessageWithRandomProviderSelector(List<string> messages)
        {
            var randomPovider = RandomProviderGenerator.GetRandomProvider();

            if (randomPovider != null)
                randomPovider.SendMessages(messages);
        }
    }
}
