using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuperNews.Abstract;
using SuperNews.Domains;
using SuperNews.Models;
using System.Linq;

namespace SuperNews.Controllers
{
    [AllowAnonymous]
    public class NewsController : Controller
    {
        private readonly IRepository<News> _repositoryNews;

        public NewsController(IRepository<News> repositoryArticle)
        {
            _repositoryNews = repositoryArticle;
        }

        public IActionResult List()
        {
            var ArticleSample = _repositoryNews
                .GetList()
                .OrderBy(p => p.NewsId)
                .Take(20)
                .Select(e => new NewsShortModel(e));

            return View(ArticleSample);
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
                    CreationDate = model.CreationDate,
                    Description = model.Description,
                    ImageUrl = model.ImageUrl
                };

                _repositoryNews.Create(article);

                return RedirectToAction("List", "News");
            }

            return View(model);
        }

        public IActionResult DeleteArticle(long id)
        {
            _repositoryNews.Delete(id);

            return RedirectToAction("List", "News");
        }

        public IActionResult EditArticle(long id)
        {
            var entity = _repositoryNews.Read(id);
            var model = entity.Adapt<NewsViewModel>();

            return View(model);
        }

        [HttpPost]
        public IActionResult EditArticle(NewsViewModel model)
        {
            News manager = new News()
            {
                NewsId = model.NewsId,
                Title = model.Title,
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
