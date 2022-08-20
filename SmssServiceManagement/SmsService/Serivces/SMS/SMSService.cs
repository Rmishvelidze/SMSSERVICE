using SmsService.Data;
using SmsService.Data.Models.Providers;
using SmsService.Extentions;
using SmsService.Helpers;

namespace SmsService.Serivces.SMS
{
    public class SMSService : ISMSService
    {
        private readonly AppDbContext _context;

        private RandomProviderGenerator RandomProviderGenerator => new(_context);

        public SMSService(AppDbContext context) =>
            _context = context ?? throw new ArgumentNullException(nameof(context));


        public List<string> SendMessageWithPercentProviderSelector(List<string> messages)
        {
            var messagesHistory = new List<string>();
            var messagesList = MessagesSplitter.GetSplitLists(messages);
            var providers = new List<SmsProvider>();

            if (RandomProviderGenerator != null && messagesList != null)
            {
                GenerateThreeDiferentProvider();

                for (int i = 0; i < messagesList.Count; i++)
                {
                    messagesHistory.Add(providers[i].SendMessages(messagesList[i])
                        + $" {Math.Round((decimal)messagesList[i].Count / (decimal)messages.Count * 100, 2)}"
                            + $" % of whole messages.");
                }
            }
            return messagesHistory;


            void GenerateThreeDiferentProvider()
            {
                while (providers.Count != 3)
                {
                    var randomPovider = RandomProviderGenerator.GetRandomProvider();
                    if (!providers.Contains(randomPovider))
                        providers.Add(randomPovider);
                }
            }
        }

        public string SendMessageWithRandomProviderSelector(List<string> messages)
        {
            if (messages == null)
                return "There aren't any messages for send!";

            var randomPovider = RandomProviderGenerator.GetRandomProvider();

            if (randomPovider != null)
                return randomPovider.SendMessages(messages);
            return "Random Provider doesn't exists!";
        }
    }
}
