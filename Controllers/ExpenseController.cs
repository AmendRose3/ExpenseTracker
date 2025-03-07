using Microsoft.AspNetCore.Mvc;
using ExpenseTracker.Models;
using System.Collections.Generic;
using System.Linq;

namespace ExpenseTracker.Controllers
{
    public class ExpenseController : Controller
    {
        private static List<Expense> expenses = new List<Expense>
        {
            new Expense { Id = 1, Value = 25.50m, Description = "Netflix" },
            new Expense { Id = 2, Value = 15.00m, Description = "Grocery" }
        };

        public IActionResult Index()
        {
            ViewBag.TotalExpense = expenses.Sum(e => e.Value);
            return View(expenses);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Expense expense)
        {
            expense.Id = expenses.Any() ? expenses.Max(e => e.Id) + 1 : 1;
            expenses.Add(expense);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var expense = expenses.FirstOrDefault(e => e.Id == id);
            return View(expense);
        }

        [HttpPost]
        public IActionResult Edit(Expense updatedExpense)
        {
            var expense = expenses.FirstOrDefault(e => e.Id == updatedExpense.Id);
            if (expense != null)
            {
                expense.Value = updatedExpense.Value;
                expense.Description = updatedExpense.Description;
            }
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var expense = expenses.FirstOrDefault(e => e.Id == id);
            if (expense != null)
            {
                expenses.Remove(expense);
            }
            return RedirectToAction("Index");
        }
    }
}
