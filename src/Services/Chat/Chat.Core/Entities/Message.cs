namespace Chat.Core.Entities;

public class Message
{
    public int Id { get; set; }
    public int DialogId { get; set; }
    public Dialog Dialog { get; set; }
    public Guid SenderId { get; set; }
    public string Text { get; set; }
    public DateTime SentAt { get; set; }
}