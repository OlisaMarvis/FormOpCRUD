using FormOpCRUD.Data;
using FormOpCRUD.Models;
using FormOpCRUD.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace FormOpCRUD.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MVCDemoDbContext _context;

        public HomeController(ILogger<HomeController> logger,
            MVCDemoDbContext context)
        {
            _logger = logger;
            _context = context;
        }
       

        public async Task<IActionResult> Index()
        {
            var employees = await _context.Employees.ToListAsync();
            return View(employees);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        
        public async Task<IActionResult> Delete(Employee employees)
        {
            var employee = await _context.Employees.FindAsync(employees.Id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}