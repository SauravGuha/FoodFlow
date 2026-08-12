using FoodFlow.Application.Common;
using FoodFlow.Domain.Models.RestaurantModels;
using MediatR;

namespace FoodFlow.Application.Commands.BranchCommands.CreateBranch;

public class CreateBranchCommand : IRequest<Result<Guid>>
{
    public Guid RestaurantId { get; set; }
    public string Name { get; set; } = null!;
    public string Street { get; set; } = null!;
    public string City { get; set; } = null!;
    public string State { get; set; } = null!;
    public string ZipCode { get; set; } = null!;
    public string Country { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Email { get; set; } = null!;
    public Dictionary<DayOfWeek, IReadOnlyCollection<TimeSlot>> OperatingHours { get; set; } = null!;
}