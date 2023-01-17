


public class OrderDetails 
{
    public int Id { get; set; }
    public string Location { get; set; }
    public List<ItemListItem> Items { get; set; } = new List<ItemListItem>();
}