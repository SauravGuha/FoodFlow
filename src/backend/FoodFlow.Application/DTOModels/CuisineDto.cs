namespace FoodFlow.Application.DTOModels;

public class CuisineDto
{
    public string Name { get; set; } = string.Empty;
    public Guid RestaurantId { get; set; }
}