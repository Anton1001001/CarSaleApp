namespace Chat.Core.Entities;

public class Dialog
{
    public int Id { get; set; }
    public int AdvertId { get; set; }
    public Guid SellerId { get; set; }
    public string Name { get; set; }
    public Guid BuyerId { get; set; }
    public DateTime LastMessageTime { get; set; }
    public string LastMessage { get; set; }
    
    public ICollection<Message> Messages { get; set; }
}
