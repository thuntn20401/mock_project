using InsternShip.Data.ViewModels.Interview;
using InsternShip.Data.ViewModels.Round;
using InsternShip.Service;
using InsternShip.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InsternShip.Api.Controllers;

public class InterviewController : BaseAPIController
{
    private readonly IInterviewService _interviewService;
    private readonly IItrsinterviewService _itrsinterviewService;
    private readonly IRoundService _roundService;

    public InterviewController(IInterviewService interviewService,
        IItrsinterviewService itrsinterviewService,
        IRoundService roundService)
    {
        _interviewService = interviewService;
        _itrsinterviewService = itrsinterviewService;
        _roundService = roundService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllInterview(Guid? id, string? status)
    {
        // Get by id
        if (id != null)
        {
            var data = await _interviewService.GetInterviewById((Guid)id);
            return data switch
            {
                null => StatusCode(StatusCodes.Status404NotFound),
                _ => Ok(data)
            };
        }

        // Get by Interviewer
        if (status == null)
        {
            //var interviewByStatus = await _interviewService.GetAllInterviewByStatus(status);
            //if (interviewByStatus == null)
            //{
            //    return StatusCode(StatusCodes.Status204NoContent);
            //}
            //return Ok(interviewByStatus);
            status = "";
        }

        var reportList = await _interviewService.GetAllInterview(status);
        if (reportList == null)
        {
            return StatusCode(StatusCodes.Status204NoContent);
        }
        return Ok(reportList);
    }

    [HttpGet("[action]/{requestId}")]
    public async Task<IActionResult> GetInterviewsByInterviewer(Guid requestId)
    {
        if (requestId == null)
        {
            return BadRequest();
        }

        var dataList = await _interviewService.GetInterviewsByInterviewer(requestId);
        if (dataList == null)
        {
            return StatusCode(StatusCodes.Status204NoContent);
        }
        return Ok(dataList);
    }

    [HttpGet("[action]/{requestId}")]
    public async Task<IActionResult> GetInterviewsByPositon(Guid requestId)
    {
        if (requestId == null)
        {
            return BadRequest();
        }

        var dataList = await _interviewService.GetInterviewsByPositon(requestId);
        if (dataList == null)
        {
            return StatusCode(StatusCodes.Status204NoContent);
        }
        return Ok(dataList);
    }

    [HttpGet("[action]/{requestId}")]
    public async Task<IActionResult> GetInterviewsByDepartment(Guid requestId)
    {
        if (requestId == null)
        {
            return BadRequest();
        }

        var dataList = await _interviewService.GetInterviewsByDepartment(requestId);
        if (dataList == null)
        {
            return StatusCode(StatusCodes.Status204NoContent);
        }
        return Ok(dataList);
    }

    [HttpPost("{applicationId:guid}")]
    public async Task<IActionResult> SaveInterview(InterviewWithTimeAddModel request, Guid applicationId)
    {
        if ((request == null)
            || applicationId == null
            || request.Interview.ApplicationId != applicationId)
        {
            return StatusCode(StatusCodes.Status400BadRequest);
        }

        var responseITRS = await _itrsinterviewService.SaveItrsinterview(request.ITRS, request.Interview.InterviewerId);
        if (responseITRS!.ItrsinterviewId == Guid.Empty)
        {
            return StatusCode(StatusCodes.Status409Conflict);
        }

        request.Interview.ApplicationId = applicationId;
        request.Interview.ItrsinterviewId = responseITRS!.ItrsinterviewId;
        var responseInterview = await _interviewService.SaveInterview(request.Interview);

        if (responseInterview != null)
        {
            //return CreatedAtAction("Created Interview", request);
            return Ok();
        }
        return StatusCode(StatusCodes.Status409Conflict);
    }

    [HttpPost("[action]/{id:guid}")]
    public async Task<IActionResult> PostQuestionInterviewResult(InterviewResultQuestionModel request)
    {
        var postQuestionIntoInterview = await _interviewService.PostQuestionIntoInterview(request);

        if (postQuestionIntoInterview == null)
        {
            return BadRequest(request);
        }
        else
        {
            return Ok(postQuestionIntoInterview);
        }
    }

    [HttpPut("{interviewId:guid}")]
    public async Task<IActionResult> UpdateInterview([FromBody] InterviewUpdateModel request, Guid interviewId)
    {
        if ((request == null)
            || interviewId == null
            || request.ApplicationId != interviewId)
        {
            return StatusCode(StatusCodes.Status400BadRequest);
        }

        // Delete old itrs
        var delITRS = await _interviewService.DeleteInterview((Guid)request.ItrsinterviewId);

        var responseITRS = await _itrsinterviewService.SaveItrsinterview(request.Itrsinterview, request.InterviewerId);
        if (responseITRS!.ItrsinterviewId == Guid.Empty)
        {
            return StatusCode(StatusCodes.Status409Conflict);
        }

        request.ApplicationId = interviewId;
        request.ItrsinterviewId = responseITRS!.ItrsinterviewId;
        var responseInterview = await _interviewService.UpdateInterview(request, interviewId);

        if (responseInterview == false)
        {
            return StatusCode(StatusCodes.Status409Conflict);
        }

        return Ok(request);
    }

    [HttpPut("[action]/{interviewId:guid}")]
    public async Task<IActionResult> UpdateStatusInterview(
        Guid interviewId,
        string? Candidate_Status,
        string? Company_Status
    )
    {
        if (Candidate_Status == null && Company_Status == null)
            return StatusCode(StatusCodes.Status400BadRequest);

        var interviewNewStatus = await _interviewService.GetInterviewById(interviewId);

        if (interviewNewStatus == null)
        {
            return BadRequest();
        }

        var response = await _interviewService.UpdateStatusInterview(interviewId, Candidate_Status, Company_Status);
        return Ok(response);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteInterview(Guid id)
    {
        return await _interviewService.DeleteInterview(id) ? NoContent() : StatusCode(StatusCodes.Status404NotFound); ;
    }
}