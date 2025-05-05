namespace Chat.Core.CQRS.Commands.CreateMessage;

public record CreateMessageResponse(int MessageId, DateTime SentAt, Guid SenderId, string Text);