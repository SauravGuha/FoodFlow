
namespace FoodFlow.Domain.Models.InventoryModels;

public enum FoodCategory
{
    veg,
    nonveg,
    pureveg,
    undescribed
}

public class InventoryItem : BaseModel
{
    public InventoryItem(string name, string description, string sku, Guid restaurantId, Guid cuisineId)
    {
        Name = name;
        Description = description;
        Sku = sku;
        RestaurantId = restaurantId;
        CuisineId = cuisineId; // Assuming cuisine is linked to the same ID as restaurant for now
        Category = FoodCategory.undescribed;
    }

    public string Name { get; private set; }

    public string Description { get; private set; }

    public string Sku { get; private set; }

    public Guid RestaurantId { get; private set; }

    public Guid CuisineId { get; private set; }

    public FoodCategory Category { get; private set; } = FoodCategory.undescribed;

    private List<BranchInventory> _branchInventories = new List<BranchInventory>();
    public IReadOnlyCollection<BranchInventory> BranchInventories => _branchInventories.AsReadOnly();

    public void AddBranchInventory(BranchInventory branchInventory)
    {
        if (_branchInventories.Any(e => e.InventoryItemId == this.Id && branchInventory.BranchId == e.BranchId))
            throw new ArgumentException("Cannot add duplicate branchinventory");

        this._branchInventories.Add(branchInventory);
    }
}