namespace FoodFlow.Application.DTOModels;

public class BranchDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public Guid RestaurantId { get; set; }

    public AddressDto Address { get; set; } = new(); // Updated to include AddressDto
    public OperatingHoursDto OperatingHours { get; set; } = new(); // Updated to include OperatingHoursDto
}