using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TalTech.Data;
using TalTech.Models;

namespace TalTech.Courses
{
    public class IndexModel : PageModel
    {
        private readonly TalTech.Data.TalTechContext _context;

        public IndexModel(TalTech.Data.TalTechContext context)
        {
            _context = context;
        }

        public IList<Course> Course { get;set; }

        public async Task OnGetAsync()
        {
            Course = await _context.Courses
                .Include(c => c.Department).ToListAsync();
        }
    }
}
