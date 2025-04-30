namespace Chat.Core.CQRS.Queries.GetDialogMessages;

public record GetDialogMessageResponse(int MessageId, DateTime SentAt, bool IsIncoming, string Text);