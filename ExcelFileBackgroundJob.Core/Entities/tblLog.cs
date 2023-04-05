using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelFileBackgroundJob.Core.Entities
{
    public class tblLog
    {
        
        public int LogId { get; set; }

        public int ErrorType { get; set; }

        public string? Description { get; set; }

        public int UserId { get; set; }

        public string? Application { get; set; }
	    public string? Modules { get; set; }
	    public string? Method { get; set; }
	    public DateTime? ErrorDate { get; set; }

    }
}
