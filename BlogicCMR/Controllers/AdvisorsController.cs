using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlogicCMR.Data;
using BlogicCMR.Models;

namespace BlogicCMR.Controllers
{
    public class AdvisorsController : Controller
    {
        private readonly BlogicDbContext _context;

        public AdvisorsController(BlogicDbContext context)
        {
            _context = context;
        }

        // GET: /Advisors
        public async Task<IActionResult> Index()
        {
            var advisors = await _context.Advisors.ToListAsync();
            return View(advisors);
        }

        // GET: /Advisors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var advisor = await _context.Advisors
                                        .FirstOrDefaultAsync(a => a.AdvisorId == id);
            if (advisor == null)
                return NotFound();

            return View(advisor);
        }

        // GET: /Advisors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Advisors/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Advisor advisor)
        {
            if (!ModelState.IsValid)
                return View(advisor);

            // Vloží lomítko mezi 6. a 7. číslici
            advisor.PersonalIdNumber = FormatRC(advisor.PersonalIdNumber);

            _context.Advisors.Add(advisor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: /Advisors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var advisor = await _context.Advisors.FindAsync(id);
            if (advisor == null)
                return NotFound();

            // Odstraníme lomítko, aby view očekávalo 10 číslic
            advisor.PersonalIdNumber = advisor.PersonalIdNumber?.Replace("/", "");
            return View(advisor);
        }

        // POST: /Advisors/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Advisor advisor)
        {
            if (id != advisor.AdvisorId)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(advisor);

            // Znovu vložíme lomítko před uložením
            advisor.PersonalIdNumber = FormatRC(advisor.PersonalIdNumber);

            try
            {
                _context.Advisors.Update(advisor);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Advisors.AnyAsync(a => a.AdvisorId == id))
                    return NotFound();
                throw;
            }

            return RedirectToAction(nameof(Details), new { id = advisor.AdvisorId });
        }

        // GET: /Advisors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var advisor = await _context.Advisors
                                        .FirstOrDefaultAsync(a => a.AdvisorId == id);
            if (advisor == null)
                return NotFound();

            return View(advisor);
        }

        // POST: /Advisors/DeleteConfirmed/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var advisor = await _context.Advisors.FindAsync(id);
            if (advisor != null)
            {
                _context.Advisors.Remove(advisor);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // Vloží lomítko do RC (např. "8501011234" → "850101/1234")
        private string FormatRC(string rc)
        {
            if (!string.IsNullOrEmpty(rc) && rc.Length == 10 && !rc.Contains("/"))
                return rc.Insert(6, "/");
            return rc;
        }
    }
}
