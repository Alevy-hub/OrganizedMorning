﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrganizedMorning.Entities;
using OrganizedMorning.Migrations;
using OrganizedMorning.Models;
using OrganizedMorning.OrganizedMorning;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace OrganizedMorning.Controllers
{
	public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(ILogger<HomeController> logger, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var MorningPlans = new List<MorningPlan>();
            var model = new List<IndexModel>();
            var context = new OrganizedMorningDbContext();

			var user = await _userManager.GetUserAsync(HttpContext.User);
            var userId = user.Id;

			MorningPlans = context.MorningPlans.Where(mp => mp.UserId == userId).ToList();

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
                var user =  await _userManager.GetUserAsync(HttpContext.User);
                morningPlan.UserId = user.Id;
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

        
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            using (var context = new OrganizedMorningDbContext())
            {
                var morningPlan = await context.MorningPlans.FindAsync(id);
                var timesToRemove = await context.Times.Where(t => t.MorningPlan.Id == id).ToListAsync();
                context.Times.RemoveRange(timesToRemove);
                context.MorningPlans.Remove(morningPlan);

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
                var stages = context.Times.Where(x => x.MorningPlan.Id == morningPlan.Id).ToList();
                MorningModel morningModel = new MorningModel();
                morningModel.MorningPlanId = morningPlan.Id;
                morningModel.MorningPlanTitle = morningPlan.Title;
                morningModel.MorningPlanBaseTime = morningPlan.BaseTime;
                morningModel.MorningPlanEncodedTitle = morningPlan.EncodedTitle;
                morningModel.MorningStages = stages;

                

				return View(morningModel);
            }
		}

        [Route("{encodedName}/Edit")]
        public async Task<IActionResult> Edit(string encodedTitle)
        {
            using (var context = new OrganizedMorningDbContext())
            {
                MorningPlan morningPlan = context.MorningPlans.FirstOrDefault(x => x.EncodedTitle == encodedTitle);
                var stages = context.Times.Where(x => x.MorningPlan.Id == morningPlan.Id).ToList();
                MorningModel morningModel = new MorningModel();
                morningModel.MorningPlanId = morningPlan.Id;
                morningModel.MorningPlanTitle = morningPlan.Title;
                morningModel.MorningPlanBaseTime = morningPlan.BaseTime;
                morningModel.MorningPlanEncodedTitle = morningPlan.EncodedTitle;
                morningModel.MorningStages = stages;

                return View(morningModel);
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