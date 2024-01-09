using InsternShip.Data.ViewModels.Interviewer;
using InsternShip.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InsternShip.Api.Controllers;

public class InterviewerController : BaseAPIController
{
    private readonly IInterviewerService _interviewerService;

    public InterviewerController(IInterviewerService interviewerService)
    {
        _interviewerService = interviewerService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllInterviewer(Guid? id, Guid? departmentId)
    {
        //if (departmentId != null)
        //{
        //    var response = await _interviewerService.getInterviewersInDepartment((Guid)departmentId);
        //}
        if (id != null)
        {
            var data = await _interviewerService.GetInterviewerById((Guid)id);
            return data switch
            {
                null => NotFound(),
                _ => Ok(data)
            };
        }

        else if (departmentId != null)
        {
            var response = await _interviewerService.getInterviewersInDepartment((Guid)departmentId);

            if (response != null)
            {
                return Ok(response);
            }
            return StatusCode(StatusCodes.Status204NoContent);
        }

        var reportList = await _interviewerService.GetAllInterviewer();
        if (reportList == null)
        {
            return StatusCode(StatusCodes.Status400BadRequest);
        }
        return Ok(reportList);
    }

    [HttpPost]
    public async Task<IActionResult> SaveInterviewer(InterviewerAddModel request)
    {
        var response = await _interviewerService.SaveInterviewer(request);
        if (response != null)
        {
            return Ok(response);
        }
        return Ok(null);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateInterviewer(InterviewerUpdateModel request, Guid id)
    {
        return await _interviewerService.UpdateInterviewer(request, id) ? NoContent() : BadRequest();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteInterviewer(Guid id)
    {
        return await _interviewerService.DeleteInterviewer(id) ? NoContent() : NotFound();
    }
}