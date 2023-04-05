using ExcelDataReader;
using ExcelFileBackgroundJob.Application.Interfaces;
using ExcelFileBackgroundJob.Infrastructure.DataAccess;
using System.Data;

namespace ExcelFileBackgroundJob.Application.Services;

public class FileService : IFileService
{
    private readonly AppDBContext appDBContext;
    IExcelDataReader reader;

    public FileService(AppDBContext _appDBContext)
    {
        appDBContext = _appDBContext;
    }

    public async Task ProcessFileAsync(string saveToPath)
    {
        using var stream = new FileStream(saveToPath, FileMode.Open);
        reader = ExcelReaderFactory.CreateBinaryReader(stream);

        DataSet ds = new DataSet();
        //ds = reader.AsDataSet();
        //reader.Close();
        //var sw = Stopwatch.StartNew();
        //if (ds != null && ds.Tables.Count > 0)
        //{
        //    // Read the the Table
        //    DataTable serviceDetails = ds.Tables[0];
        //    // Create Storage Queue Message
        //    _ = WorkerService.AzureQueueWorker.InsertMsgAsync(serviceDetails, _azureConnectionString);

        //    for (int i = 1; i < serviceDetails.Rows.Count; i++)
        //    {
        //        CustomerResponseDetail details = new CustomerResponseDetail();
        //        details.ServiceEngineerName = serviceDetails.Rows[i][0].ToString();
        //        details.CustomerName = serviceDetails.Rows[i][1].ToString();
        //        details.Country = serviceDetails.Rows[i][3].ToString();
        //        details.City = serviceDetails.Rows[i][2].ToString();
        //        details.ComplaintType = serviceDetails.Rows[i][4].ToString();
        //        details.DeviceName = serviceDetails.Rows[i][5].ToString();
        //        details.ComplaintDate = Convert.ToDateTime(serviceDetails.Rows[i][6].ToString());
        //        details.VisitDate = Convert.ToDateTime(serviceDetails.Rows[i][7].ToString());
        //        details.ComplaintDetails = serviceDetails.Rows[i][8].ToString();
        //        details.RepairDetails = serviceDetails.Rows[i][9].ToString();
        //        details.ResolveDate = Convert.ToDateTime(serviceDetails.Rows[i][10].ToString());

        //        details.Fees = Convert.ToDecimal(serviceDetails.Rows[i][11].ToString());
        //        details.UploadDate = DateTime.Now;

        //        // Add the record in Database
        //        await context.CustomerResponseDetails.AddAsync(details);
        //        await context.SaveChangesAsync();
        //    }
        //}
    }
}
