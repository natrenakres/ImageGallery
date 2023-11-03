using ImageGallery.Application.Images.AddImage;
using ImageGallery.Application.Images.EditImage;
using ImageGallery.Application.Images.GetImage;
using ImageGallery.Application.Images.GetImages;
using ImageGallery.Application.Images.RemoveImage;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImageGallery.API.Controllers;

[Route("api/images")]
[ApiController]
[Authorize]
public class ImagesController : ControllerBase
{
    private readonly ISender _sender;

    public ImagesController(ISender sender)
    {
        _sender = sender;
    }
    
    
    [HttpGet()]
    public async Task<IActionResult> GetImages()
    {
        var result = await _sender.Send(new GetImagesQuery());

        return Ok(result.Value);
    }
    
    [HttpGet("{id:guid}", Name = "GetImage")]
    [Authorize(Policy = "MustOwnImage")]
    public async Task<IActionResult> GetImage(Guid id)
    {
        var result = await _sender.Send(new GetImageQuery(id));

        if (result.IsFailed)
        {
            return NotFound(result.Errors);
        }

        return Ok(result.Value);
    }
    
    [HttpPost]
    //[Authorize(Roles = "PayingUser")]
    [Authorize(Policy = "UserCanAddImage")]
    [Authorize(Policy = "ClientApplicationCanWrite")]
    public async Task<IActionResult> AddImage([FromBody] AddImageRequest request, CancellationToken cancellationToken)
    {
        var command = new AddImageCommand(request.Title, request.Bytes);
        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailed)
        {
            return BadRequest(result.Errors);
        }

        var imageToReturn = result.Value;

        return CreatedAtRoute("GetImage", new { id = imageToReturn.Id }, imageToReturn);
    }
    
    [HttpPut("{id:guid}")]
    [Authorize(Policy = "MustOwnImage")]
    public async Task<IActionResult> UpdateImage(Guid id, [FromBody] EditImageRequest request)
    {
        var command = new EditImageCommand(id, request.Title);

        var result = await _sender.Send(command);

        if (result.IsFailed)
        {
            return BadRequest();
        }
        
        
        return NoContent();
    }
    
    
    [HttpDelete("{id}")]
    [Authorize(Policy = "MustOwnImage")]
    public async Task<IActionResult> DeleteImage(Guid id)
    {
        var command = new RemoveImageCommand(id);

        var result = await _sender.Send(command);

        if (result.IsFailed)
        {
            return BadRequest(result.Errors);
        }

        return NoContent();
    }
}