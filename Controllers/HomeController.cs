using Microsoft.AspNetCore.Mvc;

namespace AMassage.Controllers
{
    /// <summary>
    /// Основной контроллер для обработки запросов главной страницы
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Действие, которое возвращает главную страницу
        /// </summary>
        public IActionResult Index()
        {
            return View();
        }
    }
}