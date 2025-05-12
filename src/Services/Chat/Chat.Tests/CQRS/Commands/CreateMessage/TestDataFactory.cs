using Chat.Core.CQRS.Commands.CreateMessage;
using Chat.Core.Entities;

namespace Chat.Tests.CQRS.Commands.CreateMessage;

public static class TestDataFactory
{
    public static CreateMessageCommand CreateCreateMessageCommand() =>
        new("6f9619ff-8b86-d011-b42d-00cf4fc964ff", 1, "test message");

    public static Dialog CreateDialog(int dialogId, string lastMessage, DateTime lastMessageTime) =>
        new()
        {
            Id = dialogId,
            LastMessage = lastMessage,
            LastMessageTime = lastMessageTime
        };

    public static Message CreateMessage(int dialogId, Guid senderId, string text) =>
        new()
        {
            Id = 1,
            DialogId = dialogId,
            SenderId = senderId,
            Text = text,
            SentAt = DateTime.UtcNow
        };

    public static CreateMessageResponse CreateCreateMessageResponse(Message message) =>
        new(message.Id, message.SentAt, message.SenderId, message.Text);
}