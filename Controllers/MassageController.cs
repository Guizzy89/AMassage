using Microsoft.AspNetCore.Mvc;
using AMassage.Data;

namespace AMassage.Controllers
{
    public class MassageController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MassageController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Massage
        public IActionResult Index()
        {
            return View(_context.Massages.ToList());
        }

        // GET: Massage/Details/{id}
        public IActionResult Details(int id)
        {
            var massage = _context.Massages.Find(id);
            if (massage == null)
                return NotFound();
            return View(massage);
        }

        // GET: Massage/Create
        public IActionResult Create() => View();

        // POST: Massage/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Massage massage)
        {
            if (!ModelState.IsValid)
                return View(massage);
            _context.Massages.Add(massage);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // GET: Massage/Edit/{id}
        public IActionResult Edit(int id)
        {
            var massage = _context.Massages.Find(id);
            if (massage == null)
                return NotFound();
            return View(massage);
        }

        // POST: Massage/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Massage massage)
        {
            if (!ModelState.IsValid || id != massage.Id)
                return View(massage);
            try
            {
                _context.Update(massage);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Unable to save changes.");
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Massage/Delete/{id}
        public IActionResult Delete(int id)
        {
            var massage = _context.Massages.Find(id);
            if (massage == null)
                return NotFound();
            return View(massage);
        }

        // POST: Massage/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var massage = _context.Massages.Find(id);
            _context.Massages.Remove(massage);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}