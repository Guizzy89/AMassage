using AMassage.Data;
using AMassage.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace AMassage.Controllers
{
    public class BookingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookingController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Booking
        public async Task<IActionResult> Index()
        {
            return View(await _context.Bookings.ToListAsync());
        }

        // GET: Booking/BookSlot/{id}
        public IActionResult BookSlot(int id)
        {
            var booking = _context.Bookings.Find(id);
            if (booking == null || !booking.IsAvailable)
                return NotFound();
            return View(booking);
        }

        /*// POST: Booking/BookSlot/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult BookSlot(int id, Booking booking)
        {
            var dbBooking = _context.Bookings.Find(id);
            if (dbBooking == null || !dbBooking.IsAvailable)
                return NotFound();
            dbBooking.ClientName = booking.ClientName;
            dbBooking.PhoneNumber = booking.PhoneNumber;
            dbBooking.Comment = booking.Comment;
            dbBooking.IsAvailable = false;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        */

        [HttpPost]
        public async Task<IActionResult> BookSlot(int id, [Bind("ClientName,PhoneNumber,Comment")] Booking booking)
        {
            var currentBooking = await _context.Bookings.FindAsync(id);
            if (currentBooking == null || currentBooking.IsAvailable == false)
            {
                return NotFound();
            }

            currentBooking.ClientName = booking.ClientName;
            currentBooking.PhoneNumber = booking.PhoneNumber;
            currentBooking.Comment = booking.Comment;
            currentBooking.IsAvailable = false;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        // GET: Booking/Create
        public IActionResult Create() => View();

        // POST: Booking/Create
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,SlotDate,IsAvailable")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                _context.Add(booking); // Добавляем новый слот в базу данных
                await _context.SaveChangesAsync(); // Сохраняем изменения
                return RedirectToAction(nameof(Index)); // Перенаправляем на страницу со списком слотов
            }
            return View(booking); // Если модель невалидная, возвращаем форму обратно
        }

        // GET: Booking/Edit/{id}
        public IActionResult Edit(int id)
        {
            var booking = _context.Bookings.Find(id);
            if (booking == null)
                return NotFound();
            return View(booking);
        }

        /*// POST: Booking/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Booking booking)
        {
            if (!ModelState.IsValid || id != booking.Id)
                return View(booking);
            try
            {
                _context.Update(booking);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Unable to save changes.");
            }
            return RedirectToAction(nameof(Index));
        }
        */

        // GET: /Booking/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }
            return View(booking);
        }

        // POST: /Booking/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SlotDate,IsAvailable,ClientName,PhoneNumber,Comment")] Booking booking)
        {
            if (id != booking.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(booking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Bookings.Any(e => e.Id == id))
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
            return View(booking);
        }

        // GET: Booking/Delete/{id}
        public IActionResult Delete(int id)
        {
            var booking = _context.Bookings.Find(id);
            if (booking == null)
                return NotFound();
            return View(booking);
        }

        /*// POST: Booking/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var booking = _context.Bookings.Find(id);
            _context.Bookings.Remove(booking);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        */

        // GET: /Booking/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings.FirstOrDefaultAsync(m => m.Id == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // POST: /Booking/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}