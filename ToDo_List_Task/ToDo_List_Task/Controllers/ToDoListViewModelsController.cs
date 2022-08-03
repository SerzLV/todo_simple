using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ToDo_List_Task.Helpers;
using ToDo_List_Task.Models;

namespace ToDo_List_Task.Controllers
{
    public class ToDoListViewModelsController : Controller
    {
        private readonly AppDbContext _context;

        public ToDoListViewModelsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ToDoListViewModels
        public async Task<IActionResult> Index()
        {
              return _context.ToDoListItems != null ? 
                          View(await _context.ToDoListItems.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.ToDoListItems'  is null.");
        }

        // GET: ToDoListViewModels/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.ToDoListItems == null)
            {
                return NotFound();
            }

            var toDoListViewModel = await _context.ToDoListItems
                .FirstOrDefaultAsync(m => m.ID == id);
            if (toDoListViewModel == null)
            {
                return NotFound();
            }

            return View(toDoListViewModel);
        }

        // GET: ToDoListViewModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ToDoListViewModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,IsCompleted")] ToDoListViewModel toDoListViewModel)
        {
            if (ModelState.IsValid)
            {
                toDoListViewModel.ID = Guid.NewGuid();
                _context.Add(toDoListViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(toDoListViewModel);
        }

        // GET: ToDoListViewModels/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.ToDoListItems == null)
            {
                return NotFound();
            }

            var toDoListViewModel = await _context.ToDoListItems.FindAsync(id);
            if (toDoListViewModel == null)
            {
                return NotFound();
            }
            return View(toDoListViewModel);
        }

        // POST: ToDoListViewModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ID,Name,IsCompleted")] ToDoListViewModel toDoListViewModel)
        {
            if (id != toDoListViewModel.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(toDoListViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ToDoListViewModelExists(toDoListViewModel.ID))
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
            return View(toDoListViewModel);
        }

        // GET: ToDoListViewModels/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.ToDoListItems == null)
            {
                return NotFound();
            }

            var toDoListViewModel = await _context.ToDoListItems
                .FirstOrDefaultAsync(m => m.ID == id);
            if (toDoListViewModel == null)
            {
                return NotFound();
            }

            return View(toDoListViewModel);
        }

        // POST: ToDoListViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.ToDoListItems == null)
            {
                return Problem("Entity set 'AppDbContext.ToDoListItems'  is null.");
            }
            var toDoListViewModel = await _context.ToDoListItems.FindAsync(id);
            if (toDoListViewModel != null)
            {
                _context.ToDoListItems.Remove(toDoListViewModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ToDoListViewModelExists(Guid id)
        {
          return (_context.ToDoListItems?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
