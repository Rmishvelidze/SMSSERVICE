namespace SmsService.Serivces
{
    public interface IService
    {
        string SendMessageWithRandomProviderSelector(List<string> messages);
        List<string> SendMessageWithPercentProviderSelector(List<string> messages);
    }
}
