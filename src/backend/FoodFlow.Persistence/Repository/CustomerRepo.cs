using System;
using System.Threading.Tasks;
using FoodFlow.Application.Common.Repositories;
using FoodFlow.Domain.Models.CustomerModels;

namespace FoodFlow.Persistence.Repository;

public class CustomerRepo : BaseRepository<Customer>, ICustomerRepository
{
    public CustomerRepo(FoodFlowContext context) : base(context)
    {
    }
}