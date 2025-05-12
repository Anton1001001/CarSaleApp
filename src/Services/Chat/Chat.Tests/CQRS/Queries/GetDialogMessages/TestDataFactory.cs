using Chat.Core.Entities;
using Chat.Core.CQRS.Queries.GetDialogMessages;

namespace Chat.Tests.CQRS.Queries.GetDialogMessages;

public static class TestDataFactory
{
    public static Guid CurrentUserId => Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
    public static int DialogId => 42;

    public static GetDialogMessagesQuery CreateValidQuery() =>
        new(DialogId, CurrentUserId.ToString());

    public static GetDialogMessagesQuery CreateQueryWithNullUserId() =>
        new(DialogId, null);

    public static Message CreateMessage(int id, int dialogId, Guid senderId, string text)
    {
        return new Message
        {
            Id = id,
            DialogId = dialogId,
            SenderId = senderId,
            Text = text,
            SentAt = DateTime.UtcNow
        };
    }

    public static List<Message> CreateMessageList()
    {
        return
        [
            CreateMessage(1, DialogId, CurrentUserId, "My message"),
            CreateMessage(2, DialogId, Guid.NewGuid(), "Other user's message")
        ];
    }
}