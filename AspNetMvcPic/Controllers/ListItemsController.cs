using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AspNetMvcPic.Data;
using AspNetMvcPic.Models;
using AspNetMvcPic.ViewModel;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace AspNetMvcPic.Controllers
{
    public class ListItemsController : Controller
    {
        private readonly ListItemDbContext _context;
        private readonly IWebHostEnvironment honstEnv;

        public ListItemsController(ListItemDbContext context, IWebHostEnvironment honstEnv)
        {
            _context = context;
            this.honstEnv = honstEnv;
        }

        // GET: ListItems
        public async Task<IActionResult> Index()
        {
            return View(await _context.Items.ToListAsync());
        }

        // GET: ListItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listItem = await _context.Items
                .FirstOrDefaultAsync(m => m.Id == id);
            if (listItem == null)
            {
                return NotFound();
            }

            return View(listItem);
        }

        // GET: ListItems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ListItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,ProfileImage")] ListItemViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = UploadedFile(model);

                ListItem listItem = new ListItem
                {
                    Name = model.FirstName,
                    ProfilePicture = uniqueFileName
                };

                _context.Add(listItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        


        // GET: ListItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listItem = await _context.Items.FindAsync(id);
            if (listItem == null)
            {
                return NotFound();
            }
            return View(listItem);
        }

        // POST: ListItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,ProfileImage")] ListItemViewModel listItem)
        {
            if (id != listItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                string uniqueFileName = UploadedFile(listItem);

                ListItem listItemZ = new ListItem
                {
                    Name = listItem.FirstName,
                    ProfilePicture = uniqueFileName
                };

                try
                {
                    _context.Update(listItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ListItemExists(listItem.Id))
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
            return View(listItem);
        }

        // GET: ListItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listItem = await _context.Items
                .FirstOrDefaultAsync(m => m.Id == id);
            if (listItem == null)
            {
                return NotFound();
            }

            return View(listItem);
        }

        // POST: ListItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var listItem = await _context.Items.FindAsync(id);
            _context.Items.Remove(listItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ListItemExists(int id)
        {
            return _context.Items.Any(e => e.Id == id);
        }

        private string UploadedFile(ListItemViewModel model)
        {
            string uniqueFileName = null;

            if (model.ProfileImage != null)
            {
                string uploadsFolder = Path.Combine(honstEnv.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ProfileImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ProfileImage.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
    }
}
