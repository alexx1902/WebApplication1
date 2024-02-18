using WebApplication1.Models;

namespace WebApplication1.Interfaces
{
    public interface IResultRepository
    {
        List<ResultModel> GetResults();
    }
}
