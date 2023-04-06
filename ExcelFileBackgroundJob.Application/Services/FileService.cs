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
        try
        {
            using var stream = new FileStream(saveToPath, FileMode.Open);
            if (saveToPath.Contains(".xlsx"))
                reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            else
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
                    TblCustomer details = new ();
                    TblLog log = new();
                    details.Name = serviceDetails.Rows[i][0].ToString();
                    details.LastName = serviceDetails.Rows[i][1].ToString();
                    details.Phone = serviceDetails.Rows[i][2].ToString();
                    details.Email = serviceDetails.Rows[i][3].ToString();
                    details.Country = serviceDetails.Rows[i][4].ToString();
                    if (string.IsNullOrEmpty(details.Name))
                    {
                        log.Description += "'Name '";
                    }
                    if (string.IsNullOrEmpty(details.LastName))
                    {
                        log.Description += "'LastName '";
                    }
                    if (string.IsNullOrEmpty(details.Email))
                    {
                        log.Description += "'Email '";
                    }
                    if (!string.IsNullOrEmpty(log.Description))
                    {
                        log.ErrorType = 1;
                        log.UserId = 1;
                        log.Application = "ExcelFileBackgroundJob";
                        log.Module = nameof(FileService);
                        log.Method = nameof(ProcessFileAsync);
                        log.ErrorDate = DateTime.UtcNow;

                        await appDBContext.ExcelLogs.AddAsync(log);
                    }
                    else
                    {
                        await appDBContext.Customer.AddAsync(details);
                    }
                    await appDBContext.SaveChangesAsync();
                }
            }
        }
        catch (Exception ex)
        {

            throw;
        }
        
    }
}
