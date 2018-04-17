﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HaroldAdviser.Models
{
    public class LogModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        public string Kind { get; set; }

        public string File { get; set; }

        public string Lines { get; set; }

        public string Message { get; set; }
    }
}
