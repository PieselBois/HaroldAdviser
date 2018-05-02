﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HaroldAdviser.Data.Enums;

namespace HaroldAdviser.Data
{
    public class Pipeline
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
        public Guid Id { get; set; }

        public PipelineStatus Status { get; set; }

        public string CloneUrl { get; set; }

        public virtual List<Log> Logs { get; set; }

        public virtual List<Warning> Warnings { get; set; }

        public virtual Repository Repository { get; set; }
    }
}