﻿using InsternShip.Data.ViewModels.Requirement;
using InsternShip.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InsternShip.Api.Controllers
{
    public class RequirementController : BaseAPIController
    {
        private readonly IRequirementService _reportService;

        public RequirementController(IRequirementService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRequirement()
        {
            var reportList = await _reportService.GetAllRequirement();
            return Ok(reportList);
        }

        [HttpPost]
        public async Task<IActionResult> SaveRequirement(RequirementAddModel request)
        {
            var reportList = await _reportService.SaveRequirement(request);
            return Ok(reportList);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateRequirement(RequirementUpdateModel request, Guid id)
        {
            var reportList = await _reportService.UpdateRequirement(request, id);
            return Ok(reportList);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteRequirement(Guid id)
        {
            var reportList = await _reportService.DeleteRequirement(id);
            return Ok(reportList);
        }
    }
}