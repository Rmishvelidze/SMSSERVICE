using SmsService.Extentions;

namespace SmsService.Helpers
{
    public static class MessagesSplitter
    {
        public static List<List<string>>? GetSplitLists(List<string> messages)
        {
            var list = new List<List<string>>();

            if (messages == null)
                return null;

            var messagesQuantity = messages.Count;
            if (messagesQuantity == 0)
                return null;

            if (messagesQuantity == 1)
                list.Add(messages);

            if (IsEven(messagesQuantity))
                list = messages.ChunkBy(messagesQuantity / 2);

            if (IsOdd(messagesQuantity) && messagesQuantity != 1)
                list = messages.ChunkBy(messagesQuantity / 2);

            return list;
        }

        private static bool IsEven(int number) =>
            number % 2 == 0;

        private static bool IsOdd(int number) =>
            number % 2 == 1;
    }
}
