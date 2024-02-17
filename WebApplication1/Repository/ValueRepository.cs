using WebApplication1.Interfaces;
using WebApplication1.Models;
using System.Collections.Generic;
using System.Linq;

namespace WebApplication1.Repository
{
    public class ValueRepository : IValueRepository
    {
        private readonly DBContext _context;

        public ValueRepository(DBContext context)
        {
            _context = context;
        }

        public List<ValueModel> GetValues()
        {
            return _context.Value.OrderBy(p => p.Id).ToList();
        }
    }
}

