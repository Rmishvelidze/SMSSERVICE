namespace SmsService.Serivces
{
    public interface IService
    {
        void SendMessageWithRandomProviderSelector(List<string> messages);
        void SendMessageWithPercentProviderSelector(List<string> messages);
    }
}
