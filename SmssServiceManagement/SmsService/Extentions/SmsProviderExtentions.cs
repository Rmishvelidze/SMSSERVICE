using SmsService.Data.Models.Providers;

namespace SmsService.Extentions
{
    public static class SmsProviderExtentions
    {
        public static string SendMessages(this SmsProvider smsProvider, List<string> messages) =>
            ($"{smsProvider.Name} sent {messages.Count} Messages!");
    }
}
