using BlogicCMR.Data;
using BlogicCMR.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogicCMR.Controllers
{
    public class ContractsController : Controller
    {
        private readonly BlogicDbContext _context;

        public ContractsController(BlogicDbContext context) => _context = context;

        // GET: /Contracts
        public async Task<IActionResult> Index()
        {
            var contracts = await _context.Contracts
                .Include(c => c.Client)
                .Include(c => c.Manager)
                .Include(c => c.Participants)
                .ToListAsync();
            return View(contracts);
        }

        // GET: /Contracts/Details/{id}
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var contract = await _context.Contracts
                .Include(c => c.Client)
                .Include(c => c.Manager)
                .Include(c => c.Participants)
                .FirstOrDefaultAsync(c => c.ContractId == id.Value);

            if (contract == null)
                return NotFound();

            return View(contract);
        }

        // GET: /Contracts/Create
        public async Task<IActionResult> Create()
        {
            await PopulateDropdownsAsync();
            return View(new Contract
            {
                DateSigned    = DateTime.Today,
                DateValidFrom = DateTime.Today,
                DateEnd       = DateTime.Today
            });
        }

        // POST: /Contracts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Contract contract)
        {
            if (!ModelState.IsValid)
            {
                await PopulateDropdownsAsync(
                    contract.ClientId,
                    contract.ManagerId,
                    contract.ParticipantIds);
                return View(contract);
            }

            foreach (var aid in contract.ParticipantIds.Distinct())
            {
                var advisor = await _context.Advisors.FindAsync(aid);
                if (advisor != null)
                    contract.Participants.Add(advisor);
            }

            _context.Contracts.Add(contract);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: /Contracts/Edit/{id}
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var contract = await _context.Contracts
                .Include(c => c.Participants)
                .FirstOrDefaultAsync(c => c.ContractId == id.Value);

            if (contract == null)
                return NotFound();

            await PopulateDropdownsAsync(
                contract.ClientId,
                contract.ManagerId,
                contract.Participants.Select(a => a.AdvisorId));

            return View(contract);
        }

        // POST: /Contracts/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Contract contract)
        {
            if (id != contract.ContractId)
                return BadRequest();

            if (!ModelState.IsValid)
            {
                await PopulateDropdownsAsync(
                    contract.ClientId,
                    contract.ManagerId,
                    contract.ParticipantIds);
                return View(contract);
            }

            var existing = await _context.Contracts
                .Include(c => c.Participants)
                .FirstOrDefaultAsync(c => c.ContractId == id);
            if (existing == null)
                return NotFound();

            existing.ClientId      = contract.ClientId;
            existing.ManagerId     = contract.ManagerId;
            existing.DateSigned    = contract.DateSigned;
            existing.DateValidFrom = contract.DateValidFrom;
            existing.DateEnd       = contract.DateEnd;

            existing.Participants.Clear();
            foreach (var aid in contract.ParticipantIds.Distinct())
            {
                var advisor = await _context.Advisors.FindAsync(aid);
                if (advisor != null)
                    existing.Participants.Add(advisor);
            }

            try
            {
                _context.Update(existing);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ContractExists(id))
                    return NotFound();
                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: /Contracts/Delete/{id}
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var contract = await _context.Contracts
                .Include(c => c.Client)
                .Include(c => c.Manager)
                .FirstOrDefaultAsync(c => c.ContractId == id.Value);

            if (contract == null)
                return NotFound();

            return View(contract);
        }

        // POST: /Contracts/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contract = await _context.Contracts.FindAsync(id);
            if (contract != null)
            {
                _context.Contracts.Remove(contract);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // Helper pro naplnění dropdown seznamů
        private async Task PopulateDropdownsAsync(
            int? selectedClientId          = null,
            int? selectedManagerId         = null,
            IEnumerable<int>? selectedParticipantIds = null)
        {
            var clients  = await _context.Clients.ToListAsync();
            var advisors = await _context.Advisors.ToListAsync();

            ViewBag.Clients        = new SelectList(clients, "ClientId", "FullName", selectedClientId);
            ViewBag.Advisors       = new SelectList(advisors, "AdvisorId", "FullName", selectedManagerId);
            ViewBag.ParticipantIds = new MultiSelectList(advisors, "AdvisorId", "FullName", selectedParticipantIds);
        }

        private async Task<bool> ContractExists(int id)
            => await _context.Contracts.AnyAsync(e => e.ContractId == id);
    }
}
