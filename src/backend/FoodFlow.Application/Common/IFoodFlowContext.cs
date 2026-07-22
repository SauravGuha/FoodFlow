
namespace FoodFlow.Application.Common;

public interface IFoodFlowContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}