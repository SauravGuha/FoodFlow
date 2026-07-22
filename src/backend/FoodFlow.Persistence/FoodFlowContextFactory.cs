
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FoodFlow.Persistence;

public class FoodFlowContextFactory : IDesignTimeDbContextFactory<FoodFlowContext>
{
    public FoodFlowContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<FoodFlowContext>();
        optionsBuilder.UseSqlServer("Server=localhost;Database=FoodFlowDB;User Id=sa;Password=YourStrong!Passw0rd;TrustServerCertificate=True;");
        return new FoodFlowContext(optionsBuilder.Options);
    }
}