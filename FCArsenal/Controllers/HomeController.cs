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
			IQueryable<SigningDateGroup> data =
				from player in _context.Players
				group player by player.SigningDate into dateGroup
				select new SigningDateGroup()
				{
					SigningDate = dateGroup.Key,
					PlayerCount = dateGroup.Count()
				};
			return View(await data.AsNoTracking().ToListAsync());
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
