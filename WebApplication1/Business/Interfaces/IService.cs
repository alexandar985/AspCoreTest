using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.Responses;

namespace WebApplication1.Business.Interfaces
{
    public interface IService
    {
        ExcModel GetDatesForApi(InputDto data);
        Task<string> GetJsonFromExternalApi(string startDate, string endDate, string baseCurrency, string targetCurrency);
        ResponseDto ReturnResultForThisTask(string json);
    }
}
