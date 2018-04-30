using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HaroldAdviser.Data
{
    public class Repository
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
        public Guid Id { get; set; }

        public string Url { get; set; }

        public string UserId { get; set; }

        public bool Checked { get; set; }

        public IList<Warning> Warnings { get; set; }
    }
}
