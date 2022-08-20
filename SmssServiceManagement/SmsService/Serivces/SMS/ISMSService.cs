namespace SmsService.Serivces.SMS
{
    public interface ISMSService
    {
        string SendMessageWithRandomProviderSelector(List<string> messages);
        List<string> SendMessageWithPercentProviderSelector(List<string> messages);
    }
}
