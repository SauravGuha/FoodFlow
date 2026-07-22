
namespace FoodFlow.Application.DTOModels;

using FoodFlow.Domain.Models.RestaurantModels;

public class RestaurantDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Gst { get; set; } = null!;
    public string FNumber { get; set; } = null!;
    public string? Description { get; set; }
    public RestaurantStatus Status { get; set; }
    public RestaurantOwnerDto RestaurantOwner { get; set; } = null!;
}