using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPrint.Models
{


    public interface ITrackable
    {
        [Column(TypeName = "DateTime2")]
        DateTime AddedOn { get; set; }
        int AddedBy { get; set; }
        [Column(TypeName = "DateTime2")]
        DateTime EditedOn { get; set; }
        int EditedBy { get; set; }
    }
    
}
