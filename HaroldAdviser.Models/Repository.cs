using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HaroldAdviser.Models
{
    public class Repository
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        public string Url { get; set; }

        public string UserId { get; set; }

        public string Token { get; set; }

        public IList<LogModel> Logs { get; set; }
    }
}
