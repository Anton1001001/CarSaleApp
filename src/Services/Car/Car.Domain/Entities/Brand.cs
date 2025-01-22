namespace Car.Domain.Entities;

public class Brand
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public virtual ICollection<Model> Models { get; set; } = new List<Model>();
}
