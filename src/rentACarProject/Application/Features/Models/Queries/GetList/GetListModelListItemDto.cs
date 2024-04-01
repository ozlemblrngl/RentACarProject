using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Models.Queries.GetList;

public class GetListModelListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid BrandId { get; set; }
    public string Name { get; set; }
    public decimal DailyPrice { get; set; }
    public string UmageUrl { get; set; }
}