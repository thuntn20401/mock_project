using InsternShip.Data.ViewModels.Position;
using InsternShip.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InsternShip.Api.Controllers
{
    public class PositionController : BaseAPIController
    {
        private readonly IPositionService _positionService;

        public PositionController(IPositionService positionService)
        {
            _positionService = positionService;
        }

        [HttpPost]
        public async Task<IActionResult> AddPosition(PositionAddModel position)
        {
            var response = await _positionService.AddPosition(position);
            return response is not null ? CreatedAtAction(nameof(AddPosition), response)
                                        : BadRequest(position);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPositions(Guid? departmentId)
        {
            List<PositionViewModel> response = await _positionService.GetAllPositions(departmentId);
            return Ok(response);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetPositionById(Guid positionId)
        {
            var response = await _positionService.GetPositionById(positionId);

            return response is not null ? Ok(response) : NotFound();
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetPositionByName(string positionName)
        {
            var response = await _positionService.GetPositionByName(positionName);
            return response is not null ? Ok(response) : NotFound(positionName);
        }

        [HttpDelete("{positionId:guid}")]
        public async Task<IActionResult> RemovePosition(Guid positionId)
        {
            var response = await _positionService.RemovePosition(positionId);
            return response is true ? NoContent() : NotFound(positionId);
        }

        [HttpPut("{positionId:guid}")]
        public async Task<IActionResult> UpdatePosition
        (PositionUpdateModel position, Guid positionId)
        {
            var response = await _positionService.UpdatePosition(position, positionId);
            return response is true ? NoContent() : NotFound();
        }
    }
}