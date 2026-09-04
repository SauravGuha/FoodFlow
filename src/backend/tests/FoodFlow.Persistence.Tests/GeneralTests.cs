using FoodFlow.Domain.Models.InventoryModels;
using Microsoft.EntityFrameworkCore;

namespace FoodFlow.Persistence.Tests;

public class GeneralTests
{
    /// <summary>
    /// Test that two concurrent save operations throw a DbUpdateConcurrencyException.
    /// </summary>
    [Fact]
    public void TwoContextConcurrencyException()
    {
        //arrange
        var options = new DbContextOptionsBuilder<FoodFlowContext>()
        .UseSqlServer("Server=localhost;Database=FoodFlowDB;User Id=sa;Password=@Bcd.1234;TrustServerCertificate=True;")
        .Options;
        var foodFlowContext = new FoodFlowContext(options);

        var context1 = new FoodFlowContext(options);
        var dbSet1 = context1.Set<BranchInventory>();
        var branchInventory = dbSet1.FirstOrDefault(e => e.BranchId.ToString() == "57ab7897-b166-4258-0cd9-08df0074eb7a"
        && e.ItemId.ToString() == "f4b739a5-77e4-4798-2f06-08df013fbda7");
        branchInventory!.AddQuantity(5);
        dbSet1.Update(branchInventory);

        var context2 = new FoodFlowContext(options);
        var dbSet2 = context2.Set<BranchInventory>();
        var branchInventory1 = dbSet2.FirstOrDefault(e => e.BranchId.ToString() == "57ab7897-b166-4258-0cd9-08df0074eb7a"
&& e.ItemId.ToString() == "f4b739a5-77e4-4798-2f06-08df013fbda7");
        branchInventory1!.RemoveQuantity(6);
        dbSet2.Update(branchInventory1);

        //act
        context1.SaveChanges();

        //assert
        Assert.Throws<DbUpdateConcurrencyException>(() => context2.SaveChanges());
    }
}
