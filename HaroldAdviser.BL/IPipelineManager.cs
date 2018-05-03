using HaroldAdviser.ViewModels;
using System;
using System.Threading.Tasks;

namespace HaroldAdviser.BL
{
    public interface IPipelineManager
    {
        Task<Result> CreatePipelineAsync(IWebhook webhook);

        Task ClosePipelineAsync(Guid pipelineId, PipelineResult model);

        Task<Result> StartPipelineAsync(Guid pipelineId);
    }
}