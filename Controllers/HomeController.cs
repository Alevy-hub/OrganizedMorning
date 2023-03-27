using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrganizedMorning.Entities;
using OrganizedMorning.Migrations;
using OrganizedMorning.Models;
using OrganizedMorning.OrganizedMorning;
using System.Diagnostics;

namespace OrganizedMorning.Controllers
{
	public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var MorningPlans = new List<MorningPlan>();
            var model = new List<IndexModel>();
            var context = new OrganizedMorningDbContext();
            MorningPlans = context.MorningPlans.ToList();

            foreach(MorningPlan morningPlan in MorningPlans)
            {
                IndexModel indexModel = new IndexModel();
                indexModel.Title = morningPlan.Title;
                indexModel.BaseTime = morningPlan.BaseTime;
                indexModel.EncodedTitle = morningPlan.EncodedTitle;

                model.Add(indexModel);
            }

            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string title, TimeSpan BaseTime, List<Stages> stages)
        {
            using (var context = new OrganizedMorningDbContext())
            {
                int order = 0;
                MorningPlan morningPlan = new MorningPlan();
                morningPlan.Title = title;
                morningPlan.BaseTime = BaseTime;
                morningPlan.EncodeTitle();

                context.MorningPlans.Add(morningPlan);
                foreach (Stages stage in stages)
                {
                    Times times = new Times();
                    times.Title = stage.StageTitle;
                    times.Time = stage.StageTime;
                    times.Order = order;
                    times.EncodeTitle();
                    times.MorningPlan = morningPlan;

                    context.Times.Add(times);

                    order++;
                }
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
        }

		[Route("{encodedName}/Details")]
		public async Task<IActionResult> Details(string encodedTitle)
		{
			using (var context = new OrganizedMorningDbContext())
            {
				MorningPlan morningPlan = context.MorningPlans.FirstOrDefault(x => x.EncodedTitle == encodedTitle);
				return View(morningPlan);
            }
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