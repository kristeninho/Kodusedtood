using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FCArsenal.Models;
using FCArsenal.Data;
using Microsoft.EntityFrameworkCore;
using FCArsenal.Models.FootballViewModels;
using System.Data.Common;

namespace FCArsenal.Controllers
{
	public class HomeController : Controller
	{
		private readonly FootballContext _context;

		public HomeController(FootballContext context)
		{
			_context = context;
		}

		public async Task<ActionResult> About()
		{
            List<SigningDateGroup> groups = new List<SigningDateGroup>();
            var conn = _context.Database.GetDbConnection();
            try
            {
                await conn.OpenAsync();
                using (var command = conn.CreateCommand())
                {
                    string query = "SELECT SigningDate, COUNT(*) AS PlayerCount "
                        + "FROM Person "
                        + "WHERE Discriminator = 'Player' "
                        + "GROUP BY SigningDate";
                    command.CommandText = query;
                    DbDataReader reader = await command.ExecuteReaderAsync();

                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            var row = new SigningDateGroup { SigningDate = reader.GetDateTime(0), PlayerCount = reader.GetInt32(1) };
                            groups.Add(row);
                        }
                    }
                    reader.Dispose();
                }
            }
            finally
            {
                conn.Close();
            }
            return View(groups);
        }
        public IActionResult Index()
		{
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
