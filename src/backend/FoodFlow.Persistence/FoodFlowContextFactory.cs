
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FoodFlow.Persistence;

public class FoodFlowContextFactory : IDesignTimeDbContextFactory<FoodFlowContext>
{
    public FoodFlowContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<FoodFlowContext>();
        optionsBuilder.UseSqlServer("Server=localhost;Database=FoodFlowDB;User Id=sa;Password=@Bcd.1234;TrustServerCertificate=True;");
        return new FoodFlowContext(optionsBuilder.Options);
    }
}