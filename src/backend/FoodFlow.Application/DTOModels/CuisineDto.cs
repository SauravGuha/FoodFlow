namespace FoodFlow.Application.DTOModels;

public class CuisineDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid RestaurantId { get; set; }
}