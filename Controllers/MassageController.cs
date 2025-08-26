using AMassage.Data;
using AMassage.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] Massage massage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(massage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(massage);
        }

        // GET: Massage/Edit/{id}
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var massage = await _context.Massages.FindAsync(id);
            if (massage == null)
            {
                return NotFound();
            }
            return View(massage);
        }

        // POST: Massage/Edit/{id}
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] Massage massage)
        {
            if (id != massage.Id)
            {
                return NotFound(); // Идентификатор не совпадает
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Прямо обновляем существующую запись
                    _context.Update(massage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Massages.Any(e => e.Id == id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(massage); // Возврат к форме, если модель недействительна
        }

        // GET: Massage/Delete/{id}
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var massage = await _context.Massages.FirstOrDefaultAsync(m => m.Id == id);
            if (massage == null)
            {
                return NotFound();
            }

            return View(massage);
        }

        // POST: Massage/Delete/{id}
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var massage = await _context.Massages.FindAsync(id);
            _context.Massages.Remove(massage);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}