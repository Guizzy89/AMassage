using Microsoft.AspNetCore.Mvc;
using AMassage.Data;

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
        public IActionResult Index()
        {
            return View(_context.Bookings.Where(b => b.IsAvailable).ToList());
        }

        // GET: Booking/BookSlot/{id}
        public IActionResult BookSlot(int id)
        {
            var booking = _context.Bookings.Find(id);
            if (booking == null || !booking.IsAvailable)
                return NotFound();
            return View(booking);
        }

        // POST: Booking/BookSlot/{id}
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

        // GET: Booking/Create
        public IActionResult Create() => View();

        // POST: Booking/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Booking booking)
        {
            if (!ModelState.IsValid)
                return View(booking);
            _context.Bookings.Add(booking);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // GET: Booking/Edit/{id}
        public IActionResult Edit(int id)
        {
            var booking = _context.Bookings.Find(id);
            if (booking == null)
                return NotFound();
            return View(booking);
        }

        // POST: Booking/Edit/{id}
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

        // GET: Booking/Delete/{id}
        public IActionResult Delete(int id)
        {
            var booking = _context.Bookings.Find(id);
            if (booking == null)
                return NotFound();
            return View(booking);
        }

        // POST: Booking/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var booking = _context.Bookings.Find(id);
            _context.Bookings.Remove(booking);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}