using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TalTech.Models;
using TalTech.Data;

namespace TalTech
{
    public class IndexModel : PageModel
    {
        private readonly TalTech.Data.TalTechContext _context;

        public IndexModel(TalTech.Data.TalTechContext context)
        {
            _context = context;
        }

        public IList<Student> Student { get;set; }

        public async Task OnGetAsync()
        {
            Student = await _context.Students.ToListAsync();
        }
    }
}
