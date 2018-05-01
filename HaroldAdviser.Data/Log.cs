using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HaroldAdviser.Data.Enums;

namespace HaroldAdviser.Data
{
    public class Log
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
        public LogType Type { get; set; }

        public string Module { get; set; }
        
        public string Value { get; set; }
    }
}