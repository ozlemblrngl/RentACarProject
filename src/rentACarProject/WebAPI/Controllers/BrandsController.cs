using Application.Features.Brands.Commands.Create;
using Application.Features.Brands.Commands.Delete;
using Application.Features.Brands.Commands.Update;
using Application.Features.Brands.Queries.GetById;
using Application.Features.Brands.Queries.GetList;
using Microsoft.AspNetCore.Mvc;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BrandsController : BaseController
{
    [HttpPost]

    // burada command'im içinde name olan bir şey
    // Mediator'e diyoruz ki elimde bir command var ve vir commandi sadece bir handler ile kullanabiliriz.
    public async Task<IActionResult> Add([FromBody] CreateBrandCommand createBrandCommand)
    {
        CreatedBrandResponse response = await Mediator.Send(createBrandCommand);  // burada bunun handler ı kim diyoruz, belleğinden bakıyor. CreateBrandCommand içindeki CreateBrandCommandHandlerı (class) buluyor ve onun Handle metodunu çalıştırıyor

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateBrandCommand updateBrandCommand)
    {
        UpdatedBrandResponse response = await Mediator.Send(updateBrandCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedBrandResponse response = await Mediator.Send(new DeleteBrandCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdBrandResponse response = await Mediator.Send(new GetByIdBrandQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListBrandQuery getListBrandQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListBrandListItemDto> response = await Mediator.Send(getListBrandQuery);
        return Ok(response);
    }
}