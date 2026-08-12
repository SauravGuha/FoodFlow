
namespace FoodFlow.Domain.Models.RestaurantModels;

public enum RestaurantStatus
{
    Inactive,
    Active,
    Suspended,
    PermanentlyClosed
}

public class Restaurant : BaseModel
{
    private Restaurant()
    {

    }
    public Restaurant(string name, string gst, string fnumber,
    RestaurantOwner restaurantOwner, RestaurantStatus status = RestaurantStatus.Inactive,
    string? description = null)
    {
        ValidateRestaurant(name, gst, fnumber, restaurantOwner);
        this.Name = name;
        this.GstNumber = gst;
        this.FNumber = fnumber;
        this.Status = status;
        this.RestaurantOwner = restaurantOwner;
        this.Description = description;
    }

    private void ValidateRestaurant(string name, string gst, string fnumber, RestaurantOwner restaurantOwner)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Restaurant name cannot be empty.");

        if (string.IsNullOrWhiteSpace(gst))
            throw new ArgumentException("GST number cannot be empty.");

        if (string.IsNullOrWhiteSpace(fnumber))
            throw new ArgumentException("F number cannot be empty.");

        if (restaurantOwner == null)
            throw new ArgumentException("Restaurant owner cannot be null.");
    }

    public string Name { get; private set; } = string.Empty;

    public string? Description { get; private set; }

    private List<Cuisine> _cuisines = [];
    public IReadOnlyCollection<Cuisine> Cuisines => _cuisines.AsReadOnly();

    private List<Branch> _branches = [];
    public IReadOnlyCollection<Branch> Branches => _branches.AsReadOnly();

    public string GstNumber { get; private set; } = string.Empty;

    public string FNumber { get; private set; } = string.Empty;

    public RestaurantOwner RestaurantOwner { get; private set; } = null!;

    public RestaurantStatus Status { get; private set; }

    public void AddCuisine(Cuisine cuisine)
    {
        _cuisines.Add(cuisine);
    }

    public void RemoveCuisine(Cuisine cuisine)
    {
        _cuisines.Remove(cuisine);
    }

    public void UpdateDescription(string? description)
    {
        if (!string.IsNullOrWhiteSpace(description))
            Description = description;
    }

    public void UpdateStatus(RestaurantStatus status)
    {
        Status = status;
    }

    public void UpdateRestaurantOwner(RestaurantOwner restaurantOwner)
    {
        if (restaurantOwner == null)
            throw new ArgumentException("Restaurant owner cannot be null.");

        RestaurantOwner = restaurantOwner;
    }

    public void UpdateGstNumber(string gst)
    {
        if (string.IsNullOrWhiteSpace(gst))
            throw new ArgumentException("GST number cannot be empty.");

        GstNumber = gst;
    }

    public void UpdateFNumber(string fnumber)
    {
        if (string.IsNullOrWhiteSpace(fnumber))
            throw new ArgumentException("F number cannot be empty.");

        FNumber = fnumber;
    }

    public void UpdateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Restaurant name cannot be empty.");

        Name = name;
    }

    public void AddBranch(Branch branch)
    {
        if (this.Id != branch.RestaurantId)
            throw new ArgumentException("Branch does not belong to this restaurant.");

        if (this._branches.Any(b => b.Name == branch.Name))
            throw new ArgumentException($"Branch with name '{branch.Name}' already exists for this restaurant.");

        _branches.Add(branch);
    }

    public void RemoveBranch(Branch branch)
    {
        _branches.Remove(branch);
    }

}

public class Cuisine : BaseModel
{
    public Cuisine(string name, Guid restaurantId)

    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Cuisine name cannot be empty.");

        this.Name = name;
        this.RestaurantId = restaurantId;
    }

    public string Name { get; private set; } = string.Empty;

    public Guid RestaurantId { get; private set; }

    public void UpdateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Cuisine name cannot be empty.");

        Name = name;
    }
}

public record RestaurantOwner(string Name, string Email, PhoneNumber PhoneNumber);

public record PhoneNumber(string CountryCode, string Number);

