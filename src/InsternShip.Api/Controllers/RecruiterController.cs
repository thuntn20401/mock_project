using InsternShip.Data.ViewModels.Recruiter;
using InsternShip.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InsternShip.Api.Controllers;

public class RecruiterController : BaseAPIController
{
    private readonly IRecruiterService _recruiterService;

    public RecruiterController(IRecruiterService recruiterService)
    {
        _recruiterService = recruiterService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllRecruiter(Guid? id)
    {
        if (id != null)
        {
            var data = await _recruiterService.GetRecruiterById((Guid)id);
            return data switch
            {
                null => NotFound(),
                _ => Ok(data)
            };
        }

        var reportList = await _recruiterService.GetAllRecruiter();
        return Ok(reportList);
    }

    [HttpPost]
    public async Task<IActionResult> SaveRecruiter(RecruiterAddModel request)
    {
        var response = await _recruiterService.SaveRecruiter(request);
        if (response != null)
        {
            return Ok(response);
        }
        return Ok("Can not create recruiter");
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateRecruiter(RecruiterUpdateModel request, Guid id)
    {
        return await _recruiterService.UpdateRecruiter(request, id) ? NoContent() : BadRequest();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteRecruiter(Guid id)
    {
        return await _recruiterService.DeleteRecruiter(id) ? NoContent() : NotFound();
    }
}