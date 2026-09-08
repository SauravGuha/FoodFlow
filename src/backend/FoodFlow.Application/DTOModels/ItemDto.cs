namespace FoodFlow.Application.DTOModels;

public class ItemDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Sku { get; set; } = default!;
    public Guid RestaurantId { get; set; }
    public Guid CuisineId { get; set; }
    public string CategoryName { get; set; } = default!;
}