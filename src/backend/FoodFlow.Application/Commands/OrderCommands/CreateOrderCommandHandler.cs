
using AutoMapper;
using FoodFlow.Application.Common;
using FoodFlow.Application.Common.Repositories;
using FoodFlow.Application.DTOModels;
using FoodFlow.Application.Services;
using FoodFlow.Domain.Models.CustomerModels;
using FoodFlow.Domain.Models.OrderModels;
using FoodFlow.Domain.Models.RestaurantModels;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace FoodFlow.Application.Commands.OrderCommands;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Result<OrderDto>>
{
    private readonly IOrderRepository orderRepository;
    private readonly IBranchInventoryRepository branchInventoryRepository;
    private readonly IOrderItemRepository orderItemRepository;
    private readonly IFoodFlowContext foodFlowContext;
    private readonly ICustomerService customerService;
    private readonly ICustomerRepository customerRepository;
    private readonly IMapper mapper;
    private readonly IPaymentGateway paymentGateway;

    public CreateOrderCommandHandler(IOrderRepository orderRepository, IBranchInventoryRepository branchInventoryRepository,
    IOrderItemRepository orderItemRepository, IFoodFlowContext foodFlowContext, ICustomerService customerService,
    ICustomerRepository customerRepository, IMapper mapper, IPaymentFactory paymentFactory, IConfiguration configuration)
    {
        this.orderRepository = orderRepository;
        this.branchInventoryRepository = branchInventoryRepository;
        this.orderItemRepository = orderItemRepository;
        this.foodFlowContext = foodFlowContext;
        this.customerService = customerService;
        this.customerRepository = customerRepository;
        this.mapper = mapper;
        this.paymentGateway = paymentFactory.CreateGateway(configuration);
    }

    public async Task<Result<OrderDto>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var customerDetails = await customerService.GetCustomerDetailAsync();
        var customer = (await this.customerRepository.GetAllAsync((e) => e.ExternalId == customerDetails.ExternalId, e => e.CreatedAt,
        cancellationToken: cancellationToken)).FirstOrDefault();
        if (customer == null)
        {
            //create a customer
            customer = new Customer(customerDetails.Name, customerDetails.Email, customerDetails.UserName, customerDetails.ExternalId);
            await customerRepository.AddAsync(customer, cancellationToken);
            await foodFlowContext.SaveChangesAsync(cancellationToken);
        }
        //create the order
        var billingAddress = this.mapper.Map<Address>(request.BillingAddress);
        var deliveryAddress = this.mapper.Map<Address>(request.DeliveryAddress);
        var order = new Order(customer.Id, request.ShippingCost, request.BranchId, billingAddress, deliveryAddress);
        foreach (var oi in request.OrderItems)
        {
            var branchInventory =
                await branchInventoryRepository.GetByIdAsync(
                    oi.BranchInventoryId,
                    cancellationToken);

            if (branchInventory != null)
            {
                branchInventory.RemoveQuantity(oi.Quantity);

                await branchInventoryRepository.UpdateAsync(
                    branchInventory,
                    cancellationToken);
            }

            order.AddOrderItem(
                new OrderItem(
                    oi.BranchInventoryId,
                    oi.Quantity,
                    oi.ItemName,
                    oi.UnitPrice,
                    oi.Sku));
        }
        await orderRepository.AddAsync(order, cancellationToken);
        await foodFlowContext.SaveChangesAsync(cancellationToken);
        var externalId = await paymentGateway.CreateOrder(order);
        order.UpdateExternalId(externalId);
        await orderRepository.UpdateAsync(order, cancellationToken);
        await foodFlowContext.SaveChangesAsync(cancellationToken);
        var createdOrder = this.mapper.Map<OrderDto>(order);
        createdOrder.OrderTotal = order.CalculateTotalPrice();
        return Result<OrderDto>.SetSuccess(createdOrder, null);
    }
}