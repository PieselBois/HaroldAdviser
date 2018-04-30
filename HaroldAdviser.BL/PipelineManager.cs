using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HaroldAdviser.Data;
using HaroldAdviser.Data.Enums;
using HaroldAdviser.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace HaroldAdviser.BL
{
    public class PipelineManager : IPipelineManager
    {
        private readonly ApplicationContext _context;
        private ICloudInstanceManager _instanceManager;

        public PipelineManager(ApplicationContext context, ICloudInstanceManager instanceManager)
        {
            _context = context;
            _instanceManager = instanceManager;
        }

        public async Task<Result> CreatePipelineAsync(IWebhook webhook)
        {
            var repository = await _context.Repositories.Include(r => r.Settings)
                .FirstAsync(r => r.Url == webhook.HtmlUrl);

            var pipeline = new Pipeline
            {
                CloneUrl = webhook.CloneUrl,
                Status = PipelineStatus.Webhook,
                Logs = new List<Log>(),
                Warnings = new List<Warning>()
            };

            //TODO: check how lazy inicialization works with insert
            pipeline.Logs.Add(new Log
            {
                Type = LogType.Debug,
                Module = "HaroldAdviser",
                Value = "Pipeline created"
            });

            repository.Pipelines.Add(pipeline);

            await _context.SaveChangesAsync();

            await _instanceManager.CreateInstanceAsync(webhook, repository.Settings);

            return Result.Ok;
        }

        public async Task<Result> StartPipelineAsync(Guid pipelineId)
        {
            var pipeline = await _context.Pipelines.FirstAsync(p => p.Id == pipelineId);

            if (pipeline == null)
            {
                return new Result("Pipeline not found");
            }

            pipeline.Status = PipelineStatus.Started;
            await _context.SaveChangesAsync();
            return Result.Ok;
        }

        public async Task ClosePipelineAsync(Guid pipelineId, PipelineResult model)
        {
            //TODO: warnings should not only be added to list, but also need to add smth like warning version

            var pipeline = await _context.Pipelines.Include(p => p.Repository).FirstAsync(p => p.Id == pipelineId);

            if (model.Success)
            {
                //TODO: Change to warning view
                pipeline.Repository.Warnings.AddRange(model.Warnings.Select(w => new Warning
                {
                    File = w.File,
                    Kind = w.Kind,
                    Lines = w.Lines,
                    Message = w.Message
                }));

                pipeline.Status = PipelineStatus.Finished;
            }
            else
            {
                pipeline.Logs.AddRange(model.Errors.Select(e =>
                    new Log
                    {
                        Module = "Halcy",
                        Type = LogType.Error,
                        Value = e
                    }
                ));
                pipeline.Status = PipelineStatus.Failed;
            }

            await _context.SaveChangesAsync();
        }
    }
}