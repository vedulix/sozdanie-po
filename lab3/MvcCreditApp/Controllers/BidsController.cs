using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcCreditApp.Data;
using MvcCreditApp.Models;

namespace MvcCreditApp.Controllers
{
    public class BidsController : Controller
    {
        private readonly CreditContext _context;

        public BidsController(CreditContext context)
        {
            _context = context;
        }

        // GET: Bids
        public async Task<IActionResult> Index()
        {
            return View(await _context.Bids.ToListAsync());
        }

        // GET: Bids/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var bid = await _context.Bids.FirstOrDefaultAsync(m => m.BidId == id);
            if (bid == null)
                return NotFound();

            return View(bid);
        }

        // GET: Bids/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Bids/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BidId,Name,CreditHead,bidDate")] Bid bid)
        {
            if (ModelState.IsValid)
            {
                bid.bidDate = DateTime.Now;
                _context.Add(bid);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bid);
        }

        // GET: Bids/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var bid = await _context.Bids.FindAsync(id);
            if (bid == null)
                return NotFound();

            return View(bid);
        }

        // POST: Bids/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BidId,Name,CreditHead,bidDate")] Bid bid)
        {
            if (id != bid.BidId)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bid);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BidExists(bid.BidId))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(bid);
        }

        // GET: Bids/Delete/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var bid = await _context.Bids.FirstOrDefaultAsync(m => m.BidId == id);
            if (bid == null)
                return NotFound();

            return View(bid);
        }

        // POST: Bids/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bid = await _context.Bids.FindAsync(id);
            if (bid != null)
                _context.Bids.Remove(bid);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BidExists(int id)
        {
            return _context.Bids.Any(e => e.BidId == id);
        }
    }
}
