using ExcelDataReader;
using ExcelFileBackgroundJob.Application.Interfaces;
using ExcelFileBackgroundJob.Core.Entities;
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
        ds = reader.AsDataSet();
        reader.Close();
        
        if (ds != null && ds.Tables.Count > 0)
        {
            // Read the the Table
            DataTable serviceDetails = ds.Tables[0];
            

            for (int i = 1; i < serviceDetails.Rows.Count; i++)
            {
                tblCustomer details = new tblCustomer();
                tblLog log = new tblLog();
                details.Name = serviceDetails.Rows[i][0].ToString();
                details.LastName = serviceDetails.Rows[i][1].ToString();
                details.Phone = serviceDetails.Rows[i][3].ToString();
                details.Email = serviceDetails.Rows[i][2].ToString();
                details.Country = serviceDetails.Rows[i][4].ToString();
                if (string.IsNullOrEmpty(details.Phone))
                {
                    log.Description = "'Phone '";
                }
                if (string.IsNullOrEmpty(details.Country))
                {
                    log.Description = "'Country '";
                }
                if (log != null)
                {
                    await appDBContext.ExcelLogs.AddAsync(log);
                }
                else {
                    await appDBContext.Customer.AddAsync(details);
                }
                
            }
        }
    }
}
