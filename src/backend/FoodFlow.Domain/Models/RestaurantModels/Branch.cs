
namespace FoodFlow.Domain.Models.RestaurantModels;

public enum BranchStatus
{
    Inactive,
    Active,
    Closed
}

public class Branch : BaseModel
{
    private Branch() { } // For EF Core

    public Branch(Guid restaurantId, string name, Address address, string phoneNumber, string email, OperatingHours operatingHours)
    {
        ValidateBranch(name, phoneNumber, email);
        this.Name = name;
        this.AddressDetails = address;
        this.PhoneNumber = phoneNumber;
        this.Email = email;
        this.OperatingHours = operatingHours;
        this.RestaurantId = restaurantId;
    }

    private void ValidateBranch(string name, string phoneNumber, string email)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Branch name cannot be empty.");

        if (string.IsNullOrWhiteSpace(phoneNumber))
            throw new ArgumentException("Branch phone number cannot be empty.");

        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Branch email cannot be empty.");
    }

    public string Name { get; private set; } = string.Empty;

    public string PhoneNumber { get; private set; } = string.Empty;

    public string Email { get; private set; } = string.Empty;

    public Guid RestaurantId { get; private set; }

    public Address AddressDetails { get; private set; } = null!;

    public BranchStatus Status { get; private set; } = BranchStatus.Active;

    public OperatingHours OperatingHours { get; private set; }

    public void UpdateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Branch name cannot be empty.");

        Name = name;
    }

    public void UpdatePhoneNumber(string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
            throw new ArgumentException("Branch phone number cannot be empty.");

        PhoneNumber = phoneNumber;
    }

    public void UpdateEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Branch email cannot be empty.");

        Email = email;
    }

    public void UpdateAddress(Address address)
    {
        AddressDetails = address;
    }

    public void UpdateStatus(BranchStatus status)
    {
        Status = status;
    }

    public void UpdateOperatingHours(Dictionary<DayOfWeek, IReadOnlyCollection<TimeSlot>> schedule)
    {
        OperatingHours = new OperatingHours(schedule);
    }

    public bool IsOpen(DateTime currentDateTime)
    {
        return OperatingHours?.IsOpen(currentDateTime) ?? false;
    }
}

public record Address(string Street, string City, string State, string ZipCode, string Country);

public sealed record OperatingHours
{
    public IReadOnlyDictionary<DayOfWeek, IReadOnlyCollection<TimeSlot>> Schedule { get; private set; }

    public OperatingHours(Dictionary<DayOfWeek, IReadOnlyCollection<TimeSlot>> schedule)
    {
        Schedule = schedule;
    }

    public bool IsOpen(DateTime currentDateTime)
    {
        if (!Schedule.TryGetValue(currentDateTime.DayOfWeek, out var slots))
            return false;

        return slots.Any(slot => slot.Contains(TimeOnly.FromDateTime(currentDateTime)));
    }
}

public sealed record TimeSlot
{
    public TimeOnly StartTime { get; init; }
    public TimeOnly EndTime { get; init; }

    public TimeSlot(TimeOnly startTime, TimeOnly endTime)
    {
        if (startTime >= endTime)
            throw new ArgumentException("Start time must be earlier than end time.");

        StartTime = startTime;
        EndTime = endTime;
    }

    public bool Contains(TimeOnly time)
    {
        return time >= StartTime && time <= EndTime;
    }
}