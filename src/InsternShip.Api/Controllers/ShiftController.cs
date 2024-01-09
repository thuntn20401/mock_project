using InsternShip.Data.ViewModels.Shift;
using InsternShip.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InsternShip.Api.Controllers
{
    public class ShiftController : BaseAPIController
    {
        private readonly IShiftService _shiftService;

        public ShiftController(IShiftService shiftService)
        {
            _shiftService = shiftService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllShift(int? query)
        {
            var shiftList = await _shiftService.GetAllShifts(query);
            if (shiftList == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return Ok(shiftList);
        }

        [HttpPost]
        public async Task<IActionResult> SaveShift(ShiftAddModel shiftModel)
        {
            if (shiftModel == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            var shiftList = await _shiftService.SaveShift(shiftModel);
            return Ok(shiftList);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateShift(ShiftUpdateModel shiftModel, Guid id)
        {
            if (id == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            var shiftList = await _shiftService.UpdateShift(shiftModel, id);
            return Ok(shiftList);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteRound(Guid id)
        {
            if (id != null)
            {
                var roundlist = await _shiftService.DeleteShift(id);
                return Ok(roundlist);
            }
            return StatusCode(StatusCodes.Status400BadRequest);
        }
    }
}