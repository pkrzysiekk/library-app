using Microsoft.AspNetCore.Mvc;
using Wypozyczalnia.Data;
using Wypozyczalnia.Models;

namespace Wypozyczalnia.Controllers
{
    public class ClientController : Controller
    {
        private LibraryContext context;
        public ClientController(LibraryContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            var clients = context.Clients.Select(x => x).ToList();
            return View(clients);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var client = await context.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            context.Clients.Remove(client);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Name,LastName")] Client client)
        {
            if (ModelState.IsValid)
            {
                context.Add(client);
                await context.SaveChangesAsync();
                return RedirectToAction("Index");

            }
            return View(client);
        }
    
        public async Task<IActionResult> Edit(int id)
        {
            var client = await context.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            return View(client);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,Client client)
        {
            if (ModelState.IsValid)
            {
                context.Update(client);
                await context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(client);
        }

    }
}
