using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OrganizedMorning.Entities;
using OrganizedMorning.Models;
using OrganizedMorning.OrganizedMorning;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace OrganizedMorning.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
		private readonly DbContextOptions<OrganizedMorningDbContext> _options;


		public HomeController(ILogger<HomeController> logger, UserManager<IdentityUser> userManager, DbContextOptions<OrganizedMorningDbContext> options)
        {
            _logger = logger;
            _userManager = userManager;
			_options = options;
		}

        
        public async Task<IActionResult> Index()
        {
            var MorningPlans = new List<MorningPlan>();
            var model = new List<IndexModel>();
            var context = new OrganizedMorningDbContext(_options);

			var user = await _userManager.GetUserAsync(HttpContext.User);
            var userId = user.Id;

			MorningPlans = context.MorningPlans.Where(mp => mp.UserId == userId)
                .OrderByDescending(mp => mp.Created)
                .ToList();

            foreach(MorningPlan morningPlan in MorningPlans)
            {
                IndexModel indexModel = new IndexModel();
                indexModel.Id = morningPlan.Id;
                indexModel.Title = morningPlan.Title;
                indexModel.BaseTime = morningPlan.BaseTime;
                indexModel.EncodedTitle = morningPlan.EncodedTitle;

                model.Add(indexModel);
            }

            return View(model);
        }

        public IActionResult NotAuthorized()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(MorningCreateModel model)
        {
            if (!ModelState.IsValid)
            {

                return View(model);
            }

            using (var context = new OrganizedMorningDbContext(_options))
            {
                int order = 0;
                MorningPlan morningPlan = new MorningPlan();
                morningPlan.Title = model.MorningPlanTitle;
                morningPlan.BaseTime = model.MorningPlanBaseTime;
                var user =  await _userManager.GetUserAsync(HttpContext.User);
                morningPlan.UserId = user.Id;
                morningPlan.EncodeTitle();

                context.MorningPlans.Add(morningPlan);
                foreach (Stages stage in model.MorningStages)
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

        
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            using (var context = new OrganizedMorningDbContext(_options))
            {
                var morningPlan = await context.MorningPlans.FindAsync(id);
                var timesToRemove = await context.Times.Where(t => t.MorningPlan.Id == id).ToListAsync();
                context.Times.RemoveRange(timesToRemove);
                context.MorningPlans.Remove(morningPlan);

                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
        }

		//[Route("{encodedName}/Details")]
		//public async Task<IActionResult> Details(string encodedTitle)
		//{
		//	using (var context = new OrganizedMorningDbContext())
  //          {
		//		MorningPlan morningPlan = context.MorningPlans.FirstOrDefault(x => x.EncodedTitle == encodedTitle);
  //              var stages = context.Times.Where(x => x.MorningPlan.Id == morningPlan.Id).ToList();
  //              MorningModel morningModel = new MorningModel();
  //              morningModel.MorningPlanId = morningPlan.Id;
  //              morningModel.MorningPlanTitle = morningPlan.Title;
  //              morningModel.MorningPlanBaseTime = morningPlan.BaseTime;
  //              morningModel.MorningPlanEncodedTitle = morningPlan.EncodedTitle;
  //              morningModel.MorningStages = stages;

  //              var user = await _userManager.GetUserAsync(HttpContext.User);
  //              var userId = user.Id;
  //              if(userId == morningPlan.UserId)
  //              {
  //                  return View(morningModel);
  //              }
  //              else
  //              {
  //                  return Redirect(nameof(NotAuthorized));
  //              }
  //          }
		//}

        [Route("{id}/Details")]
        public async Task<IActionResult> Details(int id)
        {
            using (var context = new OrganizedMorningDbContext(_options))
            {
                MorningPlan morningPlan = context.MorningPlans.FirstOrDefault(x => x.Id == id);
                //var stages = context.Times.Where(x => x.MorningPlan.Id == morningPlan.Id).ToList();

                var stages = new List<Times>();
                if (morningPlan != null)
                {
                    stages = context.Times.Where(x => x.MorningPlan.Id == morningPlan.Id).ToList();
                }

                MorningModel morningModel = new MorningModel();
                morningModel.MorningPlanId = morningPlan.Id;
                morningModel.MorningPlanTitle = morningPlan.Title;
                morningModel.MorningPlanBaseTime = morningPlan.BaseTime;
                morningModel.MorningPlanEncodedTitle = morningPlan.EncodedTitle;
                morningModel.MorningStages = stages;

                var user = await _userManager.GetUserAsync(HttpContext.User);
                var userId = user.Id;
                if (userId == morningPlan.UserId)
                {
                    return View(morningModel);
                }
                else
                {
                    return Redirect(nameof(NotAuthorized));
                }
            }
        }

        [Route("{id}/Edit")]
        public async Task<IActionResult> Edit(int id)
        {
            using (var context = new OrganizedMorningDbContext(_options))
            {
                MorningPlan morningPlan = context.MorningPlans.FirstOrDefault(x => x.Id == id);
                MorningModel morningModel = new MorningModel();
                morningModel.MorningPlanId = morningPlan.Id;
                morningModel.MorningPlanBaseTime = morningPlan.BaseTime;

                return View(morningModel);
            }
        }

        [Route("{id}/EditDb")]
        public async Task<IActionResult> EditDb(int id, TimeSpan baseTime)
        {
            using(var context  = new OrganizedMorningDbContext(_options))
            {
                var morningPlan = context.MorningPlans.FirstOrDefault(mp => mp.Id == id);
                morningPlan.BaseTime = baseTime;
                await context.SaveChangesAsync();
            }

			return RedirectToAction("Details", new { id = id });

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