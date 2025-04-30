namespace Chat.Core.CQRS.Commands.CreateDialog;

public record CreateDialogRequest(int AdvertId, string Text);