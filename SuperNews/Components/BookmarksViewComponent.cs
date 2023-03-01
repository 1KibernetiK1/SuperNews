using Microsoft.AspNetCore.Mvc;
using SuperNews.BusinessLogic;
using SuperNews.Helpers;

namespace SuperNews.Components
{
    [ViewComponent]
    public class BookmarksViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var bookmarks = HttpContext.LoadFromSession<Bookmarks>();
            return View(bookmarks);
        }
    }
}
