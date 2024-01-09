using InsternShip.Data.ViewModels.Certificate;
using InsternShip.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InsternShip.Api.Controllers
{
    public class CertificateController : BaseAPIController
    {
        private readonly ICertificateService _certificateService;

        public CertificateController(ICertificateService certificateService)
        {
            _certificateService = certificateService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCertificate(string? request)
        {
            var response = await _certificateService.GetAllCertificate(request);
            if (response == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> SaveCertificate(CertificateAddModel request)
        {
            var response = await _certificateService.SaveCertificate(request);
            if (response == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            return Ok(response);
        }

        [HttpPut("{requestId:guid}")]
        public async Task<IActionResult> UpdateCertificate(CertificateUpdateModel request, Guid requestId)
        {
            var response = await _certificateService.UpdateCertificate(request, requestId);
            if (response == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            return Ok(response);
        }

        [HttpDelete("{requestId:guid}")]
        public async Task<IActionResult> DeleteCertificate(Guid requestId)
        {
            var response = await (_certificateService.DeleteCertificate(requestId));
            if (response == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            return Ok(response);
        }
    }
}