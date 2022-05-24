using System;
using System.Threading.Tasks;
using Marketplace.Api.ApplicationService;
using Marketplace.Api.Contracts;
using Marketplace.Api.Handler;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Api.Controllers
{
    [Route("api/ad")]
    [ApiController]
    public class ClassifiedAdsCommandsApi : ControllerBase
    {
        private readonly ClassifiedAdsApplicationService _applicationService;

        public ClassifiedAdsCommandsApi(ClassifiedAdsApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Contracts.ClassifiedAds.V1.Create request)
        {
            await _applicationService.HandleAsync(request);
            return Ok();
        }

        [Route("name")]
        [HttpPut]
        public async Task<IActionResult> Put(ClassifiedAds.V1.SetTitle request)
        {
            await _applicationService.HandleAsync(request);
            return Ok();
        }

        [Route("text")]
        [HttpPut]
        public async Task<IActionResult> Put(ClassifiedAds.V1.UpdateText request)
        {
            await _applicationService.HandleAsync(request);
            return Ok();
        }

        [Route("price")]
        [HttpPut]
        public async Task<IActionResult> Put(ClassifiedAds.V1.UpdatePrice request)
        {
            await _applicationService.HandleAsync(request);
            return Ok();
        }

        [Route("publish")]
        public async Task<IActionResult> Put(ClassifiedAds.V1.RequestToPublish request)
        {
            await _applicationService.HandleAsync(request);
            return Ok();

        }
    }
}
