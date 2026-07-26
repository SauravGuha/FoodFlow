
namespace FoodFlow.Application.DTOModels;

using FoodFlow.Domain.Models.RestaurantModels;

public class RestaurantDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string GstNumber { get; set; } = null!;
    public string FNumber { get; set; } = null!;
    public string? Description { get; set; }
    public string Status { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public RestaurantOwnerDto RestaurantOwner { get; set; } = null!;

    public List<BranchDto> Branches { get; set; } = null!;

    public List<Cuisine> Cuisines { get; set; } = null!;
}