using FoodFlow.Application.DTOModels;
using MediatR;

public class FilteredRestaurantRequest : IRequest<IEnumerable<RestaurantDto>>
{
    #region filterable/searchable
    public string? Name { get; set; } // Restaurant name
    public string? FNumber { get; set; } // FNumber might be non-clustered
    public string? GstNumber { get; set; } // FNumber might be non-clustered
    public string? Status { get; set; } // Assuming status is an indexed column
    #endregion

    public RestaurantSortField SortBy { get; set; } = RestaurantSortField.Name;

    public SortDirection SortDirection { get; set; } = SortDirection.Asc;
}

public enum RestaurantSortField
{
    Name,
    FNumber,
    GstNumber,
    Status
}

public enum SortDirection
{
    Asc,
    Desc
}

