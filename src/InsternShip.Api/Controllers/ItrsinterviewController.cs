using InsternShip.Data.ViewModels.Itrsinterview;
using InsternShip.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InsternShip.Api.Controllers;

public class ItrsinterviewController : BaseAPIController
{
    private readonly IItrsinterviewService _itrsinterviewService;

    public ItrsinterviewController(IItrsinterviewService itrsinterviewService)
    {
        _itrsinterviewService = itrsinterviewService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllItrsinterview(Guid? id)
    {
        if (id != null)
        {
            var data = await _itrsinterviewService.GetItrsinterviewById((Guid)id);
            return data switch
            {
                null => StatusCode(StatusCodes.Status404NotFound),
                _ => Ok(data)
            };
        }
        var reportList = await _itrsinterviewService.GetAllItrsinterview();
        if (reportList == null)
        {
            return StatusCode(StatusCodes.Status404NotFound);
        }
        return Ok(reportList);
    }

    [HttpPost]
    public async Task<IActionResult> SaveItrsinterview(ItrsinterviewAddModel request, Guid interviewerId)
    {
        if ((request == null))
        {
            return StatusCode(StatusCodes.Status400BadRequest);
        }

        var response = await _itrsinterviewService.SaveItrsinterview(request, interviewerId);
        if (response!.ItrsinterviewId != Guid.Empty)
        {
            return Ok(response);
        }
        return StatusCode(StatusCodes.Status409Conflict);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateItrsinterview(ItrsinterviewUpdateModel request, Guid id, Guid interviewerId)
    {
        return await _itrsinterviewService.UpdateItrsinterview(request, id, interviewerId) ? NoContent() : StatusCode(StatusCodes.Status404NotFound);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteItrsinterview(Guid id)
    {
        return await _itrsinterviewService.DeleteItrsinterview(id) ? NoContent() : StatusCode(StatusCodes.Status404NotFound);
    }
}