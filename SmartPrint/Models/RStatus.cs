using System.ComponentModel.DataAnnotations;

namespace SmartPrint.Models
{
    public class RStatus
    {
        [Key]
        public int StatusId { get; set; }
        public string StatusName { get; set; }
    }
}