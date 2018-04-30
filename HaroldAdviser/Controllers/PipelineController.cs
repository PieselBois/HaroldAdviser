using System;
using System.Threading.Tasks;
using HaroldAdviser.BL;
using HaroldAdviser.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HaroldAdviser.Controllers
{
    public class PipelineController : BaseController
    {
        private readonly IPipelineManager _pipelineManager;

        public PipelineController(IPipelineManager pipelineManager)
        {
            _pipelineManager = pipelineManager;
        }

        [HttpPost, Route("api/Pipeline/Create")]
        public async Task<IActionResult> CreatePipeline(IWebhook webhook)
        {
            //TODO: handle github webhook to create pipeline

            var result = await _pipelineManager.CreatePipelineAsync(webhook);

            if (result.Success)
            {
                return Ok();
            }

            return BadRequest(result.Error);
        }

        [HttpPost, Route("api/Pipeline/Close/{pipelineId}")]
        public async Task<IActionResult> ClosePipeline([FromRoute] Guid pipelineId, PipelineResult model)
        {
            await _pipelineManager.ClosePipelineAsync(pipelineId, model);
            return Ok();
        }

        [HttpPost, Route("api/Pipeline/Start/{pipelineId}")]
        public async Task<IActionResult> StartPipeline([FromRoute] Guid pipelineId)
        {
            var result = await _pipelineManager.StartPipelineAsync(pipelineId);
            if (result.Success)
            {
                return Ok();
            }

            return BadRequest(result.Error);
        }
    }
}