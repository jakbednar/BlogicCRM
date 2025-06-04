using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlogicCMR.Data;
using BlogicCMR.Models;

namespace BlogicCMR.Controllers
{
    public class ClientsController : Controller
    {
        private readonly BlogicDbContext _context;

        public ClientsController(BlogicDbContext context)
        {
            _context = context;
        }

        // GET: /Clients
        public async Task<IActionResult> Index()
        {
            var clients = await _context.Clients.ToListAsync();
            return View(clients);
        }

        // GET: /Clients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var client = await _context.Clients
                                      .FirstOrDefaultAsync(c => c.ClientId == id);
            if (client == null)
                return NotFound();

            return View(client);
        }

        // GET: /Clients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Clients/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Client client)
        {
            if (!ModelState.IsValid)
                return View(client);

            // Vloží lomítko mezi 6. a 7. číslici
            client.PersonalIdNumber = FormatRC(client.PersonalIdNumber);

            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: /Clients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var client = await _context.Clients.FindAsync(id);
            if (client == null)
                return NotFound();

            // Odstraníme lomítko, aby view očekávalo 10 číslic
            client.PersonalIdNumber = client.PersonalIdNumber?.Replace("/", "");
            return View(client);
        }

        // POST: /Clients/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Client client)
        {
            if (id != client.ClientId)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(client);

            // Znovu vložíme lomítko před uložením
            client.PersonalIdNumber = FormatRC(client.PersonalIdNumber);

            try
            {
                _context.Clients.Update(client);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Clients.AnyAsync(c => c.ClientId == id))
                    return NotFound();
                throw;
            }

            return RedirectToAction(nameof(Details), new { id = client.ClientId });
        }

        // GET: /Clients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var client = await _context.Clients
                                      .FirstOrDefaultAsync(c => c.ClientId == id);
            if (client == null)
                return NotFound();

            return View(client);
        }

        // POST: /Clients/DeleteConfirmed/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client != null)
            {
                _context.Clients.Remove(client);
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
