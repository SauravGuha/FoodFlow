using System;
using System.Threading.Tasks;
using FoodFlow.Application.Common.Repositories;
using FoodFlow.Domain.Models.CustomerModels;

namespace FoodFlow.Application.Common.Repositories;

public interface ICustomerRepository : IBaseRepository<Customer>
{
}