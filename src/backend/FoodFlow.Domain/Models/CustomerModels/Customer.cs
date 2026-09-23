
namespace FoodFlow.Domain.Models.CustomerModels;

public class Customer : BaseModel
{
    public Customer(string name, string email, string userName, string externalId)
    {
        this.Name = name;
        this.Email = email;
        this.UserName = userName;
        this.ExternalId = externalId;
    }

    public string Name { get; private set; }
    public string Email { get; private set; }
    public string UserName { get; private set; }

    public string ExternalId { get; private set; }

    public void UpdateName(string name)
    {
        this.Name = name;
    }

    public void UpdateEmail(string email)
    {
        this.Email = email;
    }

    public void UpdateUserName(string userName)
    {
        this.UserName = userName;
    }
}