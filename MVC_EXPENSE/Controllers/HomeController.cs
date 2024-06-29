using Microsoft.AspNetCore.Mvc;
using MVC_EXPENSE.Models;
using System.Diagnostics;

namespace MVC_EXPENSE.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        //step 1 added
        private readonly SpendSmartDbContext _context;

       //step2 added
       public HomeController(ILogger<HomeController> logger,SpendSmartDbContext context)
        {
            _logger = logger;
            //i addded
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Expense()

        {
            var allexpenses=_context.Expenses.ToList();
            var totalexpenses = allexpenses.Sum(x => x.value);
            ViewBag.Expenses = totalexpenses;
            return View(allexpenses);
        }
        public IActionResult CreateEditExpense(int?id)
        {
            if (id != null)
            {
                //edit->load an expense by id
                var expensesInDb = _context.Expenses.SingleOrDefault(expense => expense.Id == id);
                return View(expensesInDb);
            }
            
            return View();
        }
        public IActionResult DeleteExpense(int id)
        {
            var expensesInDb = _context.Expenses.SingleOrDefault(expense => expense.Id == id);
            _context.Expenses.Remove(expensesInDb);
            _context.SaveChanges();
            return RedirectToAction("Expense");
        }
        public IActionResult CreateEditExpenseform(Expense model)
        {
            if (model.Id == 0)
            {
                //create
                _context.Expenses.Add(model);

            }
            else
            {
                //Edit
                _context.Expenses.Update(model);
            }

            //added

            
            _context.SaveChanges();

            return RedirectToAction("Expense");
        }




        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
