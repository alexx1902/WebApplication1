using WebApplication1.Models;

namespace WebApplication1.Repository
{
    public class ResultRepository
    {
        private readonly DBContext _context;

        public ResultRepository(DBContext context)
        {
            _context = context;
        }

        public List<ResultModel> GetResults()
        {
            return _context.Results.OrderBy(p => p.Id).ToList();
        }
    }
}

