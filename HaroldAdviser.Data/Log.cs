﻿using HaroldAdviser.Data.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HaroldAdviser.Data
{
    public class Log
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
        public Guid Id { get; set; }

        public LogType Type { get; set; }

        public string Module { get; set; }

        public string Value { get; set; }
    }
}