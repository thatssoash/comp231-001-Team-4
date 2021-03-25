using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AccountApp.BOL;

namespace AccountApp.Controllers
{
    public class CustomFormsController : Controller
    {
        private readonly AccountAppDbContext _context;

        public CustomFormsController(AccountAppDbContext context)
        {
            _context = context;
        }

        // GET: CustomForms
        public async Task<IActionResult> Index()
        {
            return View(await _context.CustomForms.ToListAsync());
        }

        // GET: CustomForms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customForm = await _context.CustomForms
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customForm == null)
            {
                return NotFound();
            }

            return View(customForm);
        }

        // GET: CustomForms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CustomForms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Number,FormName,Code,Title,Status")] CustomForm customForm)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customForm);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customForm);
        }

        // GET: CustomForms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customForm = await _context.CustomForms.FindAsync(id);
            if (customForm == null)
            {
                return NotFound();
            }
            return View(customForm);
        }

        // POST: CustomForms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Number,FormName,Code,Title,Status")] CustomForm customForm)
        {
            if (id != customForm.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customForm);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomFormExists(customForm.Id))
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
            return View(customForm);
        }

        // GET: CustomForms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customForm = await _context.CustomForms
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customForm == null)
            {
                return NotFound();
            }

            return View(customForm);
        }

        // POST: CustomForms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customForm = await _context.CustomForms.FindAsync(id);
            _context.CustomForms.Remove(customForm);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomFormExists(int id)
        {
            return _context.CustomForms.Any(e => e.Id == id);
        }
    }
}
