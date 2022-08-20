using SmsService.Data;
using SmsService.Extentions;
using SmsService.Helpers;
using SmsService.Models.Providers;

namespace SmsService.Serivces
{
    public class SMSService : IService
    {
        private readonly AppDbContext _context;

        private RandomProviderGenerator RandomProviderGenerator => new(_context);


        public SMSService(AppDbContext context) =>
            _context = context ?? throw new ArgumentNullException(nameof(context));
        

        public List<string> SendMessageWithPercentProviderSelector(List<string> messages)
        {
            var result = new List<string>();    
            var messagesList = MessagesSplitter.GetSplitLists(messages);
            var providers = new List<SmsProvider>();

            if (RandomProviderGenerator != null && messagesList != null)
            {
                while (providers.Count != 3)
                {
                    var randomPovider = RandomProviderGenerator.GetRandomProvider();
                    if (!providers.Contains(randomPovider))
                        providers.Add(randomPovider);
                }

                for (int i = 0; i < messagesList.Count; i++)
                {
                    result.Add(providers[i].SendMessages(messagesList[i]));
                }
            }
                //foreach (var item in messagesList)
                //{
                //    var randomPovider = RandomProviderGenerator.GetRandomProvider();
                //    if(randomPovider != null)
                //    {
                //        if(providers.Contains(randomPovider))
                //        {
                //            var sameProvider = providers.FirstOrDefault( p => p.Id == randomPovider.Id);
                //            sameProvider.SendMessages(messages);
                //        }
                //        else
                //        {
                //            providers.Add(randomPovider);
                //            result.Add(randomPovider.SendMessages(item));
                //        }
                //    }
                //}
            return result;
        }

        public string SendMessageWithRandomProviderSelector(List<string> messages)
        {
            var randomPovider = RandomProviderGenerator.GetRandomProvider();

            if (randomPovider != null)
               return randomPovider.SendMessages(messages);
            return "Random Provider doesn't exists!";
        }
    }
}
