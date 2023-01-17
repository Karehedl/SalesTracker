
public interface IItemService
{
    Task<bool> CreateItemAsync(ProductCreate itemCreate);
    Task<IEnumerable<ItemListItem>> GetAllItemsAsync();
    Task<ProductDetails> GetItemByIdAsync(int itemId);
    Task<bool> EditItemAsync(ProductEdit request);
    Task<bool> DeleteItemAsync(int itemId);
}
