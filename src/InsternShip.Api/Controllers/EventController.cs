using InsternShip.Data.ViewModels.Event;
using InsternShip.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InsternShip.Api.Controllers;

public class EventController : BaseAPIController
{
    private readonly IEventService _EventService;

    public EventController(IEventService eventService)
    {
        _EventService = eventService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllEvent(Guid? id)
    {
        if (id != null)
        {
            var data = await _EventService.GetEventById((Guid)id);
            return data switch
            {
                null => NotFound(),
                _ => Ok(data)
            };
        }

        var eventList = await _EventService.GetAllEvent();
        if (eventList == null)
        {
            return StatusCode(StatusCodes.Status404NotFound);
        }
        return Ok(eventList);
    }

    [HttpPost]
    public async Task<IActionResult> SaveEvent(EventAddModel request)
    {
        // TODO: Should return 201 Created if success
        var response = await _EventService.SaveEvent(request);
        if (response != null)
        {
            return Ok(response);
        }
        else
            return StatusCode(StatusCodes.Status404NotFound);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateEvent(EventUpdateModel request, Guid id)
    {
        var response = await _EventService.UpdateEvent(request, id);
        if (response == true)
        {
            return Ok(response);
        }
        else
            return StatusCode(StatusCodes.Status404NotFound);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteEvent(Guid id)
    {
        var response = await _EventService.DeleteEvent(id);
        if (response == true)
        {
            return Ok(response);
        }
        else
            return StatusCode(StatusCodes.Status404NotFound);
    }
}