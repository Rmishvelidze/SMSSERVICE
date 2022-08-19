using SmsService.Models.Providers;

namespace SmsService.Extentions
{
    public static class SmsProviderExtentions
    {
        public static void SendMessages(this SmsProvider smsProvider, List<string> messages) =>
            Console.WriteLine($"{smsProvider} sent {messages.Count} Messages!");

    }
}
