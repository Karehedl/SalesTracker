public class OrderListItem 
{
    public int Id { get; set; }
    public string location { get; set; }   
    public List<ItemListItem> Items { get; set; } = new List<ItemListItem>();

}