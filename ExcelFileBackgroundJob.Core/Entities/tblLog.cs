namespace ExcelFileBackgroundJob.Core.Entities
{
    public class TblLog
    {
        public int LogId { get; set; }
        public int ErrorType { get; set; }
        public string Description { get; set; } = string.Empty;
        public int UserId { get; set; }
        public string? Application { get; set; }
	    public string? Module { get; set; }
	    public string? Method { get; set; }
        public DateTime? ErrorDate { get; set; }
    }
}
