using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Models.Queries.GetList;

public class GetListModelListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid BrandId { get; set; }

    public string BrandName { get; set; }
    // BrandName şeklinde olursa mapler çünkü Brand'in içindeki Name e gidiyor. İsimlendirme böyle olmalı ki maplesin.
    public string Name { get; set; }
    public decimal DailyPrice { get; set; }
    public string UmageUrl { get; set; }
}