using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SuperNews.Abstract;
using SuperNews.DataAccessLayer;
using SuperNews.Domains;
using SuperNews.Models;
using System.Collections.Generic;
using System.Linq;

namespace SuperNews.Controllers
{
    [AllowAnonymous]
    public class NewsController : Controller
    {
        private readonly IRepository<News> _repositoryNews;
        private readonly ApplicationDbContext _context;

        public NewsController(IRepository<News> repositoryArticle, ApplicationDbContext context)
        {
            _repositoryNews = repositoryArticle;
            _context = context; 
        }

        public IActionResult List(int? Rubric, string? name)
        {
            IQueryable<News> news = _context.News.Include(p => p.NewsRubric);

            if (Rubric != null && Rubric != 0)
            {
                news = news.Where(p => p.RubricId == Rubric);
            }
            if (!string.IsNullOrEmpty(name))
            {
                news = news.Where(p => p.Title!.Contains(name));
            }

            List<Rubric> companies = _context.Rubrics.ToList();

            // устанавливаем начальный элемент, который позволит выбрать всех
            companies.Insert(0, new Rubric { Name = "Все", RubricId = 0 });

            NewsListViewModel viewModel = new NewsListViewModel
            {
                News = news.ToList(),
                Rubrics = new SelectList(companies, "RubricId", "Name", Rubric),
                Name = name
            };
            return View(viewModel);

        }

        public IActionResult Details(long id)
        {
            var entity = _repositoryNews.Read(id);
            var model = entity.Adapt<NewsViewModel>();

            return View(model);
        }

        public IActionResult Create(long id)
        {
            var manager = _repositoryNews.Read(id);

            return View(manager);
        }

        [HttpPost]
        public IActionResult Create(NewsViewModel model)
        {
            if (ModelState.IsValid)
            {
                News article = new News()
                {
                    NewsId = model.NewsId,
                    Title = model.Title,
                    RubricId = model.Rubric,
                    CreationDate = model.CreationDate,
                    Description = model.Description,
                    ImageUrl = model.ImageUrl
                };

                _repositoryNews.Create(article);

                return RedirectToAction("List", "News");
            }

            return View(model);
        }

        public IActionResult Delete(long id)
        {
            _repositoryNews.Delete(id);

            return RedirectToAction("List", "News");
        }

        public IActionResult Edit(long id)
        {
            var entity = _repositoryNews.Read(id);
            var model = entity.Adapt<NewsViewModel>();

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(NewsViewModel model)
        {
            News manager = new News()
            {
                NewsId = model.NewsId,
                Title = model.Title,
                RubricId = model.Rubric,
                CreationDate = model.CreationDate,
                Description = model.Description,
                ImageUrl = model.ImageUrl

            };

            if (ModelState.IsValid)
            {
                _repositoryNews.Update(manager);
                return RedirectToAction("List", new { id = model.NewsId });
            }

            return View();
        }
    }
}
